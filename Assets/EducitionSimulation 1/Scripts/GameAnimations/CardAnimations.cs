using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GameDatas;

public class CardAnimations : MonoBehaviour
{
    public static CardAnimations Instance;

    public GameObject[] PlayerCards;
    public Transform[] PlayerCardsTargetObje;

    public GameObject[] DealerCards;
    public Transform[] DealerCardsTargetObje;

    public GameObject[] ScoreBoard;
    public GameObject AllButtons;
    public GameObject[] TargetObjects;

    public Transform ComeBackPosition;

    private void Awake() => Instance = this;

    public void GameStartsAnimations()
    {
        StartCoroutine(CardComeTarget(PlayerCards, PlayerCardsTargetObje, 0, 0));
        StartCoroutine(CardComeTarget(DealerCards, DealerCardsTargetObje, 0.5f, 0));
        StartCoroutine(CardComeTarget(PlayerCards, PlayerCardsTargetObje, 1f, 1));
        StartCoroutine(CardComeTarget(DealerCards, DealerCardsTargetObje, 1.5f, 1));
        StartCoroutine(ButtonsPosition(TargetObjects[0].transform, 2f));
    }

    public void GameFinishsAnimations()
    {
        StartCoroutine(CardComeBack(PlayerCards, ComeBackPosition, 0, 0));
        StartCoroutine(CardComeBack(PlayerCards, ComeBackPosition, 0f, 1));
        StartCoroutine(CardComeBack(DealerCards, ComeBackPosition, 0.5f, 0));
        StartCoroutine(CardComeBack(DealerCards, ComeBackPosition, 0.5f, 1));
        StartCoroutine(ButtonsPosition(TargetObjects[1].transform, 1f));
    }

    public IEnumerator CardComeTarget(GameObject[] cards, Transform[] target, float time, int indexNumber)
    {
        yield return new WaitForSeconds(time);
        cards[indexNumber].transform.DOMove(target[indexNumber].position, 0.3f).SetEase(Ease.Flash)
            .OnComplete(() =>
            {
                if (cards[indexNumber] != DealerCards[1])
                {
                    cards[indexNumber].transform.DORotate(new Vector3(0, 180, 0), 0.3f);
                    cards[indexNumber].transform.GetChild(1).gameObject.SetActive(true);
                    CheckScoreBoardController(cards, indexNumber, true);
                }
            });
    }

    public IEnumerator CardComeBack(GameObject[] cards, Transform target, float time, int indexNumber)
    {
        yield return new WaitForSeconds(time);
        if (cards[indexNumber] != DealerCards[1])
        {
            cards[indexNumber].transform.DORotate(new Vector3(0, -360, 0), 0.1f);
            cards[indexNumber].transform.GetChild(1).gameObject.SetActive(false);
        }

        CheckScoreBoardController(cards, indexNumber, false);
        cards[indexNumber].transform.DOMove(target.position, 0.2f).SetEase(Ease.Flash);
    }

    private void CheckScoreBoardController(GameObject[] cards, int indexNumber, bool isActive)
    {
        if (cards[indexNumber] == PlayerCards[0])
        {
            ScoreBoard[0].gameObject.SetActive(isActive);
            ScoreBoard[0].transform.Find("Text").GetComponent<Text>().text =
                HandBooksController.Instance.HandBooksUsers.PlayerCardValue[0].ToString();
        }
        else if (cards[indexNumber] == PlayerCards[1])
        {
            ScoreBoard[0].transform.Find("Text").GetComponent<Text>().text =
                (HandBooksController.Instance.HandBooksUsers.PlayerCardValue[0] +
                 HandBooksController.Instance.HandBooksUsers.PlayerCardValue[1]).ToString();
        }
        else if (cards[indexNumber] == DealerCards[0])
        {
            ScoreBoard[1].gameObject.SetActive(isActive);
            ScoreBoard[1].transform.Find("Text").GetComponent<Text>().text =
                HandBooksController.Instance.HandBooksUsers.DealerCardValue.ToString();
        }
    }

    IEnumerator ButtonsPosition(Transform pos, float time)
    {
        yield return new WaitForSeconds(time);
        AllButtons.transform.DOMove(pos.position, 0.3f).SetEase(Ease.Flash);
    }
}