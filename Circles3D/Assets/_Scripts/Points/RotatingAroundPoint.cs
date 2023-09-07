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
    [SerializeField] private float _startAngle;

    private bool _isRotating;

    public Action TargetComplete;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _centerPoint = _circle.transform;
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
                transform.RotateAround(_centerPoint.position, -Vector3.up, _rotationSpeed * Time.deltaTime); //Менять направление движение с помощью +- у оси
            }
            else
            {
                TargetComplete?.Invoke();
            }
        }
    }
}