using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlackJackGameManager : GameManager
{
    public List<Card> houseHand;
    public int playerHandValue;
    public int houseHandValue;
    [SerializeField] CardSlot[] houseCardSlots;
    public bool isBusted;
    public bool isBetweenGames;
    new void Start()
    {
        base.Start();
        houseHand = new List<Card>();
        display.SetAllButtonsActive(false);
        isBetweenGames = true;
        display.mainMessageDisplay.SetActive(true);
        display.mainMessageText.text = "Blackjack";
    }
    public void StartGame()
    {
        display.SetAllButtonsActive(true);
        display.deal.SetActive(false);
        display.mainMessageDisplay.SetActive(false);
        isBetweenGames = false;
        credits -= bet;
        creditsText.text = credits.ToString();
        foreach (CardSlot slot in cardSlots)
        {
            slot.isEmpty = true;
            GameObject cardObject = Instantiate(cardPrefab, slot.transform);
            slot.visuals = cardObject.GetComponent<CardVisuals>();
            slot.visuals.SetCardVisualsActive(false);
        }
        foreach (CardSlot slot in houseCardSlots)
        {
            slot.isEmpty = true;
            GameObject cardObject = Instantiate(cardPrefab, slot.transform);
            slot.visuals = cardObject.GetComponent<CardVisuals>();
            slot.visuals.SetCardVisualsActive(false);
        }
        FirstDeal();
    }
    void FirstDeal()
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            if(i < 2) // Deals 2 cards to the player and the house at the start of the game
            {
                AddCardToHand(i, cardSlots, playerHand);
                AddCardToHand(i, houseCardSlots, houseHand);
                if (i == 0) // Turns the house's first card face down
                {
                    houseCardSlots[i].visuals.TurnCardFaceDown();
                }
            }
            else // And hides any remaining cards from the last game
            {
                cardSlots[i].visuals.SetCardVisualsActive(false);
                houseCardSlots[i].visuals.SetCardVisualsActive(false);
            }
            if (EvaluateHand(playerHand) == 21) // Checks if the player has a natural Blackjack
            {
                StartCoroutine(display.DisplayInbetweenGameScreen("Blackjack!"));
                BlackJack();
            }
        }
    }
    void PlayerWins()
    {
        credits += bet;
        creditsText.text = credits.ToString();
        RevealHouseHand();
        isBetweenGames = true;
    }
    void BlackJack()
    {
        credits += (int)Math.Ceiling(bet * 1.5f);
        creditsText.text = credits.ToString();
        RevealHouseHand();
        isBetweenGames = true;
    }
    void PlayerLoses()
    {
        RevealHouseHand();
        isBetweenGames = true;
    }
    public void RestartGame()
    {
        isBetweenGames = false;
        credits -= bet;
        creditsText.text = credits.ToString();
        deck.ShuffleDeck();
        playerHand.Clear();
        playerHandValue = 0;
        houseHand.Clear();
        houseHandValue = 0;

        display.mainMessageDisplay.SetActive(false);
        display.playAgain.SetActive(false);
        display.SetAllButtonsActive(true);
        foreach (CardSlot slot in cardSlots)
        {
            slot.isEmpty = true;
        }
        foreach (CardSlot slot in houseCardSlots)
        {
            slot.isEmpty = true;
        }
        FirstDeal();     
    }
    void AddCardToHand(int slotIndex, CardSlot[] cardSlots, List<Card> hand)
    {
        Card newCard = deck.DealCard();
        hand.Add(newCard);
        cardSlots[slotIndex].currentCard = newCard;
        cardSlots[slotIndex].isEmpty = false;
        cardSlots[slotIndex].visuals.SetCardVisuals(newCard.suit, newCard.number);
        cardSlots[slotIndex].visuals.TurnCardFaceUp();
        cardSlots[slotIndex].visuals.SetCardVisualsActive(true);              
    }
    public void PlayerHits()
    {
        int i = 0;
        foreach(CardSlot slot in cardSlots)
        {
            if(!slot.isEmpty)
            {
                i++;
            }
        }
        AddCardToHand(i, cardSlots, playerHand);
        if(EvaluateHand(playerHand) > 21)
        {
            StartCoroutine(display.DisplayInbetweenGameScreen("Bust!"));
            PlayerLoses();
        }
        else if(EvaluateHand(playerHand) == 21)
        {
            StartCoroutine(display.DisplayInbetweenGameScreen("Blackjack!"));
            BlackJack();
        }
        else if(EvaluateHand(playerHand) < 21)
        {
            StartCoroutine(display.DisplayMessage("Hit or Stay?"));
        }
    }
    public void HouseHits()
    {
        int i = 0;
        foreach (CardSlot slot in houseCardSlots)
        {
            if (!slot.isEmpty)
            {
                i++;
            }
        }
        AddCardToHand(i, houseCardSlots, houseHand);
    }
    public void Stay() // Initiates the house's turn and determines the winner
    {
        playerHandValue = EvaluateHand(playerHand);
        while(ShouldHouseHit())
        {
            HouseHits();
        }
        houseHandValue = EvaluateHand(houseHand);
        DetermineWinner();
    }
    void DetermineWinner() // Compares the player's hand and the house's hand
    {
        if (houseHandValue > 21)
        {
            StartCoroutine(display.DisplayInbetweenGameScreen("House Busts, You Win!"));
            PlayerWins();
        }
        else if (playerHandValue >= houseHandValue)
        {
            StartCoroutine(display.DisplayInbetweenGameScreen("You Win!"));
            PlayerWins();
        }
        else if (playerHandValue < houseHandValue)
        {
            StartCoroutine(display.DisplayInbetweenGameScreen("House Wins"));
            PlayerLoses();
        }
    }
    void RevealHouseHand()
    {
        foreach(CardSlot slot in houseCardSlots)
        {
            if(!slot.isEmpty)
            {
                slot.visuals.TurnCardFaceUp();
            }
        }
    }
    public bool ShouldHouseHit()
    {
        if(EvaluateHand(houseHand) < 17) // House hits on 16 or below
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public int EvaluateHand(List<Card> hand)
    {
        int total = 0;
        int numberOfAces = 0;
        foreach(Card card in hand)
        {
            switch(card.number) // Counts face cards as 10, and counts the number of Aces
            {
                case Card.Number.ACE:
                    numberOfAces++;
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
        if(total <= 10) // Determines the value of an Ace depending on the current value of the hand
        { 
            if(total + (numberOfAces ) * 11 <= 21) 
            {
                total += (numberOfAces * 11);
            }
        }
        else if(total > 10)
        {
            total += (numberOfAces * 1);
        }
        return total;
    }
    public void RaiseBet()
    {
        if (!isBetweenGames)
            return;
        if (bet < 10)
        {
            bet++;
            betText.text = bet.ToString();
        }
    }
    public void LowerBet()
    {
        if (!isBetweenGames)
            return;
        if (bet > 1)
        {
            bet--;
            betText.text = bet.ToString();
            
        }
    }
}
