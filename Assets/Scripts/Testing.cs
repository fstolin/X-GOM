using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private GridSystem g;

    // Start is called before the first frame update
    void Start()
    {
        g = new GridSystem(10, 10, 2f);

        Debug.Log(new GridPosition(5, 7));
    }

    private void Update()
    {
        Debug.Log(g.GetGridPosition(MouseWorld.GetMouseWorldPosition()));
    }
}
