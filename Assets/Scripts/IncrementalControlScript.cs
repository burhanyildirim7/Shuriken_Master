//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//using Obi;



public class IncrementalControlScript : MonoBehaviour
{

    public static IncrementalControlScript instance;

    public List<GameObject> _sagSutunListesi = new List<GameObject>(), _solSutunListesi = new List<GameObject>(), _karakterListesi = new List<GameObject>();
    [SerializeField] GameObject _yikiciObj, _powerButonPasifPaneli, _staminaButonPasifPaneli, _incomeButonPasifPaneli;
    [SerializeField] Text _powerIncLevelText, _staminaIncLevelText, _incomeIncLevelText, _powerIncBedelText, _staminaIncBedelText, _incomeIncBedelText;
    [SerializeField] int _powerIncBedelDeger, _staminaIncBedelDeger, _incomeIncBedelDeger;
    [SerializeField] List<int> _incrementalBedel = new List<int>();

    [SerializeField] private Slider _staminaSlider;
    [SerializeField] private List<GameObject> _emojiList = new List<GameObject>();
    [SerializeField] private List<GameObject> _karakterEmojiList = new List<GameObject>();
    [SerializeField] private List<ParticleSystem> _efektList = new List<ParticleSystem>();

    [SerializeField] private Slider _ustGucSlider;
    [SerializeField] private Slider _altGucSlider;

    [SerializeField] private GameObject _coinObjesi;
    [SerializeField] private GameObject _coinParent;

    [SerializeField] private List<Vector3> _cameraPoziyonlari = new List<Vector3>();

    private Vector3 _cameraOffset;

    private int _karakteriGeriCekenKuvvetSayaci;

    public bool _yikim;

    private float _staminaDeger;

    private float _time;

    private int _tiklamaSayac;

    private bool _tamamlandi;

    private bool _yik;

    private float _incStaminaDeger;

    private float _incParaKazanma;

    private float _gucDegeri;
    private float _timeDegeri;

    private bool _coinSpawn;

    private float _staminaDusurmeTimer;

