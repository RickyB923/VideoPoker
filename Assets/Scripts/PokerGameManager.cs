using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerGameManager : GameManager
{
    new void Start()
    {
        base.Start();
        StartGame();
    }


    public void StartGame()
    {
        //Deals 5 cards to the player at the start of the game
        for (int i = 0; i < 5; i++)
        {
            Card newCard = deck.DealCard();    
            hand.Add(newCard);

            GameObject cardObject = Instantiate(cardPrefab, cardSlots[i].transform);
            CardVisuals cardVisuals = cardObject.GetComponent<CardVisuals>();
            cardVisuals.SetCardVisuals(newCard.suit, newCard.number);
            
            Debug.Log($"{hand[i].number} of {hand[i].suit}");
        }
        
    }


}
