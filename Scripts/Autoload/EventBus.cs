using System;
using Godot;
using GodotBattler.Battle;

namespace GodotBattler.Autoload;

/// <summary>
/// Central static event hub. Managers publish and subscribe to events
/// here instead of holding direct references to each other, per
/// 03_ARCHITECTURE.md ("Managers communicate through events rather than
/// direct references").
/// </summary>
public partial class EventBus : Node
{
    /// <summary>Raised whenever the active monster's stats change.</summary>
    public static event Action? MonsterStatsChanged;

    /// <summary>Raised when a save has completed successfully.</summary>
    public static event Action? SaveCompleted;

    /// <summary>Raised when a battle has been resolved.</summary>
    public static event Action<BattleResult>? BattleResolved;

    /// <summary>Publishes <see cref="MonsterStatsChanged"/>.</summary>
    public static void PublishMonsterStatsChanged()
    {
        MonsterStatsChanged?.Invoke();
    }

    /// <summary>Publishes <see cref="SaveCompleted"/>.</summary>
    public static void PublishSaveCompleted()
    {
        SaveCompleted?.Invoke();
    }

    /// <summary>Publishes <see cref="BattleResolved"/>.</summary>
    /// <param name="result">The resolved battle outcome.</param>
    public static void PublishBattleResolved(BattleResult result)
    {
        BattleResolved?.Invoke(result);
    }
}
