using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public enum Suit
    {
        SPADES = 1, 
        HEARTS = 2,
        DIAMONDS = 3,
        CLUBS = 4
    }

    public enum Number
    {
        ACE = 1,
        TWO = 2,
        THREE = 3, 
        FOUR = 4, 
        FIVE = 5,
        SIX = 6,
        SEVEN = 7, 
        EIGHT = 8, 
        NINE = 9, 
        TEN = 10, 
        JACK = 11, 
        QUEEN = 12,
        KING = 13
    }

    public Suit suit { get; set; }
    public Number number { get; set; }
    //public CardVisuals visuals;

    public Card(Suit newSuit, Number newNumber)
    {
        suit = newSuit;
        number = newNumber;
    }
}
