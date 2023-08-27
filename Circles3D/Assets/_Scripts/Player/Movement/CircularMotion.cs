using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    [SerializeField] private Transform _centerPosition;
    [SerializeField] private float _RotationRadius;
    [SerializeField] private float _AngularSpeed;

    private float currentAngle;

    private void Awake()
    {
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        float positionX = _centerPosition.position.x + Mathf.Cos(currentAngle) * _RotationRadius;
        float positionY = _centerPosition.position.y + Mathf.Sin(currentAngle) * _RotationRadius;

        transform.position = new Vector3(positionX, positionY, 0);
        currentAngle = (Time.deltaTime * _AngularSpeed + currentAngle) % 360;
    }
}