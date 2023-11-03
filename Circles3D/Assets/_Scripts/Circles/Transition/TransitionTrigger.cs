using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TransitionTrigger : MonoBehaviour
{
    private BrigeTail _selfTail;

    public bool IsEdgeTrigger { get; set; }

    public void Initialize(BrigeTail brigeTail)
    {
        _selfTail = brigeTail;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BrigeTail otherCircle))
        {
            if (_selfTail != otherCircle)
            {
                TransitionController.Instance.Contact(otherCircle, IsEdgeTrigger);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out BrigeTail otherCircle))
        {
            if (_selfTail != otherCircle)
            {
                TransitionController.Instance.Break(otherCircle, IsEdgeTrigger);
            }
        }
    }
}