using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brige : MonoBehaviour
{
    [SerializeField] private BrigeTail _SelfTail;
    [SerializeField] private BrigeTail _TargetTail;

    private SectorPainting _selfPainter;
    private SectorPainting _targetPainter;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
    }

#endif
}