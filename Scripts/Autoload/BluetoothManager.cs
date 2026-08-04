using Godot;
using GodotBattler.Bluetooth;

namespace GodotBattler.Autoload;

/// <summary>
/// Godot-facing entry point for Bluetooth battles. Delegates all real
/// work to an <see cref="IBluetoothTransport"/> so the mock, Windows
/// dev/test, and Android/iOS GDExtension backends are interchangeable.
/// Defaults to <see cref="MockBluetoothTransport"/> until the real
/// transports land — see 04_ROADMAP.md's Bluetooth Architecture section.
/// </summary>
public partial class BluetoothManager : Node
{
    private IBluetoothTransport _transport = new MockBluetoothTransport();

    /// <inheritdoc/>
    public override void _Ready()
    {
        _transport.PacketReceived += OnPacketReceived;
    }

    /// <summary>
    /// Replaces the active transport, e.g. to swap in the Windows
    /// dev-transport or the real GDExtension backend once they exist.
    /// </summary>
    /// <param name="transport">The transport implementation to use.</param>
    public void SetTransport(IBluetoothTransport transport)
    {
        _transport.PacketReceived -= OnPacketReceived;
        _transport = transport;
        _transport.PacketReceived += OnPacketReceived;
    }

    /// <summary>Begins scanning for nearby battle opponents.</summary>
    public void StartScan()
    {
        _transport.StartScan();
    }

    private void OnPacketReceived(byte[] data)
    {
        // Milestone 3: deserialize into a battle exchange payload
        // (monster snapshot + seed + protocol version) and hand it to
        // BattleManager.
        _ = data;
    }
}
