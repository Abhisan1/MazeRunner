using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public enum ZoneType
    {
        Obstacle,
        Checkpoint,
        Finish
    }

    public ZoneType zoneType;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        switch (zoneType)
        {
            case ZoneType.Obstacle:
                GameManager.Instance.HitObstacle();
                break;

            case ZoneType.Checkpoint:
                GameManager.Instance.ReachCheckpoint(transform.position);
                break;

            case ZoneType.Finish:
                GameManager.Instance.WinGame();
                break;
        }
    }
}