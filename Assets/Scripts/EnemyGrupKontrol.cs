using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGrupKontrol : MonoBehaviour
{
    [SerializeField] private GrupKontrol _grupKontrol;
    [SerializeField] private GameObject _kontrolEdilecekEnemy;
    [SerializeField] private float _health;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private ParticleSystem _vurulmaEfekti;
    [SerializeField] private ParticleSystem _olmeEfekti;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _atisHizi;

    private float _timer;

    void Start()
    {
        _kontrolEdilecekEnemy.SetActive(true);
        _health = 100 + (50 * LevelController.instance.totalLevelNo);
        _healthSlider.maxValue = _health;
        _healthSlider.value = _health;
        _timer = 0;
    }

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;

        if (_health > 0)
        {
            if (_timer > _atisHizi)
            {
                Instantiate(_bullet, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
                _timer = 0;
            }
            else
            {

            }
        }
        else
        {

        }

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
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
            _olmeEfekti.Play();
            _kontrolEdilecekEnemy.SetActive(false);
            GetComponent<Collider>().enabled = false;
            _healthSlider.gameObject.SetActive(false);
            _grupKontrol.EnemySayac();

            GameController.instance.SetScore(PlayerController._incomeDegeri + PlayerPrefs.GetInt("level"));

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
            _healthSlider.value = _health;
            _vurulmaEfekti.Play();
        }
    }
}
