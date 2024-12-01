using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJackGameManager : GameManager
{
    public List<Card> houseHand;
    public int handValue;
    public int houseHandValue;
    public bool isBusted;
    new void Start()
    {
        base.Start();
        houseHand = new List<Card>();
        StartGame();
    }
    public void StartGame()
    {
        foreach(CardSlot slot in cardSlots)
        {
            slot.isEmpty = true;
        }
        //Deals 2 cards to the player at the start of the game
        for (int i = 0; i < 2; i++)
        {
            AddCardToHand(i);
            //isFirstGame = false;
        }
    }
    void AddCardToHand(int slotIndex)
    {
        Card newCard = deck.DealCard();
        hand.Add(newCard);
        cardSlots[slotIndex].currentCard = newCard;
        cardSlots[slotIndex].isEmpty = false;

        GameObject cardObject = Instantiate(cardPrefab, cardSlots[slotIndex].transform);
        cardSlots[slotIndex].visuals = cardObject.GetComponent<CardVisuals>();
        cardSlots[slotIndex].visuals.SetCardVisuals(newCard.suit, newCard.number);
    }
    public void Hit()
    {
        int i = 0;
        foreach(CardSlot slot in cardSlots)
        {
            if(!slot.isEmpty)
            {
                i++;
            }
        }
        AddCardToHand(i);
        if(EvaluateHand(hand) > 21)
        {
            Debug.Log("Bust");
        }
        else if(EvaluateHand(hand) == 21)
        {
            Debug.Log("Blackjack");
        }
        else if(EvaluateHand(hand) < 21)
        {
            Debug.Log("Hit or Stay?");
        }
        //Debug.Log($"Player's hand: {hand}");
    }
    public int EvaluateHand(List<Card> hand)
    {
        int total = 0;
        foreach(Card card in hand)
        {
            switch(card.number) // Counts face cards as 10, and Aces as 1 or 11
            {
                case Card.Number.ACE:
                    if(total + 11 > 21)
                    {
                        total += 1;
                    }
                    else
                    {
                        total += 11;
                    }
                    break;
                case Card.Number.JACK:
                    total += 10;
                    break;
                case Card.Number.QUEEN:
                    total += 10;
                    break;
                case Card.Number.KING:
                    total += 10;
                    break;
                default:
                    total += (int)card.number;
                    break;
            }        
        }
        if(total > 21) // Evaluates the hand again to recount the value of an Ace
        {
            foreach(Card card in hand)
            {
                if(card.number == Card.Number.ACE)
                {
                    total -= 11;
                    total += 1;
                }
            }
        }
        Debug.Log(total);
        return total;
    }
}
