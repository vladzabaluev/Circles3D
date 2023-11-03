using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BrigeTail;

public class BrigeTail : MonoBehaviour
{
    [Header("Точки передвижения")]
    [SerializeField] private Transform _centerPoint;

    [SerializeField] private Transform _edgePoint;

    [Header("Передвижения")]
    [SerializeField] private SelfRotating _circleRotating;

    [SerializeField] private RotatingAroundPoint _movementToTransitionArea;

    [Header("Следующий круг")]
    [SerializeField] private BrigeTail _targetTail;

    public BrigeTail TargetTail => _targetTail;

    [SerializeField] private bool _isPlayerHere;
    public bool IsPlayerHere => _isPlayerHere;

    [SerializeField] private bool _canTransitToThisCircle;
    public bool CanTransitToThisCircle => _canTransitToThisCircle;

    private bool _isPlayerInTransitionArea = false;

    private List<MoveState> _dependendedMotions = new List<MoveState>();

    [SerializeField] private RotationTarget _rotationTarget;

    private void Start()
    {
        _dependendedMotions.Add(PointToPointMovement.Instace);
        _dependendedMotions.Add(_circleRotating);
        _dependendedMotions.Add(_movementToTransitionArea);

        _movementToTransitionArea.SetRotationDirection(_circleRotating.IsClockwise);

        _movementToTransitionArea.TargetComplete += (() =>
        {
            SendMotionEventPerfomed(MovementEvent.WaitingTransitionMoment);
            _isPlayerInTransitionArea = true;
        });

        SendMotionEventPerfomed(MovementEvent.Normal);

        PointToPointMovement.Instace.AnotherCircleReached += (() =>
        {
            if (_isPlayerHere)
                SendMotionEventPerfomed(MovementEvent.ComingToTransitionArea);
        });

        float rotationSpeed = Random.Range(4, 7) * 10;
        foreach (var d in _dependendedMotions)
        {
            d.SetRotationSpeed(rotationSpeed);
        }
    }

    public void StartGame()
    {
        if (IsPlayerHere)
        {
            PointToPointMovement.Instace.SetTargetPoints(_edgePoint, _centerPoint);
            SendMotionEventPerfomed(MovementEvent.ComingToTransitionArea);
        }
    }

    public void DoTransit(bool isNewBrigeTail)
    {
        if (isNewBrigeTail)
        {
            PointToPointMovement.Instace.SetTargetPoints(_edgePoint, _centerPoint);
            if (PointToPointMovement.Instace.gameObject.TryGetComponent(out PlayerRotater playerRotater))
            {
                playerRotater.SetRotationTarget(_rotationTarget);
            }
            _canTransitToThisCircle = false;
            if (_targetTail)
                _targetTail._canTransitToThisCircle = true;
            _isPlayerHere = true;
            SendMotionEventPerfomed(MovementEvent.TransitionStart);
        }
        else
        {
            _isPlayerHere = false;
        }
    }

    public bool CheckTransitionPossibility()
    {
        if (_isPlayerInTransitionArea)
        {
            Debug.Log("TRUE");
            return true;
        }
        else
        {
            Debug.Log("FALSE");
            return false;
        }
    }

    private void SendMotionEventPerfomed(MovementEvent movementEvent)
    {
        foreach (var d in _dependendedMotions)
        {
            d.OnMovementEventPerfomed(movementEvent);
        }
    }

    public enum MovementEvent
    {
        TransitionStart,
        TransitionEnd,
        ComingToTransitionArea,
        WaitingTransitionMoment,
        Normal
    }
}