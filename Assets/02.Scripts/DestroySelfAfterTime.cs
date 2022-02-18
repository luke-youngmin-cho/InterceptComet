using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfAfterTime : MonoBehaviour
{
    [SerializeField] float delay;
    private void Awake()
    {
        GameStateManager.instance.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnDestroy()
    {
        GameStateManager.instance.OnGameStateChanged -= OnGameStateChanged;
    }
    private void OnEnable()
    {
        Destroy(this.gameObject, delay);
    }
    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Play;
    }
}
