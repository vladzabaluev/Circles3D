using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotating : MoveState
{
    [SerializeField] private float _rotationSpeed;
    private float _currentRotationSpeed;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _currentRotationSpeed = _rotationSpeed;
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

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(-Vector3.up, _currentRotationSpeed * Time.deltaTime); //������ ����������� �������� � ������� +- � ���
    }
}