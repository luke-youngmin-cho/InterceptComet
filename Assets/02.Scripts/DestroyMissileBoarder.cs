using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMissileBoarder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == null) return;

        Destroy(other.gameObject);
    }
}
