using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeScore : MonoBehaviour
{
    float elapsedTime;
    [SerializeField] Text timeText;
    [SerializeField] Text milliSecText;
    private void Awake()
    {
        GameStateManager.instance.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnDestroy()
    {
        GameStateManager.instance.OnGameStateChanged -= OnGameStateChanged;
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
        string timeString = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        timeText.text = timeString;
        string milliSecString = string.Format(":{0:D2}", timeSpan.Milliseconds/10);
        milliSecText.text = milliSecString;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Play;
    }
}
