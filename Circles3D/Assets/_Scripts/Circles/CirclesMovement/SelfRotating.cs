using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SelfRotating : MoveState
{
    [SerializeField] private float _rotationSpeed;
    private float _currentRotationSpeed;

    [SerializeField]
    private bool _isClockwise;

    public bool IsClockwise => _isClockwise;
    private int _clockwiseValue;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        _clockwiseValue = _isClockwise ? 1 : -1;
    }

    public override void OnMovementEventPerfomed(BrigeTail.MovementEvent movementEvent)
    {
        base.OnMovementEventPerfomed(movementEvent);
        if (movementEvent == BrigeTail.MovementEvent.ComingToTransitionArea)
        {
            _currentRotationSpeed = 0;
        }
        else
        {
            _currentRotationSpeed = _rotationSpeed;
        }
    }

    public override void SetRotationSpeed(float speed)
    {
        base.SetRotationSpeed(speed);
        _rotationSpeed = speed;

        _currentRotationSpeed = _rotationSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(Vector3.up * _clockwiseValue, _currentRotationSpeed * Time.deltaTime); //������ ����������� �������� � ������� +- � ���
    }
}