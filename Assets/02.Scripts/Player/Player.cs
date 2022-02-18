using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    static public Player instance;
    private void Awake()
    {
        instance = this;
        GameStateManager.instance.OnGameStateChanged += OnGameStateChanged;
    }
    // energy
    private float _energy;
    public float energy
    {
        set
        {
            float tmpValue = value;
            if (tmpValue > energyMax)
                tmpValue = energyMax;
            _energy = tmpValue;

            if (PlayerUI.instance != null)
            {
                PlayerUI.instance.energySlider.value = tmpValue / energyMax;
                int tmpIntValue = (int)tmpValue;
                PlayerUI.instance.energyText.text = tmpIntValue.ToString();
            }
        }
        get { return _energy; }
    }
    public float energyInit;
    public float energyMax;
    public float energyPerSec;

    // missile
    [HideInInspector]public List<MissileLauncher> missileLaunchers = new List<MissileLauncher>();
    private List<GameObject> deck = new List<GameObject>();
    private GameObject currentMissilePrefab;
    private void Start()
    {
        energy = energyInit;
    }
    private void OnDestroy()
    {
        GameStateManager.instance.OnGameStateChanged -= OnGameStateChanged;
    }
    private void Update()
    {
        energy += energyPerSec * Time.deltaTime;
    }
    public void SetDeck(List<GameObject> missileGOList)
    {
        deck = missileGOList;
    }

    public void ChooseMissile(GameObject missilePrefab)
    {
        currentMissilePrefab = missilePrefab;
        RefreshMissile();
    }
    public void RefreshMissile()
    {
        if (currentMissilePrefab == null) return;
        foreach (MissileLauncher missileLauncher in missileLaunchers)
        {
            missileLauncher.missile = currentMissilePrefab;
            missileLauncher.SetPreviewMissile(currentMissilePrefab);
        }
    }
    public void TryLanchMissile()
    {
        if (missileLaunchers[0].IsMissileEquiped() == false) return;
        float energyRequired = missileLaunchers[0].GetEnergyRequired();
        if (energyRequired <= energy)
        {
            energy -= energyRequired;
            foreach (MissileLauncher missileLauncher in missileLaunchers)
            {   
                missileLauncher.LaunchMissile();
            }
        }
        else
        {
            // Pop up ui - warn not enough energy
        }
    }
    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Play;
    }
}
