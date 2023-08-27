using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigeTail : MonoBehaviour
{
    [SerializeField] private Transform _centerPoint;
    [SerializeField] private Transform _edgePoint;

    [SerializeField] private PointToPointMovement _playerPointToPointMovement;
    [SerializeField] private SelfRotating _circleRotating;
    [SerializeField] private RotatingAroundPoint _movementToTransitionArea;

    private SectorPainting _selfPainter;

    //public PointToPointMovement PlayerPointToPointMovement => _playerPointToPointMovement;
    //public SelfRotating CircleRotating => _circleRotating;
    //public RotatingAroundPoint MovementToTransitionArea => _movementToTransitionArea;

    private List<MoveState> _dependendedMotions = new List<MoveState>();

    private void Start()
    {
        _dependendedMotions.Add(_playerPointToPointMovement);
        _dependendedMotions.Add(_circleRotating);
        _dependendedMotions.Add(_movementToTransitionArea);

        _movementToTransitionArea.TargetComplete += (() =>
        {
            SendMotionEventPerfomed(MovementEvent.WaitingTransitionMoment);
        });

        SendMotionEventPerfomed(MovementEvent.Normal);

        CreateTransitionTriggers();
    }

    public void StartGame()
    {
        _playerPointToPointMovement.SetTargetPoints(_edgePoint, _centerPoint);
        SendMotionEventPerfomed(MovementEvent.ComingToTransitionArea);
    }

    private void SendMotionEventPerfomed(MovementEvent movementEvent)
    {
        foreach (var d in _dependendedMotions)
        {
            d.OnMovementEventPerfomed(movementEvent);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void CreateTransitionTriggers()
    {
        transform.TryGetComponent(out _selfPainter);

        Vector3 firstPos, lastPos;
        _selfPainter.GetEdgeLineRenderPoints(out firstPos, out lastPos);

        TriggerSpawner.Instance.SpawnTrigger(transform.position + firstPos * transform.parent.localScale.x, transform);
        TriggerSpawner.Instance.SpawnTrigger(transform.position + lastPos * transform.parent.localScale.x, transform);
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