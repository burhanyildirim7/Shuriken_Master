using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrupKontrol : MonoBehaviour
{
    [SerializeField] private int _gruptaKacEnemyVar;



    public void EnemySayac()
    {
        _gruptaKacEnemyVar--;

        if (_gruptaKacEnemyVar == 0)
        {
            GameObject.FindGameObjectWithTag("ChunkSpawner").GetComponent<ChunkSpawner>().EnemyAc();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().EnemyGrupResetle();
            Destroy(gameObject);
        }
        else
        {

        }
    }
}
