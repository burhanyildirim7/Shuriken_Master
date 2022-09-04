using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyYolKontrol : MonoBehaviour
{
    [SerializeField] private GameObject _kontrolEdilecekEnemy;

    void Start()
    {
        _kontrolEdilecekEnemy.SetActive(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shuriken")
        {
            _kontrolEdilecekEnemy.SetActive(false);
            GetComponent<Collider>().enabled = false;
            Destroy(other.gameObject);
        }
        else
        {

        }
    }

    public void EnemySetTrue()
    {
        _kontrolEdilecekEnemy.SetActive(true);
        GetComponent<Collider>().enabled = true;
    }
}
