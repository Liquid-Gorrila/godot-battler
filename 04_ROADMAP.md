# Roadmap & Technical Plan

## Milestones

Expands the roadmap line in `03_ARCHITECTURE.md` into concrete scope.

- **M1 — Single-player virtual pet.** Project scaffold, `Monster` data
  model, core stats/needs loop (feed/clean/play), save/load, basic UI.
- **M2 — Evolution system.** Genetics/personality/care-mistake tracking,
  evolution resolution rules, data-driven species/evolution `Resource`
  files.
- **M3 — Bluetooth battles.** Deterministic battle simulator + seeded
  RNG, the Bluetooth transport (mock → Windows dev transport → Android/iOS
  GDExtension), snapshot/seed exchange protocol.
- **M4 — Online backend.** Accounts, ranked/friends/guilds, cloud save
  sync, server-authoritative validation.
- **M5 — Live events.** Seasonal content, event scheduling.

## Bluetooth Architecture

**Why this needs special handling:** Godot 4 has no built-in Bluetooth
Classic/LE API in core. Nothing reachable from GDScript or C# talks to a
platform's Bluetooth radio directly — this has to come from native code.

**Approach:** a **GDExtension** (`bluetooth_le` native module) is the
integration point for the shipping platforms, plus two lower-cost paths
for iterating on the protocol before the native module exists:

- **Android (ship target 1).** Native code (C++/JNI), or a bundled
  Android `.aar` plugin registered alongside the GDExtension — Godot 4.2+
  supports shipping both together — driving `BluetoothLeScanner` and
  `BluetoothGattServer`/`BluetoothGattClient`.
- **iOS (ship target 2).** An Obj-C++ shim over `CoreBluetooth`
  (`CBCentralManager`/`CBPeripheralManager`), compiled into the
  extension's iOS `.xcframework` target.
- **Windows (dev/test transport, not a ship target).** A real BLE backend
  for desktop, implemented in **pure C#** via the `InTheHand.BluetoothLE`
  NuGet package, which wraps the `Windows.Devices.Bluetooth` WinRT APIs —
  no GDExtension needed here since .NET can reach WinRT directly. This
  lets two BLE-equipped Windows dev machines run an actual nearby-battle
  exchange during development, so the battle protocol and
  `BluetoothManager` logic can be iterated on and tested without
  deploying to Android/iOS on every change.
- **Editor/CI with no BLE hardware at all.** A **mock transport**
  (loopback / two-local-monster fake exchange) so gameplay and the battle
  simulator can be built and unit-tested without any radio.

**Godot-facing surface:** `BluetoothManager` (C# autoload) depends only
on an `IBluetoothTransport` interface:

```
StartScan()
Connect(peer)
SendPacket(byte[] data)
event OnPacketReceived
```

Mock, Windows, and the GDExtension-backed mobile implementation are three
interchangeable implementations of the same interface — nothing above it
needs to know which one is active.

**Implementation order:**
1. M1 ships the mock transport only — pure logic, no hardware.
2. Early M3 adds the Windows transport for real two-device testing.
3. Later M3 adds the actual Android/iOS GDExtension once the protocol is
   proven out on Windows.

**Payload:** matches `02_GAMEPLAY.md` — monster snapshot + random seed +
protocol version only, exchanged once per battle. Small, one-shot BLE
payload; no streaming needed.

**Consequence for M1:** build the deterministic RNG utility and the
`IBluetoothTransport` seam now, even though the real GDExtension is M3
work, so `BattleManager` doesn't need re-architecting later.

## Save Format

JSON payload with a `version` int and a migration hook per version bump —
but **not plaintext-trusted**. Stats, genetics, and care history directly
drive battle outcomes and the Bluetooth-exchanged monster snapshot, so a
hand-edited save is a direct cheating vector. Mitigation:

- Serialize to JSON, then wrap it: compute an HMAC-SHA256 over the
  payload using a key derived on-device (not stored in plaintext
  alongside the data) and store `{ payload, hmac, version }`.
  `SaveManager` rejects/flags loads whose HMAC doesn't match instead of
  silently trusting the file.
- Encrypt the payload (AES) at rest so casual editing in a text/hex
  editor isn't possible, on top of the integrity check.
- The same signed representation is what gets serialized into the
  Bluetooth battle snapshot, so a tampered local save also fails
  verification on the *opponent's* device during a nearby battle, not
  just on load.
- **Limits:** this raises the bar against casual/local tampering, but a
  determined attacker with root/jailbreak on their own device can still
  forge their own signing key. Real anti-cheat — server-authoritative
  validation — is out of scope until M4's online backend.
- Cloud sync remains optional, deferred to M4.

## Coding Standards

SOLID, dependency injection via constructor-injected manager references
(not singleton lookups buried in logic), data-driven `Resource` files for
species/personality/evolution tables, UI separated from logic, unit tests
for deterministic battle math — plus a house style **stricter than
Microsoft's default C# conventions**, aimed at "a junior dev can read
this without asking the author":

- Explicit access modifiers on every member (no implicit `private`).
- Braces required on every `if`/`for`/`while`, even single-line bodies.
- No public fields — properties only.
- `///` XML doc summary required on every public type/member.
- No magic numbers/strings — named constants or config resources.
- Guard clauses / early return instead of deep nesting.
- Nullable reference types enabled project-wide; nulls handled
  explicitly rather than suppressed.
- One class per file, filename matches class name.
- Enforced via a committed `.editorconfig` with these rules set to
  warning/error, not left as unwritten convention.
