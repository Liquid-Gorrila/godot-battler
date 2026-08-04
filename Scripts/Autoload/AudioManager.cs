using Godot;

namespace GodotBattler.Autoload;

/// <summary>
/// Plays SFX and music. Milestone 1 stub — no audio buses or asset
/// wiring yet.
/// </summary>
public partial class AudioManager : Node
{
    /// <summary>Plays a one-shot sound effect by resource path. Stub.</summary>
    /// <param name="resourcePath">The <c>res://</c> path to the sound resource.</param>
    public void PlaySfx(string resourcePath)
    {
        GD.Print($"AudioManager: would play SFX at {resourcePath}");
    }
}
