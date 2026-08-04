namespace GodotBattler.Battle;

/// <summary>
/// The minimal representation of a monster exchanged over Bluetooth for a
/// battle — species and the stats battle resolution needs, per
/// 02_GAMEPLAY.md ("Bluetooth exchanges only battle state and random
/// seed"). Not the full <c>Monster</c> save record, and not trusted
/// as-is: see 04_ROADMAP.md's save format section for the signing this
/// snapshot inherits from the source save.
/// </summary>
public sealed class MonsterSnapshot
{
    /// <summary>Species identifier.</summary>
    public string Species { get; set; } = string.Empty;

    /// <summary>Attack stat at the time of the snapshot.</summary>
    public float Attack { get; set; }

    /// <summary>Defense stat at the time of the snapshot.</summary>
    public float Defense { get; set; }

    /// <summary>Speed stat at the time of the snapshot.</summary>
    public float Speed { get; set; }
}
