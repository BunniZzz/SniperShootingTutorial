using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

public class ParabolicBullet : MonoBehaviour
{
    private float speed;
    private float gravity;
    private Vector3 startPosition;
    private Vector3 startForward;

    private bool isInitialized = false;
    private float startTime = -1;

    public void Initialize(Transform startPoint, float speed, float gravity)
    {
        this.speed = speed;
        this.gravity = gravity;
        startPosition = transform.position;
        startForward = transform.forward;
        isInitialized = true;
    }

    private Vector3 FindPointOnParabola(float time)
    {
        Vector3 point = startPosition + startForward * speed * time;
        Vector3 gravityVec = Vector3.down * gravity * time * time;
        return point + gravityVec;
    }

    private bool CastRayBetweenPoints(Vector3 startPoint, Vector3 endPoint, out RaycastHit hit)
    {
        Vector3 direction = endPoint - startPoint;
        float distance = direction.magnitude;
        direction.Normalize();
        return Physics.Raycast(startPoint, direction, out hit, distance);
    }


    private void FixedUpdate()
    {
        if (!isInitialized)
            return;
        if (startTime < 0)
            startTime = Time.time;

        float currentTime = Time.time - startTime;
        float nextTime = currentTime + Time.fixedDeltaTime;

        Vector3 currentPoint = FindPointOnParabola(currentTime);
        Vector3 nextPoint = FindPointOnParabola(nextTime);

        if (CastRayBetweenPoints(currentPoint, nextPoint, out RaycastHit hit))
        {
            // Handle collision here (e.g., apply damage, play effects)
            Destroy(gameObject); // Destroy the bullet on impact
        }
        else
        {
            transform.position = nextPoint;
        }

    }

    // Update is called once per frame
    private void Update()
    {
        if (!isInitialized || startTime < 0) return;

        float currentTime = Time.time - startTime;
        Vector3 currentPoint = FindPointOnParabola(currentTime);
        transform.position = currentPoint;

    }
}
