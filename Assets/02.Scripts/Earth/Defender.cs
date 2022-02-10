using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Defender : MonoBehaviour
{
    static public Vector3 centorPos;
    static public float radius = 4f;
    static public float radiusOffset = 4f;
    static public float speed = 1f;
    static public float speedOffset = 1f;
    static public float damageReduce = 1f;

    private float _durability;
    public float durability
    {
        set
        {
            _durability = value;
            durabilitySlider.value = _durability / durabilityMax;
        }
        get
        {
            return _durability;
        }
    }
    [SerializeField] Slider durabilitySlider;
    [SerializeField] float durabilityInit;
    public float durabilityMax;
    private float elapsedTime;

    private Transform tr;
    private void Awake()
    {
        tr = GetComponent<Transform>();
        centorPos = Earth.instance.transform.position;        
    }
    private void Start()
    {
        durability = durabilityInit;
    }
    private void Update()
    {
        Move();
        elapsedTime += Time.deltaTime;
    }
    private void Move()
    {
        float angle = speed * elapsedTime;
        if (angle > Mathf.PI * 2)
            elapsedTime = 0;

        float z = radius * Mathf.Cos(speed * elapsedTime);
        float x = radius * Mathf.Sin(speed * elapsedTime);
        
        Vector3 pos = new Vector3(x, 0, z) + centorPos;
        tr.position = pos;

    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject tmpGO = collision.gameObject;
        if (tmpGO == null) return;
        if (tmpGO.layer == LayerMask.NameToLayer("Comet"))
        {
            float hp = tmpGO.GetComponent<Comet>().HP;
            tmpGO.GetComponent<Comet>().HP -= _durability;
            durability -= hp * damageReduce;
        }
    }
}
