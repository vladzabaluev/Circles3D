using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TriggerSpawner : MonoBehaviour
{
    [SerializeField] private TransitionTrigger _trigger;
    [SerializeField] private SectorPainting _sectorPainter;

    [SerializeField] private BrigeTail _selfTail;

    private List<TransitionTrigger> _triggers;

    [SerializeField] private bool _CreateStartTriggers;

    private bool _isEdgeTrigger;
    public bool IsEdgeTrigger => _isEdgeTrigger;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    private void Start()
    {
        _triggers = new List<TransitionTrigger>();

        if (_CreateStartTriggers)
        {
            CreateTransitionTriggers();
        }

        TransitionController.Instance.OnTransitionComplete += CheckOnComplince;
    }

    private void OnDisable()
    {
        TransitionController.Instance.OnTransitionComplete -= CheckOnComplince;
    }

    private void CheckOnComplince(BrigeTail playerTail, BrigeTail newTargetTrail)
    {
        if (_selfTail == playerTail)
        {
            DestroyTriggers();
        }
        if (_selfTail == newTargetTrail)
        {
            CreateTransitionTriggers();
        }
    }

    private void CreateTransitionTriggers()
    {
        List<Vector3> positions = _sectorPainter.GetLinePointsPositions();
        for (int i = 0; i < positions.Count; i++)
        {
            if (i == 0 || i == positions.Count - 1)
            {
                SpawnTrigger(transform.position + positions[i] /** transform.parent.localScale.x*/).IsEdgeTrigger = true;
            }
            else
            {
                SpawnTrigger(transform.position + positions[i] /** transform.parent.localScale.x*/).IsEdgeTrigger = false;
            }
        }
    }

    private void DestroyTriggers()
    {
        Destroy(gameObject);
    }

    private TransitionTrigger SpawnTrigger(Vector3 position)
    {
        TransitionTrigger transitionTrigger = Instantiate(_trigger, position, Quaternion.identity, transform);
        transitionTrigger.Initialize(_selfTail);
        _triggers.Add(transitionTrigger);
        return transitionTrigger;
        //transitionTrigger.transform.position = position;
    }
}