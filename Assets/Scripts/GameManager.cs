using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    protected Deck deck;
    protected int credits;
    protected int bet = 1;
    protected List<Card> hand;
    protected HandEvaluator evaluator;

    [SerializeField] public PayTable payTable;
    [SerializeField] public GameObject cardPrefab;

    [SerializeField] public CardSlot[] cardSlots;
    [SerializeField] public TextMeshProUGUI creditsText;

    public static event Action gameInitialized;
    public void Start()
    {
        deck = new Deck();
        hand = new List<Card>();
        evaluator = new HandEvaluator();

        creditsText.text = credits.ToString();
        payTable.Initialize();

        gameInitialized?.Invoke();
    }
}
