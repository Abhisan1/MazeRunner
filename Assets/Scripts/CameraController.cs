using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Top Down")]
    public float pcHeight = 35f;
    public float mobileHeight = 45f;

    [Header("Follow Camera")]
    public Vector3 followOffset = new Vector3(0, 8, -10);
    public float followSmoothSpeed = 5f;

    private bool isTopDown = true;
    private float topDownHeight;

    void Start()
    {
        topDownHeight = Application.isMobilePlatform ? mobileHeight : pcHeight;
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.cKey.wasPressedThisFrame)
            ToggleCamera();
    }

    void LateUpdate()
    {
        if (player == null) return;

        if (isTopDown)
        {
            // Fixed overhead — centered on maze, not following player
            transform.position = new Vector3(0, topDownHeight, 0);
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
        else
        {
            Vector3 targetPos = player.position + followOffset;
            transform.position = Vector3.Lerp(
                transform.position, targetPos,
                followSmoothSpeed * Time.deltaTime);
            transform.LookAt(player.position);
        }
    }

    public void ToggleCamera()
    {
        isTopDown = !isTopDown;
    }
}