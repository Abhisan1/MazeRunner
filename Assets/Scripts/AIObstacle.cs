using UnityEngine;

public class AIObstacle : MonoBehaviour
{
    [Header("Patrol Points")]
    public Vector3 pointA = new Vector3(-5f, 0.5f, 0f);
    public Vector3 pointB = new Vector3(5f, 0.5f, 0f);

    [Header("Speed")]
    public float speed = 3f;

    private Vector3 targetPoint;

    void Start()
    {
        targetPoint = pointB;
    }

    void Update()
    {
        // Move towards target point
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint,
            speed * Time.deltaTime
        );

        // Switch target when reached
        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;
        }

        // Rotate to look like it's alive
        transform.Rotate(0f, 90f * Time.deltaTime, 0f);
    }
}