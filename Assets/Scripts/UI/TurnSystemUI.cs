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
        turnCountText.text = "Turn: 1";

        nextTurnButton.onClick.AddListener(() =>
        {
            TurnSystem.Instance.NextTurn();
            UpdateTurnCountText();
        });
    }

    private void UpdateTurnCountText()
    {
        turnCountText.text = "Turn: " + TurnSystem.Instance.GetTurnNumber();
    }
}
