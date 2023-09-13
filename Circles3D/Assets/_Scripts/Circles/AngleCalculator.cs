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
            Debug.Log("1 ый");
        }
        else if (_nextCircle == null)
        {
            _rotatingAroundPoint.SetStartAndTargetAngles(CalculateAngle(transform.position, _previousCircle.position), CalculateAngle(transform.position, transform.position));
            Debug.Log("3 uй");
        }
        else
        {
            _rotatingAroundPoint.SetStartAndTargetAngles(CalculateAngle(transform.position, _previousCircle.position), CalculateAngle(transform.position, _nextCircle.position));
            Debug.Log("2 oй");
        }
    }

    private float CalculateAngle(Vector3 startPoint, Vector3 finishPoint)
    {
        Vector3 direction = finishPoint - startPoint;

        // Вычисляем угол между этим вектором и направлением вперед (или другим направлением, если нужно)
        float angle = Vector3.Angle(direction.normalized, transform.right);

        // Теперь переменная 'angle' содержит угол между объектами
        if (finishPoint.x > startPoint.x)
        {
            if (finishPoint.z > startPoint.z)
            {
                Debug.Log("ПОПАЛ");
                angle -= 90;
            }
        }
        else
        {
            if (finishPoint.z > startPoint.z)
            {
                Debug.Log("ПОПАЛ 2");
                angle += 90;
            }
        }

        Debug.Log("Угол между объектами: " + angle);
        return angle;
    }
}