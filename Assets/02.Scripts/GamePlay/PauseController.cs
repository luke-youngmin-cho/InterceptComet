using UnityEngine;
public class PauseController: MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameState currentGameState = GameStateManager.instance.CurrentGameState;
            GameState newGameState = currentGameState == GameState.Play
                ? GameState.Paused
                : GameState.Play;

            GameStateManager.instance.SetState(newGameState);
        }
    }
}
