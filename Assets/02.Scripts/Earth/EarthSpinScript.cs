using UnityEngine;
using System.Collections;

public class EarthSpinScript : MonoBehaviour {
    public float speed = 10f;
    private void Awake()
    {
        GameStateManager.instance.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnDestroy()
    {
        GameStateManager.instance.OnGameStateChanged -= OnGameStateChanged;
    }
    void Update() {
        transform.Rotate(Vector3.up, speed * Time.deltaTime, Space.World);
    }
    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Play;
    }
}