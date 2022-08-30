using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmazingAssets;
using AmazingAssets.CurvedWorld;
using UnityEngine.UI;



public class CurvedKontrolScript : MonoBehaviour
{
    [SerializeField] private Slider _curvedSlider;
    [SerializeField] private float _baslangicCurveDegeri;
    [SerializeField] private float _alacagiAciMin, _alacagiAciMax;
    [SerializeField] private float _acininDegisecegiSaniyeMin, _acininDegisecegiSaniyeMax;

    private float _timer;
    private float _degisimZamani;
    private float _degisecegiAci;

    void Start()
    {
        _timer = 0;
        _degisimZamani = Random.Range(_acininDegisecegiSaniyeMin, _acininDegisecegiSaniyeMax);
        _curvedSlider.minValue = _alacagiAciMin;
        _curvedSlider.maxValue = _alacagiAciMax;
        _curvedSlider.value = _baslangicCurveDegeri;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameController.instance.isContinue)
        {
            _timer += Time.deltaTime;

            if (_timer > _degisimZamani)
            {
                _timer = 0;
                _degisimZamani = Random.Range(_acininDegisecegiSaniyeMin, _acininDegisecegiSaniyeMax);
                StartCoroutine(AciDegistir());
            }
            else
            {

            }
        }
        else
        {

        }
    }

    IEnumerator AciDegistir()
    {

        _degisecegiAci = Random.Range(_alacagiAciMin, _alacagiAciMax);

        while (_curvedSlider.value != _degisecegiAci)
        {
            if (_curvedSlider.value == _degisecegiAci)
            {

                break;

            }
            else
            {
                yield return new WaitForSeconds(0.001f);

                if (_curvedSlider.value < _degisecegiAci)
                {
                    _curvedSlider.value += 0.001f;
                }
                else
                {
                    _curvedSlider.value -= 0.001f;
                }
            }

            yield return null;

        }
    }
}
