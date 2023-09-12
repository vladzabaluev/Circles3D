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
            _rotatingAroundPoint.SetAngles(CalculateAngles(transform.position, transform.position), CalculateAngles(transform.position, _nextCircle.position));
            Debug.Log("1 ый");
            //test(transform, _nextCircle);
        }
        else if (_nextCircle == null)
        {
            _rotatingAroundPoint.SetAngles(CalculateAngles(transform.position, _previousCircle.position), CalculateAngles(transform.position, transform.position));
            Debug.Log("3 uй");

            //test(transform, _previousCircle);
            //test()
        }
        else
        {
            _rotatingAroundPoint.SetAngles(CalculateAngles(transform.position, _previousCircle.position), CalculateAngles(transform.position, _nextCircle.position));
            Debug.Log("2 oй");
            //test(transform, _previousCircle);
            //test(transform, _nextCircle);
        }
    }

    private float CalculateAngles(Vector3 startPoint, Vector3 finishPoint)
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
        //if (finishPoint.x > startPoint.x)
        //{
        //    angle -= 90;
        //}
        Debug.Log("Угол между объектами: " + angle);
        return angle;
        //  Debug.Log("Угол между объектами: " + Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg);
        //  return Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
    }

    private void test(Transform object1, Transform object2)
    {
        Vector3 direction = object2.position - object1.position;
        Vector3 aaa = new Vector3(object2.position.x, object1.position.y, object1.position.z);

        // Вычисляем угол между этим вектором и направлением вперед (или другим направлением, если нужно)
        float angle = Vector3.Angle(direction.normalized, transform.right);

        // Теперь переменная 'angle' содержит угол между объектами
        Debug.Log("Угол между объектами: " + angle);
    }
}