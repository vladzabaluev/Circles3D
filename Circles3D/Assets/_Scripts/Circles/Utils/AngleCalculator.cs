using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleCalculator : MonoBehaviour
{
    [SerializeField] private RotatingAroundPoint _rotatingAroundPoint;

    [SerializeField] private Transform _previousCircle;
    [SerializeField] private Transform _nextCircle;

    // Start is called before the first frame update
    private void Start()
    {
        if (_previousCircle == null)
        {
            _rotatingAroundPoint.SetStartAndTargetAngles(CalculateAngle(transform.position, transform.position), CalculateAngle(transform.position, _nextCircle.position));
            //Debug.Log("Нет предыдущего");
        }
        else if (_nextCircle == null)
        {
            _rotatingAroundPoint.SetStartAndTargetAngles(CalculateAngle(transform.position, _previousCircle.position), CalculateAngle(transform.position, transform.position));
            //Debug.Log("Нет следующего");
        }
        else
        {
            _rotatingAroundPoint.SetStartAndTargetAngles(CalculateAngle(transform.position, _previousCircle.position), CalculateAngle(transform.position, _nextCircle.position));

            //Debug.Log("Есть оба");
        }
    }

    private float CalculateAngle(Vector3 startPoint, Vector3 finishPoint)
    {
        Vector3 direction = finishPoint - startPoint;
        //Debug.DrawRay(transform.position, (finishPoint - startPoint).normalized * 5, Color.blue, 100);
        //Debug.DrawRay(transform.position, transform.right * 5, Color.blue, 100);
        //Debug.Log(Vector3.Angle(transform.right, (_nextCircle.position - transform.position).normalized));

        float angle = Vector3.Angle(transform.right, direction.normalized);
        if (_nextCircle != null)
            if (finishPoint == _nextCircle.position)
            {
                angle = 360 - angle;
            }
        //Debug.Log("Угол между объектами: " + angle);

        return angle;
    }

    //#if UNITY_EDITOR

    //    private void OnDrawGizmosSelected()
    //    {
    //        Gizmos.color = Color.gray;
    //        Gizmos.DrawRay(transform.position, (_nextCircle.position - transform.position).normalized * 5);
    //        Gizmos.DrawRay(transform.position, transform.right * 5);
    //        Debug.Log(Vector3.Angle(transform.right, (_nextCircle.position - transform.position).normalized));
    //    }

    //#endif
}