using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Deck deck;
    void Start()
    {
        deck = new Deck();
    }
}
