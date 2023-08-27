using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointToPointMovement : MoveState
{
    [SerializeField] private Transform _CenterPoint;
    [SerializeField] private Transform _EdgePoint;

    private Transform _target;

    [SerializeField] private float _Speed;
    private bool _moveDirection;
    private bool _canSwitchTarget;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        TouchInputHandler.Instance.OnDirectionChanged += ChangeDirection;
    }

    protected override void HandleMovement(MovementStateController.MoveState state)
    {
        base.HandleMovement(state);
        _canSwitchTarget = state != MovementStateController.MoveState.Transition;
    }

    public override void StartMoving()
    {
        base.StartMoving();
        _canSwitchTarget = true;
    }

    public override void StopMoving()
    {
        base.StopMoving();
        _canSwitchTarget = false;
    }

    public override void OnMovementEventPerfomed(BrigeTail.MovementEvent movementEvent)
    {
        _canSwitchTarget = movementEvent != BrigeTail.MovementEvent.TransitionStart;
    }

    private void ChangeDirection(bool moveDirection)
    {
        _moveDirection = moveDirection;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_canSwitchTarget && _target != null)
        {
            if (_moveDirection)
            {
                _target = _CenterPoint;
            }
            else
            {
                _target = _EdgePoint;
            }
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_target.position.x, transform.position.y, _target.position.z), _Speed * Time.deltaTime);
        }

        //if (_targetPosition == _FarFromCenterPoint.position)
        //{
        //    Debug.Log("aaa");
        //}
    }

    public void SetTargetPoints(Transform edgePoint, Transform centerPoint)
    {
        _EdgePoint = edgePoint;
        _CenterPoint = centerPoint;
        _target = _CenterPoint;
    }
}