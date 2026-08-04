using Godot;
using GodotBattler.Data;

namespace GodotBattler.Autoload;

/// <summary>
/// Owns the player's active <see cref="Monster"/> and exposes the care
/// actions that mutate it. Publishes <c>EventBus</c> events rather than
/// being called into directly by UI. Milestone 1 stub — feed/clean/play
/// need-decay curves and care-mistake tracking land with the full needs
/// loop.
/// </summary>
public partial class MonsterManager : Node
{
    /// <summary>The player's currently active monster, if hatched.</summary>
    public Monster? ActiveMonster { get; private set; }

    /// <summary>Assigns the active monster, e.g. after loading a save or hatching an egg.</summary>
    /// <param name="monster">The monster to make active.</param>
    public void SetActiveMonster(Monster monster)
    {
        ActiveMonster = monster;
        EventBus.PublishMonsterStatsChanged();
    }

    /// <summary>Feeds the active monster, reducing hunger. Stub.</summary>
    public void Feed()
    {
        if (ActiveMonster is null)
        {
            return;
        }

        EventBus.PublishMonsterStatsChanged();
    }

    /// <summary>Cleans the active monster, restoring cleanliness. Stub.</summary>
    public void Clean()
    {
        if (ActiveMonster is null)
        {
            return;
        }

        EventBus.PublishMonsterStatsChanged();
    }

    /// <summary>Plays with the active monster, raising mood and friendship. Stub.</summary>
    public void Play()
    {
        if (ActiveMonster is null)
        {
            return;
        }

        EventBus.PublishMonsterStatsChanged();
    }
}
