using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : MonoBehaviour
{
    protected virtual void Start()
    {
        //MovementStateController.Instance.OnMoveStateChanged += HandleMovement;
    }

    public virtual void OnMovementEventPerfomed(BrigeTail.MovementEvent movementEvent)
    {
    }

    public virtual void SetRotationSpeed(float speed)
    { }
}