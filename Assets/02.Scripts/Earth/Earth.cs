using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Earth : MonoBehaviour
{
    static public Earth instance;
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] Slider HPSlider;
    [SerializeField] Text HPText;
    [SerializeField] float HPInit;
    [SerializeField] float HPMax;
    private float _HP;
    public float HP
    {
        set
        {
            _HP = value;
            HPSlider.value = _HP / HPMax;
            int HPInt = (int)_HP;
            HPText.text = HPInt.ToString();
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

    [HideInInspector] public List<Transform> missileLaunchers = new List<Transform>();
    private void Start()
    {
        HP = HPInit;
    }
    private void SpawnDestroyEffect()
    {
        GameObject effect = Instantiate(destroyEffect);
        effect.transform.position = transform.position;
    }
    public void IncreaseHPMax(float increase)
    {
        HPMax += increase;
        HP += increase;
    }
    public void DecreaseHPMax(float decrease)
    {
        HPMax -= decrease;
        if (HPMax < HP)
            HP = HPMax;
    }
}
