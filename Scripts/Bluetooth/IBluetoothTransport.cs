using System;

namespace GodotBattler.Bluetooth;

/// <summary>
/// Platform-agnostic Bluetooth LE transport used by <c>BluetoothManager</c>.
/// Godot has no built-in Bluetooth API, so real implementations live
/// outside this interface: a GDExtension-backed transport for the
/// Android/iOS ship targets, and an <c>InTheHand.BluetoothLE</c>-backed
/// transport for Windows dev/test. See 04_ROADMAP.md's Bluetooth
/// Architecture section.
/// </summary>
public interface IBluetoothTransport
{
    /// <summary>Raised when a packet arrives from a connected peer.</summary>
    event Action<byte[]>? PacketReceived;

    /// <summary>Begins scanning for nearby peers advertising the battle service.</summary>
    void StartScan();

    /// <summary>Stops an in-progress scan.</summary>
    void StopScan();

    /// <summary>Connects to a discovered peer by its transport-specific identifier.</summary>
    /// <param name="peerId">Opaque identifier for the peer, as reported during scanning.</param>
    void Connect(string peerId);

    /// <summary>Sends a packet to the connected peer.</summary>
    /// <param name="data">Raw bytes to send — the serialized battle exchange payload.</param>
    void SendPacket(byte[] data);
}
