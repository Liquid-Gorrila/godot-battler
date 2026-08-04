using System;
using Godot;
using GodotBattler.Battle;

namespace GodotBattler.Autoload;

/// <summary>
/// Resolves battles deterministically from a shared seed, so two devices
/// that exchange the same monster snapshots and seed (see 04_ROADMAP.md's
/// Bluetooth Architecture section) compute identical results locally
/// without exchanging anything further. Milestone 1 stub — the actual
/// resolution algorithm lands with Milestone 3.
/// </summary>
public partial class BattleManager : Node
{
    /// <summary>
    /// Creates the seeded RNG both battle participants must use, so the
    /// simulation stays deterministic across devices.
    /// </summary>
    /// <param name="seed">The random seed exchanged for this battle.</param>
    /// <returns>A <see cref="Random"/> seeded identically on both sides.</returns>
    public Random CreateBattleRandom(int seed)
    {
        return new Random(seed);
    }

    /// <summary>Resolves a battle between two monster snapshots. Stub.</summary>
    /// <param name="local">The local player's monster snapshot.</param>
    /// <param name="opponent">The opponent's monster snapshot.</param>
    /// <param name="seed">The shared random seed for this battle.</param>
    /// <returns>The resolved battle outcome.</returns>
    public BattleResult Resolve(MonsterSnapshot local, MonsterSnapshot opponent, int seed)
    {
        Random battleRandom = CreateBattleRandom(seed);
        _ = battleRandom;
        _ = opponent;

        return new BattleResult
        {
            Winner = local.Species,
        };
    }
}
