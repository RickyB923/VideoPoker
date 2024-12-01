using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    protected Deck deck;
    protected int credits;
    protected int bet;
    protected List<Card> hand;

    [SerializeField] public PayTable payTable;
    [SerializeField] public GameObject cardPrefab;

    [SerializeField] public CardSlot[] cardSlots;

    public static event Action gameInitialized;
    public void Start()
    {
        deck = new Deck();
        hand = new List<Card>();
        payTable.Initialize();

        gameInitialized?.Invoke();
    }
}
