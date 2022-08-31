using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomKontrol : MonoBehaviour
{
    [SerializeField] private GameObject _enemiesParent;

    //[SerializeField] private int _kacTaneEnemyVar;

    private int _kacTaneAcilacak;

    private int _acilacakEnemy;

    void Start()
    {
        RandomEnemyAc();
    }


    public void RandomEnemyAc()
    {
        _kacTaneAcilacak = Random.Range(3, 8);

        for (int i = 0; i < _kacTaneAcilacak; i++)
        {
            _enemiesParent.transform.GetChild(RandomSayiBul()).gameObject.SetActive(true);
        }
    }

    private int RandomSayiBul()
    {
        int sayi = Random.Range(0, _enemiesParent.transform.childCount + 1);

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
}
