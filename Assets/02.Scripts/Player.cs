using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
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
            energySlider.value = tmpValue / energyMax;
            int tmpIntValue = (int)tmpValue;
            energyText.text = tmpIntValue.ToString();
        }
        get { return _energy; }
    }
    public float energyInit;
    public float energyMax;
    public float energyPerSec;
    [SerializeField] Text energyText;
    [SerializeField] Slider energySlider;

    // missile
    [SerializeField] MissileLauncher missileLauncher;

    private List<GameObject> deck = new List<GameObject>();
    private void Awake()
    {
        energy = energyInit;
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
        missileLauncher.missile = Instantiate(missilePrefab, missileLauncher.missilePreviewPoint);
    }
    public void TryLanchMissile()
    {
        if (missileLauncher.IsMissileEquiped() == false) return;
        float energyRequired = missileLauncher.GetEnergyRequired();
        if (energyRequired <= energy)
        {
            energy -= energyRequired;
            missileLauncher.LaunchMissile();
        }
        else
        {
            // Pop up ui - warn not enough energy
        }
    }
}
