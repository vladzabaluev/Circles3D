using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorPainting : MonoBehaviour
{
    [SerializeField] private LineRenderer _LineRenderer;
    [SerializeField] private Circle _circle;
    private Transform _centerPoint;

    [SerializeField] private float _radius = 6f;
    [SerializeField] private float _startAngle = 30f;
    [SerializeField] private float _endAngle = 150f;
    [SerializeField] private int _numPoints = 20;

    private void Start()
    {
        _centerPoint = _circle.transform;
        DrawSectorShape();
    }

    private void DrawSectorShape()
    {
        _LineRenderer.widthMultiplier = _circle.transform.parent.localScale.x;
        _LineRenderer.positionCount = _numPoints + 1;
        float angleStep = (_endAngle - _startAngle) / _numPoints;

        for (int i = 0; i <= _numPoints; i++)
        {
            float angle = _startAngle + angleStep * i;
            float x = _radius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float z = _radius * Mathf.Sin(Mathf.Deg2Rad * angle);
            Vector3 pointPosition = new Vector3(x, _centerPoint.position.y, z);
            _LineRenderer.SetPosition(i, pointPosition);
        }
    }

    public void GetEdgeLineRenderPoints(out Vector3 firstPoint, out Vector3 lastPoint)
    {
        firstPoint = _LineRenderer.GetPosition(0);
        lastPoint = _LineRenderer.GetPosition(_numPoints - 1);
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        _centerPoint = _circle.transform;
        if (_LineRenderer != null && _centerPoint != null)
            DrawSectorShape();
    }

#endif

    private void Update()
    {
    }
}