    private void Awake()
    {
        if (instance == null) instance = this;
        //else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("ButtonlarIcinIlkSefer") == 0)
        {
            PlayerPrefs.SetInt("SutunDegisimSayaci", 0);
            PlayerPrefs.SetInt("PowerLevelDegeri", 1);
            PlayerPrefs.SetInt("StaminaLevelDegeri", 1);
            PlayerPrefs.SetInt("IncomeLevelDegeri", 1);

            //_powerIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("PowerLevelDegeri").ToString();
            //_staminaIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("StaminaLevelDegeri").ToString();
            //_incomeIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("IncomeLevelDegeri").ToString();

            _powerIncLevelText.text = "LEVEL 1";
            _staminaIncLevelText.text = "LEVEL 1";
            _incomeIncLevelText.text = "LEVEL 1";

            _powerIncBedelText.text = "$" + _powerIncBedelDeger.ToString();
            _staminaIncBedelText.text = "$" + _staminaIncBedelDeger.ToString();
            _incomeIncBedelText.text = "$" + _incomeIncBedelDeger.ToString();

            PlayerPrefs.SetInt("ButtonlarIcinIlkSefer", 1);
            PlayerPrefs.SetInt("KarakterDegisimSayaci", 1);
            _powerButonPasifPaneli.SetActive(false);
            _staminaButonPasifPaneli.SetActive(false);
            _incomeButonPasifPaneli.SetActive(false);
        }
        else
        {
            if (PlayerPrefs.GetInt("PowerLevelDegeri") == 75)
            {
                _powerIncLevelText.text = "MAX";
                _powerIncBedelText.text = "MAX";
                _powerButonPasifPaneli.SetActive(true);

            }
            else
            {
                _powerIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("PowerLevelDegeri").ToString();
                _powerIncBedelText.text = "$" + _incrementalBedel[PlayerPrefs.GetInt("PowerCostDegeri")];
                _powerButonPasifPaneli.SetActive(false);
            }

            if (PlayerPrefs.GetInt("StaminaLevelDegeri") == 75)
            {
                _staminaIncLevelText.text = "MAX";
                _staminaIncBedelText.text = "MAX";
                _staminaButonPasifPaneli.SetActive(true);

                //_incStaminaDeger = 1.6f - PlayerPrefs.GetInt("StaminaLevelDegeri") * 0.02f;
            }
            else
            {
                _staminaIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("StaminaLevelDegeri").ToString();
                _staminaIncBedelText.text = "$" + _incrementalBedel[PlayerPrefs.GetInt("StaminaCostDegeri")];
                _staminaButonPasifPaneli.SetActive(false);

                //_incStaminaDeger = 1.6f - PlayerPrefs.GetInt("StaminaLevelDegeri") * 0.02f;
            }

            if (PlayerPrefs.GetInt("IncomeLevelDegeri") == 75)
            {
                _incomeIncLevelText.text = "MAX";
                _incomeIncBedelText.text = "MAX";
                _incomeButonPasifPaneli.SetActive(true);
            }
            else
            {
                _incomeIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("IncomeLevelDegeri").ToString();
                _incomeIncBedelText.text = "$" + _incrementalBedel[PlayerPrefs.GetInt("IncomeCostDegeri")];
                _incomeButonPasifPaneli.SetActive(false);
            }
        }
        for (int i = 0; i < _karakterListesi.Count; i++)
        {
            if (i == PlayerPrefs.GetInt("KarakterSirasi"))
            {
                _karakterListesi[i].SetActive(true);
            }
            else
            {
                _karakterListesi[i].SetActive(false);
            }

        }

        BaslangicButonAyarlari();

        _time = 0;
        Animator _karakterAnimation = _karakterListesi[PlayerPrefs.GetInt("KarakterSirasi")].GetComponent<Animator>();
        _karakterAnimation.SetFloat("Time", _time);

        _ustGucSlider.value = 0;
        _altGucSlider.value = 0;

        _yikim = false;
        _tamamlandi = false;
        _yik = false;

        _sagSutunListesi[PlayerPrefs.GetInt("SutunSirasi")].SetActive(true);
        _solSutunListesi[PlayerPrefs.GetInt("SutunSirasi")].SetActive(true);

        _tiklamaSayac = 0;

        if (PlayerPrefs.GetInt("SutunSirasi") < 10)
        {
            _cameraOffset = _cameraPoziyonlari[0];
            Debug.Log(_cameraOffset);
        }
        else if (PlayerPrefs.GetInt("SutunSirasi") >= 10 && PlayerPrefs.GetInt("SutunSirasi") < 17)
        {
            _cameraOffset = new Vector3(_cameraPoziyonlari[0].x, _cameraPoziyonlari[0].y, _cameraPoziyonlari[0].z + 10);
            Debug.Log(_cameraOffset);
        }
        else if (PlayerPrefs.GetInt("SutunSirasi") >= 17)
        {
            _cameraOffset = new Vector3(_cameraPoziyonlari[0].x, _cameraPoziyonlari[0].y, _cameraPoziyonlari[0].z + 20);
            Debug.Log(_cameraOffset);
        }
        else
        {
            Debug.Log(_cameraOffset);
        }


        _incStaminaDeger = 1.6f - PlayerPrefs.GetInt("StaminaLevelDegeri") * 0.02f;

        _incParaKazanma = 1 + PlayerPrefs.GetInt("IncomeLevelDegeri") * 2;

        _ustGucSlider.maxValue = 20 + PlayerPrefs.GetInt("SutunSirasi") * 10;
        _altGucSlider.maxValue = 20 + PlayerPrefs.GetInt("SutunSirasi") * 10;

        if (PlayerPrefs.GetInt("SutunSirasi") < 1)
        {
            _gucDegeri = 3 + PlayerPrefs.GetInt("PowerLevelDegeri") / 2;
        }
        else if (PlayerPrefs.GetInt("SutunSirasi") == 1)
        {
            _gucDegeri = 2 + PlayerPrefs.GetInt("PowerLevelDegeri") / 2;
        }
        else
        {
            _gucDegeri = 1 + PlayerPrefs.GetInt("PowerLevelDegeri") / 2;
        }


        _timeDegeri = _gucDegeri / _ustGucSlider.maxValue;

        ButonKontrol();

        //PlayerPrefs.SetInt("totalScore", 99999);

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

    }

