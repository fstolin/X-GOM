using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{

    private float alreadySpinned = 0f;

    private void Update()
    {
        if (!isActive) return;

        float spinAddAmount = 360f * Time.deltaTime;
        alreadySpinned += spinAddAmount;

        if (alreadySpinned > 360f ) isActive = false;

        transform.eulerAngles += new Vector3(0,spinAddAmount, 0);
    }

    // Spins the the player 360 degrees
    public void Spin()
    {
        isActive = true;
        alreadySpinned = 0f;
    }
}
