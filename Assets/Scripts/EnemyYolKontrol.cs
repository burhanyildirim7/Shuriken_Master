using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyYolKontrol : MonoBehaviour
{
    [SerializeField] private List<GameObject> _kontrolEdilecekEnemyList = new List<GameObject>();
    [SerializeField] private ParticleSystem _olmeEfekti;

    private int _acilanEnemy;



    void Start()
    {

        // EnemySetTrue();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shuriken")
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
            _olmeEfekti.Play();
            _kontrolEdilecekEnemyList[_acilanEnemy].SetActive(false);
            GetComponent<Collider>().enabled = false;
            GameController.instance.SetScore(PlayerController._incomeDegeri + PlayerPrefs.GetInt("level"));
            Destroy(other.gameObject);

            if (PlayerController._canCalmaAktif)
            {
                PlayerController.instance.CanCalmaAktif();
            }
            else
            {

            }
        }
        else
        {

        }
    }

    public void EnemySetTrue()
    {
        _acilanEnemy = Random.Range(0, _kontrolEdilecekEnemyList.Count);
        _kontrolEdilecekEnemyList[_acilanEnemy].SetActive(true);
        GetComponent<Collider>().enabled = true;
    }

    public void EnemySetFalse()
    {

        _kontrolEdilecekEnemyList[_acilanEnemy].SetActive(false);
        GetComponent<Collider>().enabled = false;
    }
}
