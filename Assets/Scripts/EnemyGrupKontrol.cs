using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGrupKontrol : MonoBehaviour
{
    [SerializeField] private GameObject _kontrolEdilecekEnemy;
    [SerializeField] private float _health;
    [SerializeField] private Slider _healthSlider;

    void Start()
    {
        _kontrolEdilecekEnemy.SetActive(true);
        _healthSlider.maxValue = _health;
        _healthSlider.value = _health;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shuriken")
        {
            Destroy(other.gameObject);
            CanKontrolEt(other.gameObject);
        }
        else
        {

        }
    }

    private void CanKontrolEt(GameObject shuriken)
    {
        _health -= shuriken.GetComponent<ShurikenKontrolScript>()._damage;


        if (_health <= 0)
        {
            _kontrolEdilecekEnemy.SetActive(false);
            GetComponent<Collider>().enabled = false;
            _healthSlider.gameObject.SetActive(false);
            gameObject.transform.parent.GetComponent<GrupKontrol>().EnemySayac();
        }
        else
        {
            _healthSlider.value = _health;
        }
    }
}
