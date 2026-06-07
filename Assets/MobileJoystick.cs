using UnityEngine;

public class MobileJoystick : MonoBehaviour
{
    public static MobileJoystick Instance;
    public Vector2 Input { get; private set; }

    void Awake()
    {
        // Singleton — destroy duplicates
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetInput(Vector2 dir)
    {
        Input = dir;
    }

    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
