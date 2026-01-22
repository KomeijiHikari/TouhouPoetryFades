using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 单位血 : MonoBehaviour
{
Image img;

    Vector2 Start;
    private void Awake()
    {
        img = this.GetComponent<Image>();
        Start = transform.localScale;
    }
    public bool B;
    bool lastb;
public void 初始化()
    {
        if (B)
        {
            img.color = Color.white;
        }
        else
        {
            img.color = Color.red;
        }
        lastb = B;


    }

    private void Update()
    {
        if (lastb != B)
        {
            lastb = B;
            if (B)
            {
                img.color = Color.white;
            }
            else
            {
                img.color = Color.red;
            }

 
            transform.DOShakeScale(duration: 0.5f, strength: 0.5f, vibrato: 10, randomness: 90f, fadeOut: true)
                .OnComplete(() => transform.localScale = Start);
        }


    }
    //private void Update()
    //{
    //    if (this.transform.GetSiblingIndex() < (int)Player3.I.当前hp)
    //    {
    //        img.enabled = true;
    //    }
    //    else
    //    {
    //        img.enabled = false;
    //    }
    //}
}
