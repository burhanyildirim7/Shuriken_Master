using UnityEngine;
using System.Collections.Generic;
using System.Linq;



public class ChunkSpawner : MonoBehaviour
{
    public static ChunkSpawner instance;

    public enum AXIS { XPositive, XNegative, ZPositive, ZNegative }

    public GameObject _chunkParentObject;
    public GameObject[] chunks;
    public int initialSpawnCount = 5;
    public float destoryZone = 300;

    [Space(10)]
    public AXIS axis;

    [HideInInspector]
    public Vector3 moveDirection = new Vector3(-1, 0, 0);
    public float movingSpeed = 1;


    public float chunkSize = 60;
    GameObject lastChunk;

    public GameObject _haritaParcalari;

    public GameObject _enemySpawnPoint;

    public List<GameObject> _aralardaCikacakEnemyler = new List<GameObject>();

    private bool _enemyKapat;

    private int _kapaliSayac;

    void Awake()
    {
        if (instance == null) instance = this;

        _chunkParentObject = GameObject.FindGameObjectWithTag("OlusturulanHarita");
        _haritaParcalari = GameObject.FindGameObjectWithTag("OlusturulanHarita");

        initialSpawnCount = initialSpawnCount > chunks.Length ? initialSpawnCount : chunks.Length;

        int chunkIndex = 0;
        for (int i = 0; i < initialSpawnCount; i++)
        {
            GameObject chunk = (GameObject)Instantiate(chunks[chunkIndex]);
            chunk.SetActive(true);

            chunk.GetComponent<RunnerChunk>().spawner = this;

            chunk.transform.parent = _chunkParentObject.transform;

            switch (axis)
            {
                case AXIS.XPositive:
                    chunk.transform.localPosition = new Vector3(-i * chunkSize, 0, transform.position.z);
                    moveDirection = new Vector3(1, 0, 0);
                    break;

                case AXIS.XNegative:
                    chunk.transform.localPosition = new Vector3(i * chunkSize, 0, transform.position.z);
                    moveDirection = new Vector3(-1, 0, 0);
                    break;

                case AXIS.ZPositive:
                    chunk.transform.localPosition = new Vector3(i * chunkSize, 0, transform.position.z);
                    break;

                case AXIS.ZNegative:
                    chunk.transform.localPosition = new Vector3(i * chunkSize, 0, transform.position.z);
                    break;
            }

            chunk.GetComponent<EnemyRandomKontrol>().enabled = false;

            lastChunk = chunk;

            if (++chunkIndex >= chunks.Length)
                chunkIndex = 0;
        }
    }

    public void DestroyChunk(RunnerChunk thisChunk)
    {
        Vector3 newPos = lastChunk.transform.position;
        switch (axis)
        {
            case AXIS.XPositive:
                newPos.x -= chunkSize;
                break;

            case AXIS.XNegative:
                newPos.x += chunkSize;
                break;

            case AXIS.ZPositive:
                break;

            case AXIS.ZNegative:
                break;
        }



        lastChunk = thisChunk.gameObject;
        lastChunk.transform.position = newPos;

        if (_enemyKapat)
        {

            thisChunk.GetComponent<EnemyRandomKontrol>().RandomEnemyKapat();
            thisChunk.GetComponent<EnemyRandomKontrol>().enabled = false;
            _kapaliSayac++;
            EnemyKapalilikSorgula();

        }
        else
        {
            if (GameController.instance.isContinue)
            {
                thisChunk.GetComponent<EnemyRandomKontrol>().enabled = true;
                thisChunk.GetComponent<EnemyRandomKontrol>().RandomEnemyKapat();
                thisChunk.GetComponent<EnemyRandomKontrol>().RandomEnemyAc();
            }
            else
            {

            }

            //thisChunk.GetComponent<EnemyRandomKontrol>().EnemyleriAc();
        }


    }

    public void EnemyKapat()
    {
        _enemyKapat = true;
        _kapaliSayac = 0;
        /*
        for (int i = 0; i < _haritaParcalari.transform.childCount; i++)
        {
            _haritaParcalari.transform.GetChild(i).GetComponent<EnemyRandomKontrol>().RandomEnemyKapat();
            _haritaParcalari.transform.GetChild(i).GetComponent<EnemyRandomKontrol>().enabled = false;
        }
        */
    }

    public void EnemyAc()
    {
        _enemyKapat = false;
        /*
        for (int i = 0; i < _haritaParcalari.transform.childCount; i++)
        {
            _haritaParcalari.transform.GetChild(i).GetComponent<EnemyRandomKontrol>().enabled = true;
            //_haritaParcalari[i].GetComponent<EnemyRandomKontrol>().RandomEnemyKapat(); 
        }
        */
    }

    private void EnemyKapalilikSorgula()
    {
        if (_kapaliSayac == 5)
        {
            //Instantiate(_aralardaCikacakEnemyler[0], _enemySpawnPoint.transform.position, Quaternion.identity);
            Instantiate(_aralardaCikacakEnemyler[0], new Vector3(25, 1, 0), Quaternion.identity);
        }
        else
        {

        }


    }

    public void LevelGecisi()
    {
        for (int i = 0; i < 4; i++)
        {
            Destroy(_haritaParcalari.transform.GetChild(0).gameObject);
        }
    }

    private void XlerGelsin()
    {
        for (int i = 0; i < 4; i++)
        {
            Destroy(_haritaParcalari.transform.GetChild(0).gameObject);
        }
        DestroyChunk(_haritaParcalari.transform.GetChild(0).GetComponent<RunnerChunk>());
    }
}

