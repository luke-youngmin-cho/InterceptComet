using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfAfterTime : MonoBehaviour
{
    [SerializeField] float delay;
    private void OnEnable()
    {
        Destroy(this.gameObject, delay);
    }
}
