namespace GodotBattler.Data;

/// <summary>
/// Numeric stats for a monster, per 02_GAMEPLAY.md's Core Stats list.
/// </summary>
public sealed class MonsterStats
{
    /// <summary>Current health points.</summary>
    public float Health { get; set; } = 100f;

    /// <summary>Current energy available for actions/training.</summary>
    public float Energy { get; set; } = 100f;

    /// <summary>Physical attack power used in battle resolution.</summary>
    public float Attack { get; set; } = 10f;

    /// <summary>Physical defense used in battle resolution.</summary>
    public float Defense { get; set; } = 10f;

    /// <summary>Turn-order/initiative stat used in battle resolution.</summary>
    public float Speed { get; set; } = 10f;

    /// <summary>Affects training efficiency and some battle outcomes.</summary>
    public float Intelligence { get; set; } = 10f;

    /// <summary>Affects special/spirit-based battle outcomes.</summary>
    public float Spirit { get; set; } = 10f;

    /// <summary>Bond with the player; influences care responsiveness and evolution.</summary>
    public float Friendship { get; set; }

    /// <summary>Rises over time, lowered by feeding.</summary>
    public float Hunger { get; set; } = 100f;

    /// <summary>Overall happiness; affected by care quality.</summary>
    public float Mood { get; set; } = 100f;

    /// <summary>Lowered over time, raised by cleaning.</summary>
    public float Cleanliness { get; set; } = 100f;

    /// <summary>Rises with activity, lowered by rest.</summary>
    public float Fatigue { get; set; }

    /// <summary>Age in in-game time units; drives lifecycle stage transitions.</summary>
    public float Age { get; set; }

    /// <summary>Physical weight; influenced by feeding and activity.</summary>
    public float Weight { get; set; } = 1f;
}
