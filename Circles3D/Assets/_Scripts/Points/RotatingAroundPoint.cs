using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingAroundPoint : MoveState
{
    //[SerializeField] private Circle _circle;
    [SerializeField] private Transform _centerPoint;

    [SerializeField] private float _rotationSpeed;

    [SerializeField] private float _targetAngle;
    [SerializeField] private float _startAngle;

    private bool _isRotating;

    public Action TargetComplete;
    private int _clockwiseValue;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public void SetRotationDirection(bool circlesIsClockwise)
    {
        _clockwiseValue = circlesIsClockwise ? -1 : 1;
    }

    public void SetStartAndTargetAngles(float startAngle, float targetAngle)
    {
        _startAngle = startAngle;
        _targetAngle = targetAngle;
        if (_targetAngle < 0)
        {
            _targetAngle += 360;
        }
        transform.eulerAngles = new Vector3(0, _startAngle, 0);
        transform.SetParent(transform.parent.parent);
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
            if (Mathf.Abs(transform.eulerAngles.y - _targetAngle) > 2f)
            {
                transform.RotateAround(_centerPoint.position, Vector3.up * _clockwiseValue, _rotationSpeed * Time.deltaTime); //������ ����������� �������� � ������� +- � ���
            }
            else
            {
                TargetComplete?.Invoke();
            }
        }
    }
}