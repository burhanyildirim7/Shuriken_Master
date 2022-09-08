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

    void Start()
    {
        RandomEnemyAc();
    }


    public void RandomEnemyAc()
    {
        _kacTaneAcilacak = Random.Range(2, 5);

        for (int i = 0; i < _kacTaneAcilacak; i++)
        {
            _enemiesParent.transform.GetChild(RandomSayiBul()).gameObject.SetActive(true);
        }
    }

    public void RandomEnemyKapat()
    {


        for (int i = 0; i < _enemiesParent.transform.childCount; i++)
        {
            _enemiesParent.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private int RandomSayiBul()
    {
        int sayi = Random.Range(0, _enemiesParent.transform.childCount);

        if (_enemiesParent.transform.GetChild(sayi).gameObject.activeSelf)
        {
            RandomSayiBul();
        }
        else
        {
            return sayi;
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
