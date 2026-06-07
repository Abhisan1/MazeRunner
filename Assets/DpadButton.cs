using UnityEngine;
using UnityEngine.EventSystems;

public class DpadButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public Vector2 direction;

    public void OnPointerDown(PointerEventData e)
    {
        if (MobileJoystick.Instance != null)
            MobileJoystick.Instance.SetInput(direction);
    }

    public void OnPointerUp(PointerEventData e)
    {
        if (MobileJoystick.Instance != null)
            MobileJoystick.Instance.SetInput(Vector2.zero);
    }

    public void OnPointerExit(PointerEventData e)
    {
        if (MobileJoystick.Instance != null)
            MobileJoystick.Instance.SetInput(Vector2.zero);
    }
}
