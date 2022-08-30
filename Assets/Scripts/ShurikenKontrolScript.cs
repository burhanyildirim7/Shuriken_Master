using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenKontrolScript : MonoBehaviour
{
    [Header("Elle Girilecekler")]
    [SerializeField] private float _speed;
    [Header("Baska Scriptten Gelenler")]
    public float _damage;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
}
