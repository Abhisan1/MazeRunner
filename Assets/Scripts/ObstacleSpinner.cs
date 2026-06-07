using UnityEngine;

public class ObstacleSpinner : MonoBehaviour
{
    public float spinSpeed = 120f;

    void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);
    }
}