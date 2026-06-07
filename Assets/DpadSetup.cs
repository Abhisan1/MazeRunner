using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DpadSetup : MonoBehaviour
{
    void Start()
    {
        if (Application.isMobilePlatform)
            BuildDpad();
    }

    void BuildDpad()
    {
        // Destroy any existing dpad canvas to prevent duplicates
        var existing = GameObject.Find("DpadCanvas");
        if (existing != null) Destroy(existing);

        var canvasObj = new GameObject("DpadCanvas");
        var canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;

        var scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = 0.5f;

        canvasObj.AddComponent<GraphicRaycaster>();

        float size = 130f;
        float cx = 200f;
        float cy = 200f;

        MakeBtn(canvasObj, "BtnUp",    "▲", new Vector2(cx,        cy + size), new Vector2(0,  1));
        MakeBtn(canvasObj, "BtnDown",  "▼", new Vector2(cx,        cy - size), new Vector2(0, -1));
        MakeBtn(canvasObj, "BtnLeft",  "◄", new Vector2(cx - size, cy),        new Vector2(-1, 0));
        MakeBtn(canvasObj, "BtnRight", "►", new Vector2(cx + size, cy),        new Vector2(1,  0));
    }

    void MakeBtn(GameObject parent, string name, string label, Vector2 pos, Vector2 direction)
    {
        float size = 130f;

        var obj = new GameObject(name);
        obj.transform.SetParent(parent.transform, false);

        var r = obj.AddComponent<RectTransform>();
        r.anchorMin        = Vector2.zero;
        r.anchorMax        = Vector2.zero;
        r.pivot            = new Vector2(0.5f, 0.5f);
        r.anchoredPosition = pos;
        r.sizeDelta        = new Vector2(size, size);

        obj.AddComponent<Image>().color = new Color(1f, 1f, 1f, 0.3f);
        obj.AddComponent<Button>();

        var dpad = obj.AddComponent<DpadButton>();
        dpad.direction = direction;

        var lblObj = new GameObject("Label");
        lblObj.transform.SetParent(obj.transform, false);
        var lr = lblObj.AddComponent<RectTransform>();
        lr.anchorMin = Vector2.zero;
        lr.anchorMax = Vector2.one;
        lr.offsetMin = Vector2.zero;
        lr.offsetMax = Vector2.zero;
        var txt = lblObj.AddComponent<Text>();
        txt.text      = label;
        txt.fontSize  = 56;
        txt.color     = Color.white;
        txt.alignment = TextAnchor.MiddleCenter;
        txt.font      = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
    }
}
