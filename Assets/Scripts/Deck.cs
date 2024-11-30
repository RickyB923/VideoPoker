using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Security.Cryptography;

public class Deck
{
    public List<Card> cards { get; set; }
    public Deck()
    {
        InitializeDeck();
        ShuffleDeck();
    }

    void InitializeDeck()
    {
        cards = new List<Card>();

        for (int i = 1; i < Enum.GetNames(typeof(Card.Suit)).Length + 1; i++)
        {
            for (int j = 1; j < Enum.GetNames(typeof(Card.Number)).Length + 1; j++)
            {
                Card newCard = new Card((Card.Suit)i, (Card.Number)j);
                cards.Add(newCard);
                
            }
        }
    }
    public void ShuffleDeck()
    {
        System.Random random = new System.Random();

        for (int i = cards.Count - 1; i > 0; i--)
        {
            var k = random.Next(i + 1);
            var value = cards[k];
            cards[k] = cards[i];
            cards[i] = value;
        }
    }
    public Card DealCard()
    {
        Card card = cards.FirstOrDefault();
        cards.Remove(card);
        return card;
    }
}
