using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Score Settings")]
    public float startingScore = 1000f;
    public float penaltyAmount = 100f;

    private float currentScore;
    private float timer;
    private bool gameActive = false;
    private bool checkpointReached = false;
    private Vector3 checkpointPosition;
    private Vector3 startPosition;
    private GameObject player;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
            startPosition = player.transform.position;

        currentScore = startingScore;
        gameActive = false;
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (!gameActive) return;

        timer += Time.deltaTime;
        currentScore = Mathf.Max(0, startingScore - (timer * 10f));

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            PauseGame();
    }

    public void StartGame()
    {
        gameActive = true;
        Time.timeScale = 1f;
        UIManager.Instance?.ShowGame();
    }

    public void PauseGame()
    {
        gameActive = false;
        Time.timeScale = 0f;
        UIManager.Instance?.ShowPause();
    }

    public void ResumeGame()
    {
        gameActive = true;
        Time.timeScale = 1f;
        UIManager.Instance?.ShowGame();
    }

    public void HitObstacle()
    {
        currentScore = Mathf.Max(0, currentScore - penaltyAmount);
        UIManager.Instance?.ShowPenaltyFlash();

        if (player != null)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            player.transform.position = checkpointReached
                ? checkpointPosition
                : startPosition;
        }
    }

    public void ReachCheckpoint(Vector3 position)
    {
        if (!checkpointReached)
        {
            checkpointReached = true;
            checkpointPosition = position;
            UIManager.Instance?.ShowCheckpointMessage();
        }
    }

    public void WinGame()
    {
        gameActive = false;
        Time.timeScale = 0f;
        SaveScore((int)currentScore);
        UIManager.Instance?.ShowWin((int)currentScore, GetBestScore());
    }

    public void LoseGame()
    {
        gameActive = false;
        Time.timeScale = 0f;
        SaveScore((int)currentScore);
        UIManager.Instance?.ShowLose((int)currentScore);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public float GetCurrentScore() => currentScore;
    public int GetBestScore() => PlayerPrefs.GetInt("BestScore", 0);

    void SaveScore(int score)
    {
        int best = PlayerPrefs.GetInt("BestScore", 0);
        if (score > best)
        {
            PlayerPrefs.SetInt("BestScore", score);
            PlayerPrefs.Save();
        }

        int[] scores = new int[5];
        for (int i = 0; i < 5; i++)
            scores[i] = PlayerPrefs.GetInt("Score_" + i, 0);

        scores[4] = score;
        System.Array.Sort(scores);
        System.Array.Reverse(scores);

        for (int i = 0; i < 5; i++)
            PlayerPrefs.SetInt("Score_" + i, scores[i]);

        PlayerPrefs.Save();
    }
}