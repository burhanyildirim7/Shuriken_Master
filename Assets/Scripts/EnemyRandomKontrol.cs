using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomKontrol : MonoBehaviour
{
    [SerializeField] private GameObject _enemiesParent;
    public GameObject _skillKapiSpawnPoint;

    //[SerializeField] private int _kacTaneEnemyVar;

    private int _kacTaneAcilacak;

    private int _acilacakEnemy;

    private int _sayi;

    void Start()
    {
        // RandomEnemyAc();
    }


    public void RandomEnemyAc()
    {
        _kacTaneAcilacak = Random.Range(4, 8);

        for (int i = 0; i < _kacTaneAcilacak; i++)
        {
            _enemiesParent.transform.GetChild(RandomSayiBul()).gameObject.SetActive(true);
            _enemiesParent.transform.GetChild(_sayi).gameObject.GetComponent<EnemyYolKontrol>().EnemySetTrue();

        }
    }

    public void RandomEnemyKapat()
    {


        for (int i = 0; i < _enemiesParent.transform.childCount; i++)
        {
            _enemiesParent.transform.GetChild(i).gameObject.SetActive(false);
            _enemiesParent.transform.GetChild(i).gameObject.GetComponent<EnemyYolKontrol>().EnemySetFalse();

        }
    }

    private int RandomSayiBul()
    {
        _sayi = Random.Range(0, _enemiesParent.transform.childCount);

        if (_enemiesParent.transform.GetChild(_sayi).gameObject.activeSelf)
        {
            RandomSayiBul();
        }
        else
        {
            return _sayi;
        }
        return 5;
    }

    public void EnemyleriAc()
    {
        for (int i = 0; i < _enemiesParent.transform.childCount; i++)
        {
            _enemiesParent.transform.GetChild(i).gameObject.GetComponent<EnemyYolKontrol>().EnemySetTrue();
        }
    }
}
