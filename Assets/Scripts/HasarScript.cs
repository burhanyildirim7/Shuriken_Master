using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasarScript : MonoBehaviour
{
    public float _verecegiHasar;

    private void Start()
    {
        _verecegiHasar = 25 + (LevelController.instance.totalLevelNo * 5);
    }

}
