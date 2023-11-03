using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Transform _centerCircle;

    private void Start()
    {
        float angleInDegrees = Random.Range(0f, 360f); // Случайный угол от 0 до 360 градусов
        float radius = Random.Range(_centerCircle.localScale.x / 5, _centerCircle.localScale.x / 2);
        float x = _centerCircle.position.x + radius * Mathf.Cos(Mathf.Deg2Rad * angleInDegrees); // Преобразование градусов в радианы
        float z = _centerCircle.position.z + radius * Mathf.Sin(Mathf.Deg2Rad * angleInDegrees);
        Vector3 newPosition = new Vector3(x, transform.position.y, z);
        transform.position = newPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PointToPointMovement player))
        {
            if (player.IsVulnerable)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}