    private void BaslangicButonAyarlari()
    {
        if (PlayerPrefs.GetInt("totalScore") < _incrementalBedel[PlayerPrefs.GetInt("PowerCostDegeri")])
        {
            _powerButonPasifPaneli.SetActive(true);
        }
        else
        {
            _powerButonPasifPaneli.SetActive(false);
        }

        if (PlayerPrefs.GetInt("totalScore") < _incrementalBedel[PlayerPrefs.GetInt("StaminaCostDegeri")])
        {
            _staminaButonPasifPaneli.SetActive(true);
        }
        else
        {
            _staminaButonPasifPaneli.SetActive(false);
        }

        if (PlayerPrefs.GetInt("totalScore") < _incrementalBedel[PlayerPrefs.GetInt("IncomeCostDegeri")])
        {
            _incomeButonPasifPaneli.SetActive(true);
        }
        else
        {
            _incomeButonPasifPaneli.SetActive(false);
        }

    }


    public void PowerButonu()
    {
        if (PlayerPrefs.GetInt("PowerLevelDegeri") < 75 && PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("PowerCostDegeri")])
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

            PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") - _incrementalBedel[PlayerPrefs.GetInt("PowerCostDegeri")]);
            PlayerPrefs.SetInt("PowerLevelDegeri", PlayerPrefs.GetInt("PowerLevelDegeri") + 1);
            PlayerPrefs.SetInt("PowerCostDegeri", PlayerPrefs.GetInt("PowerCostDegeri") + 1);
            PlayerPrefs.SetInt("KarakterDegisimSayaci", PlayerPrefs.GetInt("KarakterDegisimSayaci") + 1);
            _powerIncBedelText.text = "$" + _incrementalBedel[PlayerPrefs.GetInt("PowerCostDegeri")];
            UIController.instance.SetTapToStartScoreText();


            BaslangicButonAyarlari();

            if (PlayerPrefs.GetInt("PowerLevelDegeri") == 75)
            {
                _powerIncLevelText.text = "MAX";
                _powerIncBedelText.text = "MAX";
                _powerButonPasifPaneli.SetActive(true);
            }
            else
            {
                _powerIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("PowerLevelDegeri").ToString();
                //_powerButonPasifPaneli.SetActive(false);


            }
            if (PlayerPrefs.GetInt("KarakterDegisimSayaci") == 6)
            {
                PlayerPrefs.SetInt("KarakterDegisimSayaci", 1);
                KarakterDegis();
            }

            _gucDegeri = 1 + PlayerPrefs.GetInt("PowerLevelDegeri") / 2;

