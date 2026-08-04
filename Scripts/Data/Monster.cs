using System;
using System.Collections.Generic;

namespace GodotBattler.Data;

/// <summary>Lifecycle stage of a monster, per 02_GAMEPLAY.md.</summary>
public enum LifeStage
{
    Egg,
    Infant,
    Child,
    Teen,
    Adult,
    Elder,
}

/// <summary>Personality archetype, per 02_GAMEPLAY.md.</summary>
public enum Personality
{
    Aggressive,
    Calm,
    Curious,
    Lazy,
    Loyal,
    Brave,
    Cowardly,
    Playful,
}

/// <summary>
/// A single monster instance: identity, lifecycle stage, stats, and the
/// hidden values that drive evolution and battles. Models the field list
/// from 03_ARCHITECTURE.md's Monster Model section.
/// </summary>
public sealed class Monster
{
    /// <summary>Stable identity for this monster across saves and battles.</summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>Species identifier, resolved against a data-driven species Resource.</summary>
    public string Species { get; set; } = string.Empty;

    /// <summary>Current lifecycle stage.</summary>
    public LifeStage Stage { get; set; } = LifeStage.Egg;

    /// <summary>Visible numeric stats.</summary>
    public MonsterStats Stats { get; set; } = new();

    /// <summary>Personality archetype; influences evolution and battle behavior.</summary>
    public Personality Personality { get; set; } = Personality.Calm;

    /// <summary>Cosmetic/behavioral traits unlocked through care and evolution.</summary>
    public List<string> Traits { get; set; } = new();

    /// <summary>Hidden genetic values that influence evolution outcomes.</summary>
    public Dictionary<string, float> Genetics { get; set; } = new();

    /// <summary>Count of care mistakes made, tracked toward evolution/legacy outcomes.</summary>
    public int CareMistakes { get; set; }

    /// <summary>Items currently held by this monster.</summary>
    public List<string> Inventory { get; set; } = new();

    /// <summary>Past battle outcomes, most recent last.</summary>
    public List<BattleHistoryEntry> BattleHistory { get; set; } = new();
}
