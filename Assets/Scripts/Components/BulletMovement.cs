using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    // Поле со ссылкой на твердое тело пули.
    [SerializeField] private Rigidbody _rigidbody;
    // Поле со ссылкой на скорость пули.
    [SerializeField] public float speed = 5;

    /// <summary>
    /// Запуск объекта.
    /// </summary>
    private void Start()
    {
        _rigidbody.velocity = transform.forward * speed;
    }
}