            _timeDegeri = _gucDegeri / _ustGucSlider.maxValue;
        }
        else
        {
            _powerButonPasifPaneli.SetActive(true);
        }

        if (PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("PowerCostDegeri")])
        {
            _powerButonPasifPaneli.SetActive(false);
        }
        else
        {
            _powerButonPasifPaneli.SetActive(true);
        }
    }

    public void StaminaButonu()
    {
        if (PlayerPrefs.GetInt("StaminaLevelDegeri") < 75 && PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("StaminaCostDegeri")])
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

            PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") - _incrementalBedel[PlayerPrefs.GetInt("StaminaCostDegeri")]);
            PlayerPrefs.SetInt("StaminaLevelDegeri", PlayerPrefs.GetInt("StaminaLevelDegeri") + 1);
            PlayerPrefs.SetInt("StaminaCostDegeri", PlayerPrefs.GetInt("StaminaCostDegeri") + 1);
            _staminaIncBedelText.text = "$" + _incrementalBedel[PlayerPrefs.GetInt("StaminaCostDegeri")];
            UIController.instance.SetTapToStartScoreText();


            BaslangicButonAyarlari();

            if (PlayerPrefs.GetInt("StaminaLevelDegeri") == 75)
            {
                _staminaIncLevelText.text = "MAX";
                _staminaIncBedelText.text = "MAX";
                _staminaButonPasifPaneli.SetActive(true);
            }
            else
            {
                _staminaIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("StaminaLevelDegeri").ToString();
                //_staminaButonPasifPaneli.SetActive(false);


            }

            _incStaminaDeger = 1.5f - PlayerPrefs.GetInt("StaminaLevelDegeri") * 0.02f;
        }
        else
        {
            _staminaButonPasifPaneli.SetActive(true);
        }

        if (PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("StaminaCostDegeri")])
        {
            _staminaButonPasifPaneli.SetActive(false);
        }
        else
        {
            _staminaButonPasifPaneli.SetActive(true);
        }


    }

    public void IncomeButonu()
    {
        if (PlayerPrefs.GetInt("IncomeLevelDegeri") < 75 && PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("IncomeCostDegeri")])
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

            PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") - _incrementalBedel[PlayerPrefs.GetInt("IncomeCostDegeri")]);
            PlayerPrefs.SetInt("IncomeLevelDegeri", PlayerPrefs.GetInt("IncomeLevelDegeri") + 1);
            PlayerPrefs.SetInt("IncomeCostDegeri", PlayerPrefs.GetInt("IncomeCostDegeri") + 1);
            _incomeIncBedelText.text = "$" + _incrementalBedel[PlayerPrefs.GetInt("IncomeCostDegeri")];
            UIController.instance.SetTapToStartScoreText();


            BaslangicButonAyarlari();

            if (PlayerPrefs.GetInt("IncomeLevelDegeri") == 75)
            {
                _incomeIncLevelText.text = "MAX";
                _incomeIncBedelText.text = "MAX";
                _incomeButonPasifPaneli.SetActive(true);
            }
            else
            {
                _incomeIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("IncomeLevelDegeri").ToString();
                //_incomeButonPasifPaneli.SetActive(false);


            }

            _incParaKazanma = 1 + PlayerPrefs.GetInt("IncomeLevelDegeri") * 2;
        }
        else
        {
            _incomeButonPasifPaneli.SetActive(true);
        }

        if (PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("IncomeCostDegeri")])
        {
            _incomeButonPasifPaneli.SetActive(false);
        }
        else
        {
            _incomeButonPasifPaneli.SetActive(true);
        }
    }

    private IEnumerator SutunDegis()
    {

        _sagSutunListesi[PlayerPrefs.GetInt("SutunSirasi")].SetActive(false);
        _solSutunListesi[PlayerPrefs.GetInt("SutunSirasi")].SetActive(false);
        PlayerPrefs.SetInt("SutunSirasi", PlayerPrefs.GetInt("SutunSirasi") + 1);
        _sagSutunListesi[PlayerPrefs.GetInt("SutunSirasi")].SetActive(true);
        _solSutunListesi[PlayerPrefs.GetInt("SutunSirasi")].SetActive(true);

        yield return new WaitForSeconds(0.1f);

    }

    public void KarakterDegis()
    {

        _karakterListesi[PlayerPrefs.GetInt("KarakterSirasi")].SetActive(false);
        PlayerPrefs.SetInt("KarakterSirasi", PlayerPrefs.GetInt("KarakterSirasi") + 1);
        _karakterListesi[PlayerPrefs.GetInt("KarakterSirasi")].SetActive(true);

        _efektList[2].Play();

    }

    private void ButonKontrol()
    {
        if (PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("PowerCostDegeri")])
        {
            _powerButonPasifPaneli.SetActive(false);
        }
        else
        {
            _powerButonPasifPaneli.SetActive(true);
        }

        if (PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("StaminaCostDegeri")])
        {
            _staminaButonPasifPaneli.SetActive(false);
        }
        else
        {
            _staminaButonPasifPaneli.SetActive(true);
        }

        if (PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("IncomeCostDegeri")])
        {
            _incomeButonPasifPaneli.SetActive(false);
        }
        else
        {
            _incomeButonPasifPaneli.SetActive(true);
        }
    }



}
