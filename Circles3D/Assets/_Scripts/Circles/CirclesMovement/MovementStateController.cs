using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class MovementStateController : MonoBehaviour
{
    public static MovementStateController Instance;

    private SelfRotating _circleRotating;
    private RotatingAroundPoint _targetPointRotating;
    private PointToPointMovement _movementBetweenTargetPoints;

    public MoveState _currentState;

    public enum MoveState
    {
        Transition,
        MovementToTransitionArea,
        WaitingTransitionMoment
    }

    public Action<MoveState> OnMoveStateChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMoveState(MoveState newState)
    {
        Instance.OnMoveStateChanged.Invoke(newState);
        _currentState = newState;
        switch (newState)
        {
            case MoveState.Transition:
                break;

            case MoveState.WaitingTransitionMoment:
                break;

            case MoveState.MovementToTransitionArea:
                break;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        Instance.OnMoveStateChanged?.Invoke(MoveState.Transition);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instance.OnMoveStateChanged.Invoke(_currentState);
        }
    }
}