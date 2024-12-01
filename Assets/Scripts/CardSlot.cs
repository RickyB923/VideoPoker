using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public Card currentCard;
    public CardVisuals visuals;
    public bool isEmpty;
    public bool shouldHold;
    [SerializeField] GameObject holdIcon;
    void Start()
    {
        
    }

    public void OnClick()
    {
        if(!shouldHold)
        {
            shouldHold = true;
            holdIcon.SetActive(true);
        }
        else
        {
            shouldHold = false;
            holdIcon.SetActive(false);
        }
    }
}
