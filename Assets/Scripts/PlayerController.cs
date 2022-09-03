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

    private float _timer;

    private bool _kapandi;

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
            if (_timer > 10 && _kapandi == false)
            {
                //_timer = 0;
                _chunkSpawner.EnemyKapat();
                _kapandi = true;
            }
            else
            {

            }
        }
        else
        {

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
            if (other.GetComponent<HangiSkill>()._heal)
            {
                _kalanHealth = _kalanHealth + (_karakterHealth * 0.2f);
                _karakterHealthSlider.value = _kalanHealth;
                _karakterHealthText.text = _kalanHealth.ToString();
            }
            else if (other.GetComponent<HangiSkill>()._canCalma)
            {
                _canCalmaAktif = true;
            }
            else if (other.GetComponent<HangiSkill>()._saldiriGücü)
            {
                _attackKontrolScript._attackDamage = _attackKontrolScript._attackDamage + (_attackKontrolScript._attackDamage * 0.2f);
            }
            else if (other.GetComponent<HangiSkill>()._saldiriHizi)
            {
                _attackKontrolScript._attackHizi = _attackKontrolScript._attackHizi + (_attackKontrolScript._attackHizi * 0.2f);
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

        }
        else if (other.CompareTag("engel"))
        {
            _kalanHealth = _kalanHealth - other.GetComponent<HasarScript>()._verecegiHasar;
            _karakterHealthSlider.value = _kalanHealth;
            _karakterHealthText.text = _kalanHealth.ToString();

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
        _kalanHealth = _kalanHealth + (_karakterHealth * 0.05f);
        _karakterHealthSlider.value = _kalanHealth;
        _karakterHealthText.text = _kalanHealth.ToString();
    }


    /// <summary>
    /// Bu fonksiyon her level baslarken cagrilir. 
    /// </summary>
    public void StartingEvents()
    {
        _kalanHealth = _karakterHealth;
        _karakterHealthSlider.maxValue = _kalanHealth;
        _karakterHealthSlider.value = _kalanHealth;
        _karakterHealthText.text = _kalanHealth.ToString();

        _canCalmaAktif = false;

        //transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        //transform.parent.transform.position = Vector3.zero;
        GameController.instance.isContinue = false;
        GameController.instance.score = 0;
        //transform.position = new Vector3(0, transform.position.y, 0);
        //GetComponent<Collider>().enabled = true;

    }

}
