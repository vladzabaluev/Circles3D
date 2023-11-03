using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotationTarget : MonoBehaviour
{
    [SerializeField] private SelfRotating parentCircle;

    private void Awake()
    {
        if (parentCircle.IsClockwise)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }
}