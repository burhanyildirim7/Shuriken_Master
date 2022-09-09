using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangiSkill : MonoBehaviour
{
    public bool _heal;
    public bool _canCalma;
    public bool _saldiriGucu;
    public bool _saldiriHizi;
    public bool _ikiliAtis;
    public bool _ucluAtis;
    public bool _besliAtis;

    public GameObject _healCanvas;
    public GameObject _canAlmaCanvas;
    public GameObject _saldiriGucuCanvas;
    public GameObject _saldiriHiziCanvas;
    public GameObject _ikiliAtisCanvas;
    public GameObject _ucluAtisCanvas;
    public GameObject _besliAtisCanvas;


    private void Start()
    {
        int sayi = Random.Range(0, 7);
        if (sayi == 0)
        {
            _heal = true;
            _healCanvas.SetActive(true);
            _canCalma = false;
            _canAlmaCanvas.SetActive(false);
            _saldiriGucu = false;
            _saldiriGucuCanvas.SetActive(false);
            _saldiriHizi = false;
            _saldiriHiziCanvas.SetActive(false);
            _ikiliAtis = false;
            _ikiliAtisCanvas.SetActive(false);
            _ucluAtis = false;
            _ucluAtisCanvas.SetActive(false);
            _besliAtis = false;
            _besliAtisCanvas.SetActive(false);
        }
        else if (sayi == 1)
        {
            _heal = false;
            _healCanvas.SetActive(false);
            _canCalma = true;
            _canAlmaCanvas.SetActive(true);
            _saldiriGucu = false;
            _saldiriGucuCanvas.SetActive(false);
            _saldiriHizi = false;
            _saldiriHiziCanvas.SetActive(false);
            _ikiliAtis = false;
            _ikiliAtisCanvas.SetActive(false);
            _ucluAtis = false;
            _ucluAtisCanvas.SetActive(false);
            _besliAtis = false;
            _besliAtisCanvas.SetActive(false);
        }
        else if (sayi == 2)
        {
            _heal = false;
            _healCanvas.SetActive(false);
            _canCalma = false;
            _canAlmaCanvas.SetActive(false);
            _saldiriGucu = true;
            _saldiriGucuCanvas.SetActive(true);
            _saldiriHizi = false;
            _saldiriHiziCanvas.SetActive(false);
            _ikiliAtis = false;
            _ikiliAtisCanvas.SetActive(false);
            _ucluAtis = false;
            _ucluAtisCanvas.SetActive(false);
            _besliAtis = false;
            _besliAtisCanvas.SetActive(false);
        }
        else if (sayi == 3)
        {
            _heal = false;
            _healCanvas.SetActive(false);
            _canCalma = false;
            _canAlmaCanvas.SetActive(false);
            _saldiriGucu = false;
            _saldiriGucuCanvas.SetActive(false);
            _saldiriHizi = true;
            _saldiriHiziCanvas.SetActive(true);
            _ikiliAtis = false;
            _ikiliAtisCanvas.SetActive(false);
            _ucluAtis = false;
            _ucluAtisCanvas.SetActive(false);
            _besliAtis = false;
            _besliAtisCanvas.SetActive(false);
        }
        else if (sayi == 4)
        {
            _heal = false;
            _healCanvas.SetActive(false);
            _canCalma = false;
            _canAlmaCanvas.SetActive(false);
            _saldiriGucu = false;
            _saldiriGucuCanvas.SetActive(false);
            _saldiriHizi = false;
            _saldiriHiziCanvas.SetActive(false);
            _ikiliAtis = true;
            _ikiliAtisCanvas.SetActive(true);
            _ucluAtis = false;
            _ucluAtisCanvas.SetActive(false);
            _besliAtis = false;
            _besliAtisCanvas.SetActive(false);
        }
        else if (sayi == 5)
        {
            _heal = false;
            _healCanvas.SetActive(false);
            _canCalma = false;
            _canAlmaCanvas.SetActive(false);
            _saldiriGucu = false;
            _saldiriGucuCanvas.SetActive(false);
            _saldiriHizi = false;
            _saldiriHiziCanvas.SetActive(false);
            _ikiliAtis = false;
            _ikiliAtisCanvas.SetActive(false);
            _ucluAtis = true;
            _ucluAtisCanvas.SetActive(true);
            _besliAtis = false;
            _besliAtisCanvas.SetActive(false);
        }
        else if (sayi == 6)
        {
            _heal = false;
            _healCanvas.SetActive(false);
            _canCalma = false;
            _canAlmaCanvas.SetActive(false);
            _saldiriGucu = false;
            _saldiriGucuCanvas.SetActive(false);
            _saldiriHizi = false;
            _saldiriHiziCanvas.SetActive(false);
            _ikiliAtis = false;
            _ikiliAtisCanvas.SetActive(false);
            _ucluAtis = false;
            _ucluAtisCanvas.SetActive(false);
            _besliAtis = true;
            _besliAtisCanvas.SetActive(true);
        }
        else
        {

        }

        Destroy(gameObject.transform.parent.gameObject, 15f);
    }
}
