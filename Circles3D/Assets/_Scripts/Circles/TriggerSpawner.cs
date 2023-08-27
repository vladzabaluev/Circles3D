using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawner : MonoBehaviour
{
    public static TriggerSpawner Instance;

    [SerializeField] private TransitionTrigger _trigger;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnTrigger(Vector3 position, Transform parent)
    {
        TransitionTrigger transitionTrigger = Instantiate(_trigger, position, Quaternion.identity, parent);
        transitionTrigger.transform.position = position;
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}