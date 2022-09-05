using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyYolKontrol : MonoBehaviour
{
    [SerializeField] private List<GameObject> _kontrolEdilecekEnemyList = new List<GameObject>();

    private int _acilanEnemy;

    void Start()
    {
        _acilanEnemy = Random.Range(0, _kontrolEdilecekEnemyList.Count);
        _kontrolEdilecekEnemyList[_acilanEnemy].SetActive(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shuriken")
        {
            _kontrolEdilecekEnemyList[_acilanEnemy].SetActive(false);
            GetComponent<Collider>().enabled = false;
            GameController.instance.SetScore(10 + PlayerPrefs.GetInt("level"));
            Destroy(other.gameObject);
        }
        else
        {

        }
    }

    public void EnemySetTrue()
    {
        _kontrolEdilecekEnemyList[_acilanEnemy].SetActive(true);
        GetComponent<Collider>().enabled = true;
    }
}
