using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Comet : MonoBehaviour
{
    [SerializeField] string name;
    private float _HP;
    public float HP
    {
        set
        {
            float tmpValue = value;
            if (tmpValue <= 0f)
            {
                SpawnDestroyEffect();
                Destroy(gameObject);
                tmpValue = 0f;
            }   
            else if (tmpValue > HPMax)
                tmpValue = HPMax;
            _HP = tmpValue;
            HPSlider.value = _HP / HPMax;
            int tmpValueInt = (int)tmpValue;
            HPText.text = tmpValueInt.ToString();
        }
        get { return _HP; }
    }
    [SerializeField] float HPinit;
    [SerializeField] float HPMax;
    [SerializeField] Slider HPSlider;
    [SerializeField] Text HPText;

    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] List<OvalOrbitFor2Point> list_OvalOrbit;
    int currentOvalOrbitIndex=0;

    [SerializeField] float _startAngleDegree;
    [SerializeField] GameObject destroyEffect;
    private float startAngleDegree 
    {
        set
        {
            float tmpValue = value;
            if(Mathf.Abs(value) >= 360)
            {
                tmpValue = 0;
            }
            _startAngleDegree = tmpValue;
        }
        get
        {
            startAngleDegree = _startAngleDegree;
            return _startAngleDegree;
        }
    }
    private float startAngleRadian
    {
        get
        {
            return startAngleDegree * Mathf.Deg2Rad;
        }
    }
    float accumulatedAngle;
    float currentAngle;
    e_OrbitMoveFSM orbitMoveFSM;


    Transform tr;
    private void Awake()
    {
        GameStateManager.instance.OnGameStateChanged += OnGameStateChanged;
        tr = GetComponent<Transform>();
        currentAngle = startAngleRadian;
    }
    private void Start()
    {
        orbitMoveFSM = e_OrbitMoveFSM.CheckOrbitIsAvailable;
        HP = HPinit;
    }
    private void OnDestroy()
    {
        GameStateManager.instance.OnGameStateChanged -= OnGameStateChanged;
    }
    
    private void FixedUpdate()
    {
        OrbitMoveWorkFlow();
    }
    private void Move(float speed)
    {
        float circumference = list_OvalOrbit[currentOvalOrbitIndex].circumference;
        float deltaAngle = (speed / circumference) * Time.fixedDeltaTime;
        currentAngle += deltaAngle;
        tr.position = list_OvalOrbit[currentOvalOrbitIndex].CalcPos(currentAngle);
        
        accumulatedAngle += deltaAngle;
    }
    private void OrbitMoveWorkFlow()
    {
        switch (orbitMoveFSM)
        {
            case e_OrbitMoveFSM.Idle:
                break;
            case e_OrbitMoveFSM.CheckOrbitIsAvailable:                
                if (list_OvalOrbit[currentOvalOrbitIndex].isAvailable == true) 
                {
                    orbitMoveFSM = e_OrbitMoveFSM.CheckCycleFinished;
                }                    
                break;
            case e_OrbitMoveFSM.CheckCycleFinished:
                if(accumulatedAngle >= Mathf.PI * 2)
                {
                    orbitMoveFSM = e_OrbitMoveFSM.SwitchOrbit;
                }
                else
                {
                    Move(speed);
                }
                break;
            case e_OrbitMoveFSM.SwitchOrbit:
                currentOvalOrbitIndex++;
                if (currentOvalOrbitIndex > list_OvalOrbit.Count - 1)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    currentAngle = startAngleRadian;
                    accumulatedAngle = 0;
                    orbitMoveFSM = e_OrbitMoveFSM.CheckCycleFinished;
                }
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == null) return;
        if(collision.gameObject.tag == "EarthShield")
        {
            collision.gameObject.GetComponent<EarthShield>().shield -= 1;
            SpawnDestroyEffect();
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Earth")
        {
            collision.gameObject.GetComponent<Earth>().HP -= damage;
            SpawnDestroyEffect();
            Destroy(this.gameObject);
        }

    }
    private void SpawnDestroyEffect()
    {
        GameObject effect = Instantiate(destroyEffect);
        effect.transform.position = tr.position;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Play;
    }
}

enum e_OrbitMoveFSM
{
    Idle,
    CheckOrbitIsAvailable,
    CheckCycleFinished,
    SwitchOrbit,
}