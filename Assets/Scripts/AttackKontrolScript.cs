using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackKontrolScript : MonoBehaviour
{
    [Header("Atilacak Shuriken")]
    public List<GameObject> _shurikenObjectList = new List<GameObject>();
    [Header("Attack Turleri")]
    public bool _standartAttack;
    public bool _ikiliAttack;
    public bool _ucluAttack;
    public bool _besliAttack;

    [Header("Attack Degiskenleri")]
    public float _attackHizi;
    public float _attackDamage;

    [Header("Attack Spawn Noktalari")]
    public GameObject _standartAttackPoint;
    public List<GameObject> _ikiliAttackPoints = new List<GameObject>();

    private float _timer;

    private bool _attackAktif;

    void Start()
    {
        _timer = 0;
        _attackAktif = false;
        _standartAttack = true;

        _attackAktif = true;
    }


    void FixedUpdate()
    {
        if (GameController.instance.isContinue)
        {
            _timer += Time.deltaTime;

            if (_attackAktif)
            {
                if (_timer > _attackHizi)
                {
                    _timer = 0;

                    _attackDamage = 10 + (PlayerPrefs.GetInt("PowerLevelDegeri") * 10);

                    if (_standartAttack)
                    {
                        GameObject shuriken = Instantiate(_shurikenObjectList[PlayerPrefs.GetInt("KarakterSirasi")], _standartAttackPoint.transform.position, Quaternion.identity);
                        shuriken.GetComponent<ShurikenKontrolScript>()._damage = _attackDamage;
                    }
                    else if (_ikiliAttack)
                    {
                        GameObject shuriken1 = Instantiate(_shurikenObjectList[PlayerPrefs.GetInt("KarakterSirasi")], _ikiliAttackPoints[0].transform.position, Quaternion.identity);
                        GameObject shuriken2 = Instantiate(_shurikenObjectList[PlayerPrefs.GetInt("KarakterSirasi")], _ikiliAttackPoints[1].transform.position, Quaternion.identity);
                        shuriken1.GetComponent<ShurikenKontrolScript>()._damage = _attackDamage;
                        shuriken2.GetComponent<ShurikenKontrolScript>()._damage = _attackDamage;
                    }
                    else if (_ucluAttack)
                    {
                        GameObject shuriken1 = Instantiate(_shurikenObjectList[PlayerPrefs.GetInt("KarakterSirasi")], _standartAttackPoint.transform.position, Quaternion.identity);
                        GameObject shuriken2 = Instantiate(_shurikenObjectList[PlayerPrefs.GetInt("KarakterSirasi")], _standartAttackPoint.transform.position, Quaternion.Euler(new Vector3(0, 10, 0)));
                        GameObject shuriken3 = Instantiate(_shurikenObjectList[PlayerPrefs.GetInt("KarakterSirasi")], _standartAttackPoint.transform.position, Quaternion.Euler(new Vector3(0, -10, 0)));
                        shuriken1.GetComponent<ShurikenKontrolScript>()._damage = _attackDamage;
                        shuriken2.GetComponent<ShurikenKontrolScript>()._damage = _attackDamage;
                        shuriken3.GetComponent<ShurikenKontrolScript>()._damage = _attackDamage;
                    }
                    else if (_besliAttack)
                    {
                        GameObject shuriken1 = Instantiate(_shurikenObjectList[PlayerPrefs.GetInt("KarakterSirasi")], _standartAttackPoint.transform.position, Quaternion.identity);
                        GameObject shuriken2 = Instantiate(_shurikenObjectList[PlayerPrefs.GetInt("KarakterSirasi")], _standartAttackPoint.transform.position, Quaternion.Euler(new Vector3(0, 10, 0)));
                        GameObject shuriken3 = Instantiate(_shurikenObjectList[PlayerPrefs.GetInt("KarakterSirasi")], _standartAttackPoint.transform.position, Quaternion.Euler(new Vector3(0, -10, 0)));
                        GameObject shuriken4 = Instantiate(_shurikenObjectList[PlayerPrefs.GetInt("KarakterSirasi")], _standartAttackPoint.transform.position, Quaternion.Euler(new Vector3(0, 20, 0)));
                        GameObject shuriken5 = Instantiate(_shurikenObjectList[PlayerPrefs.GetInt("KarakterSirasi")], _standartAttackPoint.transform.position, Quaternion.Euler(new Vector3(0, -20, 0)));
                        shuriken1.GetComponent<ShurikenKontrolScript>()._damage = _attackDamage;
                        shuriken2.GetComponent<ShurikenKontrolScript>()._damage = _attackDamage;
                        shuriken3.GetComponent<ShurikenKontrolScript>()._damage = _attackDamage;
                        shuriken4.GetComponent<ShurikenKontrolScript>()._damage = _attackDamage;
                        shuriken5.GetComponent<ShurikenKontrolScript>()._damage = _attackDamage;
                    }
                    else
                    {

                    }


                }
                else
                {

                }
            }
            else
            {

            }
        }
        else
        {

        }
    }
}
