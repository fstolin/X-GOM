using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SpinAction : BaseAction
{
    // Simple delegate definition for complete spin action - defining our own
    /*
    public delegate void SpinCompleteDelegate();
    private SpinCompleteDelegate onSpinComplete;
    */

    // Delegate from using System - refactoring it to base Class
    /*
    private Action onSpinComplete;
        */

    private float alreadySpinned = 0f;


    private void Update()
    {
        if (!isActive) return;

        float spinAddAmount = 360f * Time.deltaTime;
        alreadySpinned += spinAddAmount;

        if (alreadySpinned > 360f)
        {
            isActive = false;
            onActionComplete();
        }

        transform.eulerAngles += new Vector3(0,spinAddAmount, 0);
    }

    // Spins the the player 360 degrees
    public void Spin(Action onActionComplete)
    {
        // assign spinComplete reference to this Spin Action
        this.onActionComplete = onActionComplete;
        isActive = true;
        alreadySpinned = 0f;
    }
}
