using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TransitionTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning(1);
    }
}