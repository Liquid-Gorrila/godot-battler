using Godot;

namespace GodotBattler.Autoload;

/// <summary>
/// Owns top-level application/game state (boot, main menu, in-session,
/// paused) and coordinates which scene is active. Milestone 1 stub —
/// scene transitions land alongside the first real UI.
/// </summary>
public partial class GameManager : Node
{
    /// <summary>The coarse states the game can be in.</summary>
    public enum GameState
    {
        Boot,
        MainMenu,
        Playing,
        Paused,
    }

    /// <summary>The current top-level game state.</summary>
    public GameState CurrentState { get; private set; } = GameState.Boot;

    /// <inheritdoc/>
    public override void _Ready()
    {
        CurrentState = GameState.MainMenu;
    }

    /// <summary>Transitions the game to a new top-level state.</summary>
    /// <param name="newState">The state to transition to.</param>
    public void ChangeState(GameState newState)
    {
        if (newState == CurrentState)
        {
            return;
        }

        CurrentState = newState;
    }
}
