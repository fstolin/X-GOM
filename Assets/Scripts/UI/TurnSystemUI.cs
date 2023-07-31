using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    public static TurnSystemUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI turnCountText;
    [SerializeField] private Button nextTurnButton;
    [SerializeField] private TextMeshProUGUI enemyTurnText;

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

    private void Start()
    {
        // UI Start 
        turnCountText.text = "Turn: 1";
        enemyTurnText.gameObject.SetActive(false);
        TurnSystem.Instance.OnNextTurnHappening += TurnSystem_OnNextTurnHappening;

        nextTurnButton.onClick.AddListener(() =>
        {
            TurnSystem.Instance.NextTurn();
            UpdateTurnCountText();
            HandleTurnSystemUI();
        });
    }

    private void TurnSystem_OnNextTurnHappening(object sender, EventArgs e)
    {
        HandleTurnSystemUI();
    }

    private void UpdateTurnCountText()
    {
        turnCountText.text = "Turn: " + TurnSystem.Instance.GetTurnNumber();
    }

    // Handles Turn System UI as a whole
    private void HandleTurnSystemUI()
    {
        HandleEnemyTurnUI();
    }

    // Handles enemy turn UI display
    private void HandleEnemyTurnUI()
    {
        if (TurnSystem.Instance.IsPlayerTurn())
        {
            // PLAYER TURN
            // disable enemy turn text
            enemyTurnText.gameObject.SetActive(false);
            // enable next turn and turn count
            nextTurnButton.gameObject.SetActive(true);
            turnCountText.gameObject.SetActive(true);
            // enable rendervisuals
            GridSystemVisual.Instance.EnableRenderVisuals();
        } else
        {
            // ENEMY TURN
            // enable enemy turn text
            enemyTurnText.gameObject.SetActive(true);
            // disable next turn and turn count
            nextTurnButton.gameObject.SetActive(false);
            turnCountText.gameObject.SetActive(false);
            // disable render visuals
            GridSystemVisual.Instance.DisableRenderVisuals();
        }
    }
}
