//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//using Obi;



public class IncrementalControlScript : MonoBehaviour
{

    public static IncrementalControlScript instance;

    public List<GameObject> _karakterListesi = new List<GameObject>();

    [SerializeField] private Button _powerButton, _healthButton, _incomeButton;
    [SerializeField] Text _powerIncLevelText, _staminaIncLevelText, _incomeIncLevelText, _powerIncBedelText, _staminaIncBedelText, _incomeIncBedelText;
    [SerializeField] int _powerIncBedelDeger, _staminaIncBedelDeger, _incomeIncBedelDeger;
    [SerializeField] List<int> _incrementalBedel = new List<int>();

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
            _powerButton.interactable = true;
            _healthButton.interactable = true;
            _incomeButton.interactable = true;

        }
        else
        {
            if (PlayerPrefs.GetInt("PowerLevelDegeri") == 75)
            {
                _powerIncLevelText.text = "MAX";
                _powerIncBedelText.text = "MAX";
                _powerButton.interactable = false;

            }
            else
            {
                _powerIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("PowerLevelDegeri").ToString();
                _powerIncBedelText.text = "$" + _incrementalBedel[PlayerPrefs.GetInt("PowerCostDegeri")];
                _powerButton.interactable = true;
            }

            if (PlayerPrefs.GetInt("StaminaLevelDegeri") == 75)
            {
                _staminaIncLevelText.text = "MAX";
                _staminaIncBedelText.text = "MAX";
                _healthButton.interactable = false;

                //_incStaminaDeger = 1.6f - PlayerPrefs.GetInt("StaminaLevelDegeri") * 0.02f;
            }
            else
            {
                _staminaIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("StaminaLevelDegeri").ToString();
                _staminaIncBedelText.text = "$" + _incrementalBedel[PlayerPrefs.GetInt("StaminaCostDegeri")];
                _healthButton.interactable = true;

                //_incStaminaDeger = 1.6f - PlayerPrefs.GetInt("StaminaLevelDegeri") * 0.02f;
            }

            if (PlayerPrefs.GetInt("IncomeLevelDegeri") == 75)
            {
                _incomeIncLevelText.text = "MAX";
                _incomeIncBedelText.text = "MAX";
                _incomeButton.interactable = false;
            }
            else
            {
                _incomeIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("IncomeLevelDegeri").ToString();
                _incomeIncBedelText.text = "$" + _incrementalBedel[PlayerPrefs.GetInt("IncomeCostDegeri")];
                _incomeButton.interactable = true;
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

        ButonKontrol();

        //PlayerPrefs.SetInt("totalScore", 99999);

        Application.targetFrameRate = 60;

    }

    private void BaslangicButonAyarlari()
    {
        if (PlayerPrefs.GetInt("totalScore") < _incrementalBedel[PlayerPrefs.GetInt("PowerCostDegeri")])
        {
            _powerButton.interactable = false;
        }
        else
        {
            _powerButton.interactable = true;
        }

        if (PlayerPrefs.GetInt("totalScore") < _incrementalBedel[PlayerPrefs.GetInt("StaminaCostDegeri")])
        {
            _healthButton.interactable = false;
        }
        else
        {
            _healthButton.interactable = true;
        }

        if (PlayerPrefs.GetInt("totalScore") < _incrementalBedel[PlayerPrefs.GetInt("IncomeCostDegeri")])
        {
            _incomeButton.interactable = false;
        }
        else
        {
            _incomeButton.interactable = true;
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
                _powerButton.interactable = false;
            }
            else
            {
                _powerIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("PowerLevelDegeri").ToString();
                //_powerButonPasifPaneli.SetActive(false);


            }
            if (PlayerPrefs.GetInt("KarakterDegisimSayaci") == 8)
            {
                PlayerPrefs.SetInt("KarakterDegisimSayaci", 1);
                KarakterDegis();
            }




        }
        else
        {
            _powerButton.interactable = false;
        }

        if (PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("PowerCostDegeri")])
        {
            _powerButton.interactable = true;
        }
        else
        {
            _powerButton.interactable = false;
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
                _healthButton.interactable = false;
            }
            else
            {
                _staminaIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("StaminaLevelDegeri").ToString();
                //_staminaButonPasifPaneli.SetActive(false);


            }


        }
        else
        {
            _healthButton.interactable = false;
        }

        if (PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("StaminaCostDegeri")])
        {
            _healthButton.interactable = true;
        }
        else
        {
            _healthButton.interactable = false;
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
                _incomeButton.interactable = false;
            }
            else
            {
                _incomeIncLevelText.text = "LEVEL " + PlayerPrefs.GetInt("IncomeLevelDegeri").ToString();
                //_incomeButonPasifPaneli.SetActive(false);


            }


        }
        else
        {
            _incomeButton.interactable = false;
        }

        if (PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("IncomeCostDegeri")])
        {
            _incomeButton.interactable = true;
        }
        else
        {
            _incomeButton.interactable = false;
        }
    }



    public void KarakterDegis()
    {

        _karakterListesi[PlayerPrefs.GetInt("KarakterSirasi")].SetActive(false);
        PlayerPrefs.SetInt("KarakterSirasi", PlayerPrefs.GetInt("KarakterSirasi") + 1);
        _karakterListesi[PlayerPrefs.GetInt("KarakterSirasi")].SetActive(true);

    }

    private void ButonKontrol()
    {
        if (PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("PowerCostDegeri")])
        {
            _powerButton.interactable = true;
        }
        else
        {
            _powerButton.interactable = false;
        }

        if (PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("StaminaCostDegeri")])
        {
            _healthButton.interactable = true;
        }
        else
        {
            _healthButton.interactable = false;
        }

        if (PlayerPrefs.GetInt("totalScore") > _incrementalBedel[PlayerPrefs.GetInt("IncomeCostDegeri")])
        {
            _incomeButton.interactable = true;
        }
        else
        {
            _incomeButton.interactable = false;
        }
    }



}
