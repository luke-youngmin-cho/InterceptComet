using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    Transform tr;
    private void Awake()
    {
        tr = gameObject.GetComponent<Transform>();
    }

    private bool _isLaunched;
    public bool isLaunched
    {
        set
        {
            _isLaunched = value;
        }
    }
    public float damage;
    public float energyRequired;
    public float accel;
    public float accelTime;
    private float accelElapsedTime;
    [HideInInspector]public Vector3 dir;
    private float speed;

    [SerializeField] GameObject missileExplosionEffectWithComet;
    private void FixedUpdate()
    {
        if(_isLaunched == true)
        {            
            Accelerate();
            Move();
        }
    }
    private void Accelerate()
    {
        if (accelElapsedTime < accelTime)
            speed += accel * Time.fixedDeltaTime;
        accelElapsedTime += Time.fixedDeltaTime;
    }
    private void Move()
    {
        Vector3 deltaMove = dir * speed * Time.fixedDeltaTime;
        tr.Translate(deltaMove, Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == null) return;
        if (other.gameObject.layer == LayerMask.NameToLayer("Comet"))
        {
            GameObject effect = Instantiate(missileExplosionEffectWithComet);
            effect.transform.position = tr.position;
            
            SphereCollider sphereCollider;
            if(other.TryGetComponent<SphereCollider>(out sphereCollider))
            {
                effect.GetComponent<MissileExplosionEffectWithComet>().SetRotationOppositeToCollision(sphereCollider);
            }
            Comet tmpComet = other.gameObject.GetComponent<Comet>();
            tmpComet.HP -= damage;
        }
        Destroy(this.gameObject);
    }
}
