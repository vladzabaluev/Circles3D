using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TransitionTrigger : MonoBehaviour
{
    private Circle _parentCircle;
    private BrigeTail _selfTail;

    public bool IsEdgeTrigger { get; set; }

    public void Initialize(Circle parentCircle, BrigeTail brigeTail)
    {
        _parentCircle = parentCircle;
        _selfTail = brigeTail;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Circle circle))
        {
            if (_parentCircle != circle)
            {
                TransitionController.Instance.Contact(_selfTail, IsEdgeTrigger);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Circle circle))
        {
            if (_parentCircle != circle)
            {
                TransitionController.Instance.Break(_selfTail, IsEdgeTrigger);
            }
        }
    }
}