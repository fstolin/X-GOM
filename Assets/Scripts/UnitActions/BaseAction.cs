using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract -> can't instantiate this class
public abstract class BaseAction : MonoBehaviour
{

    protected Unit unit;
    protected bool isActive = false;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

}
