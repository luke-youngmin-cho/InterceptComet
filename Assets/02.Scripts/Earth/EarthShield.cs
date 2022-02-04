using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EarthShield : MonoBehaviour
{
    [SerializeField] Slider shieldGaugeSlider;
    [SerializeField] Text shieldGaugeText;
    [SerializeField] int shieldInit;
    [SerializeField] int shieldMax;
    private int _shield;
    public int shield
    {
        set
        {
            _shield = value;
            shieldGaugeSlider.value = (float)_shield / (float)shieldMax;
            shieldGaugeText.text = _shield.ToString();
            if(_shield <= 0)
            {   
                if (gameObject.activeSelf)
                {
                    gameObject.SetActive(false);
                    SpawnDestroyEffect();
                }   
            }
            else
            {
                if (gameObject.activeSelf == false)
                {
                    gameObject.SetActive(true);
                    SpawnCreateEffect();
                }   
            }
        }
        get
        {
            return _shield;
        }
    }
    [SerializeField] GameObject shieldGameObject;
    [SerializeField] GameObject destroyEffect;
    [SerializeField] GameObject createEffect;
    private void Awake()
    {
        shield = shieldInit;
    }
    private void SpawnDestroyEffect()
    {
        GameObject effect = Instantiate(destroyEffect);
        effect.transform.position = transform.position;
        shieldGameObject.SetActive(false);
    }
    private void SpawnCreateEffect()
    {
        GameObject effect = Instantiate(createEffect);
        effect.transform.position = transform.position;
        shieldGameObject.SetActive(true);
    }
}
