using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlimePatlamasiSonrasi : MonoBehaviour
{
    [SerializeField] GameObject _jumpPoint;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalJump(_jumpPoint.transform.localPosition,5,1,1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
