using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float stoppingDistance = .05f;

    private Vector3 targetPosition;

    private void Update()
    {
        // Running - Prevent the unit from moving after it's almost at the position
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance) 
        {
            // We calculate normalized direction & then move the unit in that direction
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
        } 
        // Stopped
        else
        {
            GetComponentInChildren<Animator>().SetBool("isRunning", false);
        }

        // Move to a new place after clicking the mouse
        if (Input.GetMouseButtonDown(1))
        {
            Move(MouseWorld.GetMouseWorldPosition());
        }
    }

    private void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        GetComponentInChildren<Animator>().SetBool("isRunning", true);
        StartCoroutine(rotateSmoothly(targetPosition));
    }

    IEnumerator rotateSmoothly(Vector3 targetPosition)
    {
        float progress = 0f;
        Quaternion startRotation = transform.rotation;

        while (progress < 1)
        {
            transform.rotation = Quaternion.Lerp(startRotation, Quaternion.LookRotation(targetPosition), progress);
            progress += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
    }

}
