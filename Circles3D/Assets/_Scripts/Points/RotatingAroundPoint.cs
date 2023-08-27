using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingAroundPoint : MoveState
{
    [SerializeField] private Circle _circle;
    private Transform _centerPoint;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private float _targetAngle;

    private bool _isRotating;

    public Action TargetComplete;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _centerPoint = _circle.transform;
    }

    protected override void HandleMovement(MovementStateController.MoveState state)
    {
        base.HandleMovement(state);
        _isRotating = state == MovementStateController.MoveState.MovementToTransitionArea;
    }

    public override void StartMoving()
    {
        base.StartMoving();
        _isRotating = true;
    }

    public override void StopMoving()
    {
        base.StopMoving();
        _isRotating = false;
    }

    public override void OnMovementEventPerfomed(BrigeTail.MovementEvent movementEvent)
    {
        base.OnMovementEventPerfomed(movementEvent);
        _isRotating = movementEvent == BrigeTail.MovementEvent.ComingToTransitionArea;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isRotating)
        {
            if (Mathf.Abs(transform.eulerAngles.y - (360 - _targetAngle)) > 2f)
            {
                transform.RotateAround(_centerPoint.position, -Vector3.up, _rotationSpeed * Time.deltaTime); //Менять направление движение с помощью +- у оси
            }
            else
            {
                TargetComplete?.Invoke();
                transform.SetParent(transform.parent.parent);
            }
        }
    }
}