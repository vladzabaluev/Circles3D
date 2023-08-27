using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : MonoBehaviour
{
    protected virtual void Start()
    {
        //MovementStateController.Instance.OnMoveStateChanged += HandleMovement;
    }

    protected virtual void HandleMovement(MovementStateController.MoveState state)
    {
    }

    public virtual void OnMovementEventPerfomed(BrigeTail.MovementEvent movementEvent)
    {
    }

    public virtual void StartMoving()
    {
    }

    public virtual void StopMoving()
    {
    }
}