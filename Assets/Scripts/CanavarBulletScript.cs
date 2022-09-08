using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanavarBulletScript : MonoBehaviour
{
    [Header("Elle Girilecekler")]
    [SerializeField] private float _speed;
    [Header("Otomatik Belirlenenler")]
    public float _damage;

    private void Start()
    {
        _damage = 25 + (LevelController.instance.totalLevelNo * 5);
        Destroy(gameObject, 10f);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }
}
