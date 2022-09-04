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
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>()._asamaSayac < 3)
            {
                GameObject.FindGameObjectWithTag("ChunkSpawner").GetComponent<ChunkSpawner>().EnemyAc();
            }
            else
            {

            }

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().EnemyGrupResetle();
            Destroy(gameObject);
        }
        else
        {

        }
    }
}
