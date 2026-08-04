namespace GodotBattler.Battle;

/// <summary>Outcome of a resolved battle.</summary>
public sealed class BattleResult
{
    /// <summary>Species identifier of the winning monster.</summary>
    public string Winner { get; set; } = string.Empty;
}
