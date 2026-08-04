namespace GodotBattler.Data;

/// <summary>
/// Versioned root object persisted by <c>SaveManager</c>. Add new fields
/// as optional/nullable and bump <see cref="Version"/> when the schema
/// changes, so older saves can be migrated instead of breaking outright.
/// </summary>
public sealed class SaveData
{
    /// <summary>Save format version, bumped on every breaking schema change.</summary>
    public int Version { get; set; }

    /// <summary>The player's active monster, if one exists yet.</summary>
    public Monster? ActiveMonster { get; set; }
}
