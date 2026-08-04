namespace GodotBattler.Data;

/// <summary>
/// On-disk wire format for a save file: the encrypted payload plus its
/// integrity signature. See <c>SaveManager</c> and 04_ROADMAP.md's save
/// format section for why saves aren't plain trusted JSON.
/// </summary>
internal sealed class SaveEnvelope
{
    /// <summary>Save format version this envelope was written with.</summary>
    public int Version { get; set; }

    /// <summary>Base64-encoded, AES-encrypted <see cref="SaveData"/> JSON.</summary>
    public string Payload { get; set; } = string.Empty;

    /// <summary>Base64-encoded HMAC-SHA256 of <see cref="Payload"/>, used to detect tampering.</summary>
    public string Hmac { get; set; } = string.Empty;
}
