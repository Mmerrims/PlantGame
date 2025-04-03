using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private List<Transform> points;

    [Header("Settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float smoothingSpeed;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float stoppingTime;

    [Tooltip("Private Variables")]
    private Vector2 movementDirection;
    private int pointIndex;
    private float timeStamp;
    private bool timeStampOnce;

    private void Start()
    {
        movementDirection = GetMovementDirection(points[0].position);

        pointIndex = 0;
        timeStamp = 0.0f;
        timeStampOnce = true;
    }

    private void Update()
    {
        transform.Translate(movementDirection.normalized * movementSpeed * (1 - Mathf.Exp(-smoothingSpeed *Time.fixedDeltaTime)));

        NextMovementDirectionHandler();
    }

    private void NextMovementDirectionHandler()
    {
        if (GetMovementDirection(points[pointIndex].position).magnitude < stoppingDistance)
        {
            movementDirection = Vector2.zero;

            if (timeStampOnce)
            {
                timeStamp = Time.time;
                timeStampOnce = false;
            }

            if((Time.time - timeStamp) > stoppingTime)
            {
                if (pointIndex == points.Count - 1)
                    pointIndex = 0;
                else
                    ++pointIndex;

                movementDirection = GetMovementDirection(points[pointIndex].position);
                timeStamp = 0.0f;
                timeStampOnce = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach (Transform point in points)
            Gizmos.DrawSphere(point.position, 0.2f);
    }

    private Vector2 GetMovementDirection(Vector2 pointPos)
    {
        return pointPos - (Vector2)transform.position;
    }




























    //    public GameObject pointA;
    //    public GameObject pointB;
    //    private Rigidbody2D rb;
    //    private Transform currentPoint;
    //    public float speed;
    //    // Start is called before the first frame update
    //    void Start()
    //    {
    //        rb = GetComponent<Rigidbody2D>();
    //        currentPoint = pointB.transform;
    //    }

    //    // Update is called once per frame
    //    void Update()
    //    {
    //        Vector2 point = currentPoint.position - transform.position;
    //        if(currentPoint == pointB.transform)
    //        {
    //            rb.velocity = new Vector2(speed, 0);
    //        }
    //        else
    //        {
    //            rb.velocity = new Vector2(-speed, 0);
    //        }

    //        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
    //        {
    //            currentPoint = pointA.transform;
    //        }
    //        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
    //        {
    //            currentPoint = pointB.transform;
    //        }
    //    }
}
