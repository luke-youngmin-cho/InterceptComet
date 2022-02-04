using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileExplosionEffectWithComet : MonoBehaviour
{
    public void SetRotationOppositeToCollision(SphereCollider other)
    {
        Vector3 coordRelComet = transform.position - other.transform.position;
        float angle = Mathf.Asin(coordRelComet.z / other.radius);
        ParticleSystem.MainModule particleMain = this.gameObject.GetComponent<ParticleSystem>().main;
        particleMain.startRotation = new ParticleSystem.MinMaxCurve(angle);
        // offset
        transform.Translate(coordRelComet.normalized * (other.radius/2));
    }
}
