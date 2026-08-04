using System;

namespace GodotBattler.Bluetooth;

/// <summary>
/// Loopback transport with no hardware dependency: anything sent is
/// echoed straight back as if a peer responded. Used in the editor and in
/// unit tests, and as the default until the real Windows dev-transport
/// and Android/iOS GDExtension transports exist. See 04_ROADMAP.md's
/// Bluetooth Architecture section.
/// </summary>
public sealed class MockBluetoothTransport : IBluetoothTransport
{
    /// <inheritdoc/>
    public event Action<byte[]>? PacketReceived;

    /// <inheritdoc/>
    public void StartScan()
    {
        // No hardware to scan; nothing to do.
    }

    /// <inheritdoc/>
    public void StopScan()
    {
        // No hardware to scan; nothing to do.
    }

    /// <inheritdoc/>
    public void Connect(string peerId)
    {
        // Loopback has no real peers to connect to; nothing to do.
    }

    /// <inheritdoc/>
    public void SendPacket(byte[] data)
    {
        PacketReceived?.Invoke(data);
    }
}
