using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotationTarget : MonoBehaviour
{
    [SerializeField] private SelfRotating parentCircle;

    private void Start()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerRotater playerRotater))
        {
            Debug.Log("ириририр");

            playerRotater.SetRotationTarget(this);
        }
    }
}