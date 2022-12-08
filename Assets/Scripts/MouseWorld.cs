using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{

    [SerializeField] private LayerMask mousePlaneLayerMask;

    private static MouseWorld instance;

    private void Awake()
    {
        instance = this;
    }

    // Returns the point of the ray, that is cast from main camera to a plane in mousePlane layer.
    // Effectively returns world position from mouse position.
    public static Vector3 GetMouseWorldPosition()
    {
        // The ray from camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Casting the ray from the camera to the mouse position in the world - respecting only mousePlaneLayerMask
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayerMask);
        // Returning the collision point of the ray
        return raycastHit.point;
    }
}
