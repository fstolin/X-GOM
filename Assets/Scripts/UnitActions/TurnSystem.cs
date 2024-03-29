using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurnSystem : MonoBehaviour
{

    public static TurnSystem Instance { get; private set; }
    public event System.EventHandler OnNextTurnHappening;

    private int turnNumber = 1;
    private bool isPlayerTurn = true;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's modre than UnitActionSystemUI! " + this.transform + " - " + Instance);
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    public void NextTurn()
    {
        if (!isPlayerTurn) turnNumber++;
        isPlayerTurn = !isPlayerTurn;

        OnNextTurnHappening?.Invoke(this, EventArgs.Empty);
    }

    public int GetTurnNumber()
    {
        return turnNumber;
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }

}
