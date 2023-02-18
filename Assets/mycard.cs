using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class mycard : MonoBehaviour
{
    public GameObject openCard;
    public GameObject CloseCard;

    public int cardvalue;


    public void Movement(Transform pos)
    {
        gameObject.transform.DOMove(pos.position, 0.3f).SetEase(Ease.Flash)
            .OnComplete(() =>
            {
                openCard.transform.DORotate(new Vector3(0, 180, 0), 0.3f).OnUpdate(() =>
                {
                    if (openCard.transform.localRotation.y >= 90)
                    {
                        openCard.SetActive(false);
                        CloseCard.SetActive(true);
                    }
                });
            });
    }

    void Start()
    {
        StartCoroutine(Yazdır(r =>
        {
          Debug.Log(r);
        }));
    }


    public IEnumerator Yazdır(Action<int> action)
    {
        yield return new WaitForSeconds(Random.Range(0f, 10.0f));
      int  a = 5;
        action?.Invoke(a);
    }

    // Update is called once per frame
    void Update()
    {
    }
}