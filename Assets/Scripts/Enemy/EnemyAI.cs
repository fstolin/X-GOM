using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private float timer;

    private void Start()
    {
        TurnSystem.Instance.OnNextTurnHappening += TurnSystem_OnNextTurnHappening;
    }

    private void TurnSystem_OnNextTurnHappening(object sender, EventArgs e)
    {
        if (!TurnSystem.Instance.IsPlayerTurn())
        {
            timer = 2f;
        }
    }

    private void Update()
    {
        // Player turn -> enemies do nothing
        if (TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        } 

        timer -= Time.deltaTime;
        Debug.Log(timer);

        if (timer < 0)
        {
            TurnSystem.Instance.NextTurn();
        }
    }
}
