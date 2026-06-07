using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private GameObject startPanel, gamePanel, pausePanel, winPanel, losePanel;
    private TextMeshProUGUI scoreText, bestScoreText, checkpointText;
    private TextMeshProUGUI winScoreText, winBestText, loseScoreText, leaderboardText;

    void Awake()
    {
        Instance = this;
        BuildUI();
    }

    void Update()
    {
        if (gamePanel != null && gamePanel.activeSelf && GameManager.Instance != null)
        {
            if (scoreText != null)
                scoreText.text = "Score: " + (int)GameManager.Instance.GetCurrentScore();
            if (bestScoreText != null)
                bestScoreText.text = "Best: " + GameManager.Instance.GetBestScore();
        }
    }

    void BuildUI()
    {
        var scaler = GetComponent<CanvasScaler>();
        if (scaler != null)
        {
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.matchWidthOrHeight = 0.5f;
        }

        // Controls text based on platform
        string controlsText = Application.isMobilePlatform
            ? "D-Pad buttons — Move ball\nCAM button — Switch camera\nPAUSE button — Pause"
            : "WASD / Arrow Keys — Move\nC — Switch camera\nESC — Pause";

        // ── START PANEL ──
        startPanel = MakePanel("StartPanel", new Color(0, 0, 0, 0.92f));
        AnchoredText(startPanel, "MAZE RUNNER",
            new Vector2(0.5f, 0.75f), Vector2.zero, new Vector2(900, 130), 80, Color.cyan);
        AnchoredText(startPanel, "Navigate the ball from Start to Finish!",
            new Vector2(0.5f, 0.63f), Vector2.zero, new Vector2(800, 65), 32, Color.white);
        AnchoredText(startPanel, controlsText,
            new Vector2(0.5f, 0.52f), Vector2.zero, new Vector2(800, 120), 28, Color.yellow);
        AnchoredText(startPanel, "Sensitivity",
            new Vector2(0.5f, 0.41f), Vector2.zero, new Vector2(500, 50), 28, Color.white);
        var slider = AnchoredSlider(startPanel,
            new Vector2(0.5f, 0.35f), Vector2.zero, new Vector2(600, 60));
        slider.onValueChanged.AddListener(OnSensitivityChanged);
        var startBtn = AnchoredButton(startPanel, "PLAY",
            new Vector2(0.5f, 0.22f), Vector2.zero, new Vector2(400, 110),
            new Color(0.1f, 0.75f, 0.1f), 44);
        startBtn.onClick.AddListener(OnStartButton);

        // ── GAME PANEL ──
        gamePanel = MakePanel("GamePanel", new Color(0, 0, 0, 0f));
        scoreText = AnchoredText(gamePanel, "Score: 1000",
            new Vector2(0f, 1f), new Vector2(170, -45), new Vector2(320, 60),
            34, Color.white, TextAlignmentOptions.Center);
        bestScoreText = AnchoredText(gamePanel, "Best: 0",
            new Vector2(0f, 1f), new Vector2(170, -110), new Vector2(320, 55),
            28, Color.yellow, TextAlignmentOptions.Center);
        checkpointText = AnchoredText(gamePanel, "",
            new Vector2(0.5f, 0.6f), Vector2.zero, new Vector2(700, 90), 46, Color.yellow);
        checkpointText.gameObject.SetActive(false);
        var camBtn = AnchoredButton(gamePanel, "CAM",
            new Vector2(1f, 1f), new Vector2(-90, -45), new Vector2(160, 70),
            new Color(0.15f, 0.15f, 0.7f), 28);
        camBtn.onClick.AddListener(SwitchCamera);
        var pauseBtn = AnchoredButton(gamePanel, "PAUSE",
            new Vector2(1f, 1f), new Vector2(-90, -125), new Vector2(160, 70),
            new Color(0.6f, 0.1f, 0.1f), 26);
        pauseBtn.onClick.AddListener(OnPauseButton);

        // ── PAUSE PANEL ──
        pausePanel = MakePanel("PausePanel", new Color(0, 0, 0, 0.92f));
        AnchoredText(pausePanel, "PAUSED",
            new Vector2(0.5f, 0.65f), Vector2.zero, new Vector2(600, 130), 85, Color.white);
        AnchoredText(pausePanel, "Game is paused",
            new Vector2(0.5f, 0.55f), Vector2.zero, new Vector2(600, 55), 30,
            new Color(1, 1, 1, 0.6f));
        var resumeBtn = AnchoredButton(pausePanel, "RESUME",
            new Vector2(0.5f, 0.44f), Vector2.zero, new Vector2(400, 110),
            new Color(0.1f, 0.75f, 0.1f), 44);
        resumeBtn.onClick.AddListener(OnResumeButton);
        var restartBtn = AnchoredButton(pausePanel, "RESTART",
            new Vector2(0.5f, 0.30f), Vector2.zero, new Vector2(400, 110),
            new Color(0.8f, 0.4f, 0.1f), 44);
        restartBtn.onClick.AddListener(OnRestartButton);

        // ── WIN PANEL ──
        winPanel = MakePanel("WinPanel", new Color(0, 0, 0, 0.92f));
        AnchoredText(winPanel, "YOU WIN!",
            new Vector2(0.5f, 0.82f), Vector2.zero, new Vector2(800, 140), 90, Color.green);
        winScoreText = AnchoredText(winPanel, "Final Score: 0",
            new Vector2(0.5f, 0.70f), Vector2.zero, new Vector2(700, 85), 50, Color.white);
        winBestText = AnchoredText(winPanel, "Best Score: 0",
            new Vector2(0.5f, 0.61f), Vector2.zero, new Vector2(700, 75), 42, Color.yellow);
        AnchoredText(winPanel, "TOP 5 SCORES",
            new Vector2(0.5f, 0.52f), Vector2.zero, new Vector2(600, 60), 36, Color.cyan);
        leaderboardText = AnchoredText(winPanel, "",
            new Vector2(0.5f, 0.38f), Vector2.zero, new Vector2(600, 210), 32, Color.white);
        var playAgainBtn = AnchoredButton(winPanel, "PLAY AGAIN",
            new Vector2(0.5f, 0.22f), Vector2.zero, new Vector2(400, 110),
            new Color(0.1f, 0.75f, 0.1f), 44);
        playAgainBtn.onClick.AddListener(OnRestartButton);

        // ── LOSE PANEL ──
        losePanel = MakePanel("LosePanel", new Color(0, 0, 0, 0.92f));
        AnchoredText(losePanel, "GAME OVER",
            new Vector2(0.5f, 0.72f), Vector2.zero, new Vector2(800, 140), 85, Color.red);
        loseScoreText = AnchoredText(losePanel, "Score: 0",
            new Vector2(0.5f, 0.60f), Vector2.zero, new Vector2(700, 85), 52, Color.white);
        AnchoredText(losePanel, "Better luck next time!",
            new Vector2(0.5f, 0.50f), Vector2.zero, new Vector2(700, 60), 32,
            new Color(1, 1, 1, 0.7f));
        var tryAgainBtn = AnchoredButton(losePanel, "TRY AGAIN",
            new Vector2(0.5f, 0.36f), Vector2.zero, new Vector2(400, 110),
            new Color(0.8f, 0.1f, 0.1f), 44);
        tryAgainBtn.onClick.AddListener(OnRestartButton);

        ShowStart();
    }

    void SwitchCamera()
    {
        var cam = FindAnyObjectByType<CameraController>();
        if (cam != null) cam.ToggleCamera();
    }

    // ── BUILDERS ──
    GameObject MakePanel(string name, Color color)
    {
        var p = new GameObject(name);
        p.transform.SetParent(transform, false);
        var r = p.AddComponent<RectTransform>();
        r.anchorMin = Vector2.zero;
        r.anchorMax = Vector2.one;
        r.offsetMin = Vector2.zero;
        r.offsetMax = Vector2.zero;
        p.AddComponent<Image>().color = color;
        return p;
    }

    TextMeshProUGUI AnchoredText(GameObject parent, string text,
        Vector2 anchor, Vector2 offset, Vector2 size, float fontSize, Color color,
        TextAlignmentOptions align = TextAlignmentOptions.Center)
    {
        var obj = new GameObject("T_" + text.Substring(0, Mathf.Min(8, text.Length)));
        obj.transform.SetParent(parent.transform, false);
        var r = obj.AddComponent<RectTransform>();
        r.anchorMin = anchor;
        r.anchorMax = anchor;
        r.pivot = new Vector2(0.5f, 0.5f);
        r.anchoredPosition = offset;
        r.sizeDelta = size;
        var tmp = obj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = fontSize;
        tmp.color = color;
        tmp.alignment = align;
        tmp.textWrappingMode = TextWrappingModes.Normal;
        return tmp;
    }

    Button AnchoredButton(GameObject parent, string label,
        Vector2 anchor, Vector2 offset, Vector2 size, Color color, float fontSize)
    {
        var obj = new GameObject("B_" + label);
        obj.transform.SetParent(parent.transform, false);
        var r = obj.AddComponent<RectTransform>();
        r.anchorMin = anchor;
        r.anchorMax = anchor;
        r.pivot = new Vector2(0.5f, 0.5f);
        r.anchoredPosition = offset;
        r.sizeDelta = size;
        obj.AddComponent<Image>().color = color;
        var btn = obj.AddComponent<Button>();
        var lbl = new GameObject("Label");
        lbl.transform.SetParent(obj.transform, false);
        var lr = lbl.AddComponent<RectTransform>();
        lr.anchorMin = Vector2.zero;
        lr.anchorMax = Vector2.one;
        lr.offsetMin = Vector2.zero;
        lr.offsetMax = Vector2.zero;
        var tmp = lbl.AddComponent<TextMeshProUGUI>();
        tmp.text = label;
        tmp.fontSize = fontSize;
        tmp.color = Color.white;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontStyle = FontStyles.Bold;
        return btn;
    }

    Slider AnchoredSlider(GameObject parent, Vector2 anchor, Vector2 offset, Vector2 size)
    {
        var obj = new GameObject("Slider");
        obj.transform.SetParent(parent.transform, false);
        var r = obj.AddComponent<RectTransform>();
        r.anchorMin = anchor;
        r.anchorMax = anchor;
        r.pivot = new Vector2(0.5f, 0.5f);
        r.anchoredPosition = offset;
        r.sizeDelta = size;
        var bg = new GameObject("Bg");
        bg.transform.SetParent(obj.transform, false);
        var bgR = bg.AddComponent<RectTransform>();
        bgR.anchorMin = new Vector2(0, 0.25f);
        bgR.anchorMax = new Vector2(1, 0.75f);
        bgR.offsetMin = Vector2.zero;
        bgR.offsetMax = Vector2.zero;
        bg.AddComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
        var fillArea = new GameObject("FillArea");
        fillArea.transform.SetParent(obj.transform, false);
        var faR = fillArea.AddComponent<RectTransform>();
        faR.anchorMin = new Vector2(0, 0.25f);
        faR.anchorMax = new Vector2(1, 0.75f);
        faR.offsetMin = new Vector2(5, 0);
        faR.offsetMax = new Vector2(-15, 0);
        var fill = new GameObject("Fill");
        fill.transform.SetParent(fillArea.transform, false);
        var fR = fill.AddComponent<RectTransform>();
        fR.anchorMin = Vector2.zero;
        fR.anchorMax = new Vector2(0.5f, 1);
        fR.offsetMin = Vector2.zero;
        fR.offsetMax = Vector2.zero;
        fill.AddComponent<Image>().color = new Color(0.1f, 0.6f, 0.9f);
        var handleArea = new GameObject("HandleArea");
        handleArea.transform.SetParent(obj.transform, false);
        var haR = handleArea.AddComponent<RectTransform>();
        haR.anchorMin = Vector2.zero;
        haR.anchorMax = Vector2.one;
        haR.offsetMin = new Vector2(10, 0);
        haR.offsetMax = new Vector2(-10, 0);
        var handle = new GameObject("Handle");
        handle.transform.SetParent(handleArea.transform, false);
        var hR = handle.AddComponent<RectTransform>();
        hR.sizeDelta = new Vector2(60, 60);
        var hImg = handle.AddComponent<Image>();
        hImg.color = Color.white;
        var slider = obj.AddComponent<Slider>();
        slider.fillRect = fR;
        slider.handleRect = hR;
        slider.targetGraphic = hImg;
        slider.minValue = 0.5f;
        slider.maxValue = 2f;
        slider.value = 1f;
        return slider;
    }

    string BuildLeaderboardString()
    {
        string result = "";
        string[] medals = { "1st", "2nd", "3rd", "4th", "5th" };
        for (int i = 0; i < 5; i++)
        {
            int s = PlayerPrefs.GetInt("Score_" + i, 0);
            result += medals[i] + " -- " + s + "\n";
        }
        return result;
    }

    void OnSensitivityChanged(float val)
    {
        var pc = FindAnyObjectByType<PlayerController>();
        if (pc != null) pc.sensitivity = val;
    }

    public void ShowStart() { SetAll(false); startPanel?.SetActive(true); }
    public void ShowGame()  { SetAll(false); gamePanel?.SetActive(true);  }
    public void ShowPause() { SetAll(false); pausePanel?.SetActive(true); }

    public void ShowWin(int score, int best)
    {
        SetAll(false);
        winPanel?.SetActive(true);
        if (winScoreText)    winScoreText.text    = "Final Score: " + score;
        if (winBestText)     winBestText.text     = "Best Score: "  + best;
        if (leaderboardText) leaderboardText.text = BuildLeaderboardString();
    }

    public void ShowLose(int score)
    {
        SetAll(false);
        losePanel?.SetActive(true);
        if (loseScoreText) loseScoreText.text = "Score: " + score;
    }

    public void ShowCheckpointMessage()
    {
        if (checkpointText == null) return;
        checkpointText.text  = "Checkpoint Reached!";
        checkpointText.color = Color.yellow;
        checkpointText.gameObject.SetActive(true);
        CancelInvoke(nameof(HideNotification));
        Invoke(nameof(HideNotification), 2f);
    }

    public void ShowPenaltyFlash()
    {
        if (checkpointText == null) return;
        checkpointText.text  = "-100 Penalty!";
        checkpointText.color = Color.red;
        checkpointText.gameObject.SetActive(true);
        CancelInvoke(nameof(HideNotification));
        Invoke(nameof(HideNotification), 1.5f);
    }

    void HideNotification()
    {
        if (checkpointText != null)
            checkpointText.gameObject.SetActive(false);
    }

    void SetAll(bool s)
    {
        startPanel?.SetActive(s);
        gamePanel?.SetActive(s);
        pausePanel?.SetActive(s);
        winPanel?.SetActive(s);
        losePanel?.SetActive(s);
    }

    public void OnStartButton()   { ShowGame(); GameManager.Instance?.StartGame(); }
    public void OnPauseButton()   { GameManager.Instance?.PauseGame(); }
    public void OnResumeButton()  { GameManager.Instance?.ResumeGame(); }
    public void OnRestartButton() { GameManager.Instance?.RestartGame(); }
}
