using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Earth : MonoBehaviour
{
    [SerializeField] Slider HPSlider;
    [SerializeField] Text HPText;
    [SerializeField] int HPInit;
    [SerializeField] int HPMax;
    private int _HP;
    public int HP
    {
        set
        {
            _HP = value;
            HPSlider.value = (float)_HP / (float)HPMax;
            HPText.text = _HP.ToString();
            if (_HP <= 0)
            {
                SpawnDestroyEffect();
                Destroy(this.gameObject);
            }
        }
        get
        {
            return _HP;
        }
    }
    [SerializeField] GameObject destroyEffect;
    private void SpawnDestroyEffect()
    {
        GameObject effect = Instantiate(destroyEffect);
        effect.transform.position = transform.position;
    }
}
