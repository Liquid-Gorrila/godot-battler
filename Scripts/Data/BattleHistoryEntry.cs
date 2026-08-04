using System;

namespace GodotBattler.Data;

/// <summary>A single recorded battle outcome kept on a monster's history.</summary>
public sealed class BattleHistoryEntry
{
    /// <summary>When the battle occurred.</summary>
    public DateTime OccurredAtUtc { get; set; }

    /// <summary>Whether this monster won the battle.</summary>
    public bool Won { get; set; }

    /// <summary>Species identifier of the opponent, if known.</summary>
    public string OpponentSpecies { get; set; } = string.Empty;
}
