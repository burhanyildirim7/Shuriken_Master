using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [Header("Eskiden Kalanlar")]
    [SerializeField] private AttackKontrolScript _attackKontrolScript;
    [SerializeField] private Animator _animator;
    [Header("Girilecek Degerler")]
    [SerializeField] private Slider _karakterHealthSlider;
    [SerializeField] private Text _karakterHealthText;
    public float _karakterHealth;
    [Header("Eskiden Kalanlar")]
    public int collectibleDegeri;
    public bool xVarMi = true;
    public bool collectibleVarMi = true;

    public ChunkSpawner _chunkSpawner;

    private float _kalanHealth;

    public static bool _canCalmaAktif;

    public static bool _dusmanVar;

    public static int _incomeDegeri;

    public float _timer;

    private bool _kapandi;

    private float _gerekliSure;

    public static int _asamaSayac;

    private void Awake()
    {
        if (instance == null) instance = this;
        //else Destroy(this);
    }

    void Start()
    {
        StartingEvents();
    }

    private void FixedUpdate()
    {
        if (GameController.instance.isContinue)
        {
            _timer += Time.deltaTime;
            if (_timer > _gerekliSure && _kapandi == false)
            {
                //_timer = 0;
                _chunkSpawner = GameObject.FindGameObjectWithTag("ChunkSpawner").GetComponent<ChunkSpawner>();
                _chunkSpawner.EnemyKapat();
                _kapandi = true;
            }
            else
            {

            }


            if (_kalanHealth > _karakterHealth)
            {
                _kalanHealth = _karakterHealth;
                _karakterHealthSlider.value = _kalanHealth;
                _karakterHealthText.text = _kalanHealth.ToString();
            }
            else
            {

            }
        }
        else
        {

        }


        if (_dusmanVar)
        {
            _animator.SetBool("Attack", true);
        }
        else
        {
            _animator.SetBool("Attack", false);
        }
    }

    /// <summary>
    /// Playerin collider olaylari.. collectible, engel veya finish noktasi icin. Burasi artirilabilir.
    /// elmas icin veya baska herhangi etkilesimler icin tag ekleyerek kontrol dongusune eklenir.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "SkillObject")
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

            Debug.Log("Skill Aldi");

            if (other.GetComponent<HangiSkill>()._heal)
            {
                if (_kalanHealth < _karakterHealth)
                {
                    _kalanHealth = (int)_kalanHealth + (_karakterHealth * 0.2f);
                    _karakterHealthSlider.value = _kalanHealth;
                    _karakterHealthText.text = _kalanHealth.ToString();
                }
                else
                {

                }

            }
            else if (other.GetComponent<HangiSkill>()._canCalma)
            {
                _canCalmaAktif = true;
            }
            else if (other.GetComponent<HangiSkill>()._saldiriGucu)
            {
                _attackKontrolScript._attackDamage = _attackKontrolScript._attackDamage + (_attackKontrolScript._attackDamage * 0.2f);
            }
            else if (other.GetComponent<HangiSkill>()._saldiriHizi)
            {
                _attackKontrolScript._attackHizi = _attackKontrolScript._attackHizi - (_attackKontrolScript._attackHizi * 0.2f);
            }
            else if (other.GetComponent<HangiSkill>()._ikiliAtis)
            {
                _attackKontrolScript._ikiliAttack = true;
                _attackKontrolScript._standartAttack = false;
                _attackKontrolScript._ucluAttack = false;
                _attackKontrolScript._besliAttack = false;
            }
            else if (other.GetComponent<HangiSkill>()._ucluAtis)
            {
                _attackKontrolScript._ucluAttack = true;
                _attackKontrolScript._ikiliAttack = false;
                _attackKontrolScript._standartAttack = false;
                _attackKontrolScript._besliAttack = false;
            }
            else if (other.GetComponent<HangiSkill>()._besliAtis)
            {
                _attackKontrolScript._besliAttack = true;
                _attackKontrolScript._ucluAttack = false;
                _attackKontrolScript._ikiliAttack = false;
                _attackKontrolScript._standartAttack = false;
            }
            else
            {

            }

            Destroy(other.gameObject);

        }
        else if (other.gameObject.tag == "EnemyBullet")
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

            _kalanHealth = _kalanHealth - other.GetComponent<CanavarBulletScript>()._damage;
            _karakterHealthSlider.value = _kalanHealth;
            _karakterHealthText.text = _kalanHealth.ToString();

            if (_kalanHealth <= 0)
            {
                _karakterHealthSlider.value = 0;
                _karakterHealthText.text = 0.ToString();
                GameController.instance.isContinue = false;
                UIController.instance.ActivateLooseScreen();
            }
            else
            {

            }
        }
        else if (other.CompareTag("engel"))
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

            _kalanHealth = _kalanHealth - other.GetComponent<HasarScript>()._verecegiHasar;
            _karakterHealthSlider.value = _kalanHealth;
            _karakterHealthText.text = _kalanHealth.ToString();

            if (_kalanHealth <= 0)
            {
                _karakterHealthSlider.value = 0;
                _karakterHealthText.text = 0.ToString();
                GameController.instance.isContinue = false;
                UIController.instance.ActivateLooseScreen();
            }
            else
            {

            }

            // ENGELELRE CARPINCA YAPILACAKLAR....
            /*
            GameController.instance.SetScore(-collectibleDegeri); // ORNEK KULLANIM detaylar icin ctrl+click yapip fonksiyon aciklamasini oku
            if (GameController.instance.score < 0) // SKOR SIFIRIN ALTINA DUSTUYSE
            {
                // FAİL EVENTLERİ BURAYA YAZILACAK..
                GameController.instance.isContinue = false; // çarptığı anda oyuncunun yerinde durması ilerlememesi için
                UIController.instance.ActivateLooseScreen(); // Bu fonksiyon direk çağrılada bilir veya herhangi bir effect veya animasyon bitiminde de çağrılabilir..
                                                             // oyuncu fail durumunda bu fonksiyon çağrılacak.. 
            }
            */

        }
        else if (other.CompareTag("finish"))
        {
            // finishe collider eklenecek levellerde...
            // FINISH NOKTASINA GELINCE YAPILACAKLAR... Totalscore artırma, x işlemleri, efektler v.s. v.s.
            GameController.instance.isContinue = false;
            GameController.instance.ScoreCarp(7);  // Bu fonksiyon normalde x ler hesaplandıktan sonra çağrılacak. Parametre olarak x i alıyor. 
            // x değerine göre oyuncunun total scoreunu hesaplıyor.. x li olmayan oyunlarda parametre olarak 1 gönderilecek.
            UIController.instance.ActivateWinScreen(); // finish noktasına gelebildiyse her türlü win screen aktif edilecek.. ama burada değil..
                                                       // normal de bu kodu x ler hesaplandıktan sonra çağıracağız. Ve bu kod çağrıldığında da kazanılan puanlar animasyonlu şekilde artacak..


        }

    }



    public void CanCalmaAktif()
    {
        if (_kalanHealth < _karakterHealth)
        {
            _kalanHealth = (int)_kalanHealth + (_karakterHealth * 0.05f);
            _karakterHealthSlider.value = _kalanHealth;
            _karakterHealthText.text = _kalanHealth.ToString();
        }
        else
        {
            _kalanHealth = _karakterHealth;
            _karakterHealthSlider.value = _kalanHealth;
            _karakterHealthText.text = _kalanHealth.ToString();
        }

    }

    public void EnemyGrupResetle()
    {
        if (_asamaSayac < 4)
        {
            _kapandi = false;
            _timer = 0;
            _asamaSayac++;
            _gerekliSure = _asamaSayac * 10;
        }
        else
        {

        }

    }


    public void CanGuncelleme()
    {
        _karakterHealth = 100 + (PlayerPrefs.GetInt("StaminaLevelDegeri") * 25);
        _kalanHealth = _karakterHealth;
        _karakterHealthSlider.maxValue = _kalanHealth;
        _karakterHealthSlider.value = _kalanHealth;
        _karakterHealthText.text = _kalanHealth.ToString();
    }

    /// <summary>
    /// Bu fonksiyon her level baslarken cagrilir. 
    /// </summary>
    public void StartingEvents()
    {

        _asamaSayac = 1;
        _gerekliSure = _asamaSayac * 10;

        _canCalmaAktif = false;

        _dusmanVar = false;

        //_timer = 0;
        _kapandi = false;

        CanGuncelleme();

        _incomeDegeri = (10 + PlayerPrefs.GetInt("IncomeLevelDegeri"));

        _attackKontrolScript._attackDamage = 10 + (PlayerPrefs.GetInt("PowerLevelDegeri") * 10);
        _attackKontrolScript._attackHizi = 0.5f;

        _attackKontrolScript._ikiliAttack = false;
        _attackKontrolScript._standartAttack = true;
        _attackKontrolScript._ucluAttack = false;
        _attackKontrolScript._besliAttack = false;

        //transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        //transform.parent.transform.position = Vector3.zero;
        GameController.instance.isContinue = false;
        GameController.instance.score = 0;
        transform.position = new Vector3(0, 1, 0);
        //GetComponent<Collider>().enabled = true;

    }

}
