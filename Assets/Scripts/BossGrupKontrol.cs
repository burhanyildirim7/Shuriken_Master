using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGrupKontrol : MonoBehaviour
{
    [SerializeField] private int _gruptaKacEnemyVar;



    public void EnemySayac()
    {
        _gruptaKacEnemyVar--;

        if (_gruptaKacEnemyVar == 0)
        {
            GameController.instance.isContinue = false;
            GameController.instance.ScoreCarp(1);
            Invoke("WinScreenAc", 3);

        }
        else
        {

        }
    }


    private void WinScreenAc()
    {
        UIController.instance.ActivateWinScreen();
        Destroy(gameObject);
    }
}
