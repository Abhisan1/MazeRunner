using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 25f;
    public float sensitivity = 1f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;

        if (MobileJoystick.Instance != null && MobileJoystick.Instance.Input != Vector2.zero)
        {
            Vector2 joy = MobileJoystick.Instance.Input;
            // X = left/right, Y = forward/back (note: positive Y on dpad = forward = positive Z in world)
            move = new Vector3(joy.x, 0f, joy.y);
        }
        else
        {
            var keyboard = Keyboard.current;
            if (keyboard != null)
            {
                if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)  move.x = -1f;
                if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) move.x =  1f;
                if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)    move.z =  1f;
                if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)  move.z = -1f;
            }
        }

        rb.AddForce(move * moveSpeed * sensitivity);
    }
}
