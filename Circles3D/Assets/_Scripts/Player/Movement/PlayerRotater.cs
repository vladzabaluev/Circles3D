using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotater : MonoBehaviour
{
    [SerializeField] private RotationTarget _rotationTarget;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        transform.rotation = _rotationTarget.transform.rotation;
    }

    public void SetRotationTarget(RotationTarget rotationTarget)
    {
        _rotationTarget = rotationTarget;
    }
}