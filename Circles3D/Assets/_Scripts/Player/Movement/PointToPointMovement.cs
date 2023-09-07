using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointToPointMovement : MoveState
{
    public static PointToPointMovement Instace;

    private Transform _CenterPoint;
    private Transform _EdgePoint;

    private Transform _target;

    [SerializeField] private float _Speed;
    private bool _moveToCenter;
    private bool _canSwitchTarget;

    public Action AnotherCircleReached;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instace == null)
        {
            Instace = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override void Start()
    {
        base.Start();
        TouchInputHandler.Instance.OnDirectionChanged += ChangeDirection;
    }

    public override void OnMovementEventPerfomed(BrigeTail.MovementEvent movementEvent)
    {
        _canSwitchTarget = movementEvent != BrigeTail.MovementEvent.TransitionStart;
    }

    private void ChangeDirection(bool moveToCenter)
    {
        _moveToCenter = moveToCenter;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_target != null)
        {
            if (_canSwitchTarget)
            {
                if (_moveToCenter)
                {
                    _target = _CenterPoint;
                }
                else
                {
                    _target = _EdgePoint;
                }
            }
            else
            {
                _target = _EdgePoint;
                if (Vector3.Distance(transform.position, new Vector3(_target.position.x, transform.position.y, _target.position.z)) < 0.1f)
                {
                    AnotherCircleReached?.Invoke();
                }
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