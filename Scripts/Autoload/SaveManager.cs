using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Godot;
using GodotBattler.Data;

namespace GodotBattler.Autoload;

/// <summary>
/// Loads and saves <see cref="SaveData"/> to <c>user://</c>. The save
/// payload is AES-encrypted and HMAC-signed at rest so a hand-edited save
/// file is rejected rather than silently trusted — stats, genetics, and
/// care history directly drive battle outcomes, so a plaintext save would
/// be a direct cheating vector. See 04_ROADMAP.md's Save Format section.
/// This raises the bar against casual local tampering; it is not a
/// substitute for the server-authoritative validation planned for
/// Milestone 4.
/// </summary>
public partial class SaveManager : Node
{
    private const string SaveFileName = "genesis.save";
    private const string DeviceKeyFileName = ".device_key";
    private const int CurrentSaveVersion = 1;
    private const int AesKeySizeBytes = 32;

    /// <summary>Serializes, encrypts, signs, and writes <paramref name="data"/> to disk.</summary>
    /// <param name="data">The save data to persist.</param>
    public void Save(SaveData data)
    {
        data.Version = CurrentSaveVersion;

        string json = JsonSerializer.Serialize(data);
        byte[] key = GetOrCreateDeviceKey();
        byte[] encrypted = Encrypt(json, key);
        byte[] hmac = ComputeHmac(encrypted, key);

        SaveEnvelope envelope = new()
        {
            Version = CurrentSaveVersion,
            Payload = Convert.ToBase64String(encrypted),
            Hmac = Convert.ToBase64String(hmac),
        };

        string envelopeJson = JsonSerializer.Serialize(envelope);
        File.WriteAllText(GetSavePath(), envelopeJson);

        EventBus.PublishSaveCompleted();
    }

    /// <summary>
    /// Reads and verifies the save file. Returns <c>null</c> if no save
    /// exists or the integrity check fails (a corrupted or tampered file)
    /// rather than trusting whatever is on disk.
    /// </summary>
    /// <returns>The loaded save data, or <c>null</c> if none is available/valid.</returns>
    public SaveData? Load()
    {
        string path = GetSavePath();
        if (!File.Exists(path))
        {
            return null;
        }

        string envelopeJson = File.ReadAllText(path);
        SaveEnvelope? envelope = JsonSerializer.Deserialize<SaveEnvelope>(envelopeJson);
        if (envelope is null)
        {
            return null;
        }

        byte[] key = GetOrCreateDeviceKey();
        byte[] encrypted = Convert.FromBase64String(envelope.Payload);
        byte[] expectedHmac = ComputeHmac(encrypted, key);
        byte[] actualHmac = Convert.FromBase64String(envelope.Hmac);

        if (!CryptographicOperations.FixedTimeEquals(expectedHmac, actualHmac))
        {
            GD.PushError("SaveManager: save file failed its integrity check, refusing to load.");
            return null;
        }

        string json = Decrypt(encrypted, key);
        return JsonSerializer.Deserialize<SaveData>(json);
    }

    private static string GetSavePath()
    {
        return ProjectSettings.GlobalizePath($"user://{SaveFileName}");
    }

    private static byte[] ComputeHmac(byte[] data, byte[] key)
    {
        using HMACSHA256 hmac = new(key);
        return hmac.ComputeHash(data);
    }

    private static byte[] Encrypt(string plaintext, byte[] key)
    {
        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.GenerateIV();

        using MemoryStream stream = new();
        stream.Write(aes.IV, 0, aes.IV.Length);

        using (CryptoStream cryptoStream = new(stream, aes.CreateEncryptor(), CryptoStreamMode.Write))
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plaintext);
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
        }

        return stream.ToArray();
    }

    private static string Decrypt(byte[] data, byte[] key)
    {
        using Aes aes = Aes.Create();
        aes.Key = key;

        byte[] iv = new byte[aes.IV.Length];
        Array.Copy(data, iv, iv.Length);
        aes.IV = iv;

        using MemoryStream stream = new(data, iv.Length, data.Length - iv.Length);
        using CryptoStream cryptoStream = new(stream, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using StreamReader reader = new(cryptoStream);
        return reader.ReadToEnd();
    }

    /// <summary>
    /// Returns the per-device signing/encryption key, generating and
    /// persisting one on first run. Stored separately from the save file
    /// itself so the key can't be read straight out of a copied save.
    /// </summary>
    private static byte[] GetOrCreateDeviceKey()
    {
        string keyPath = ProjectSettings.GlobalizePath($"user://{DeviceKeyFileName}");
        if (File.Exists(keyPath))
        {
            return Convert.FromBase64String(File.ReadAllText(keyPath));
        }

        byte[] key = RandomNumberGenerator.GetBytes(AesKeySizeBytes);
        File.WriteAllText(keyPath, Convert.ToBase64String(key));
        return key;
    }
}
