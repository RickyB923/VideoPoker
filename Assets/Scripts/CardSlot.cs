using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    private Card currentCard;
    private bool shouldHold;
    [SerializeField] GameObject holdIcon;
    void Start()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("click");
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
