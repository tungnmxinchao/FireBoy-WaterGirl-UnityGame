using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    [Header("Time Display")]
    [SerializeField] private TextMeshProUGUI timeDisplay;
    private float timeElapsed = 0f;
    private string timeString = "";

    [Header("Gems")]
    [SerializeField] private int totalGems = 6;
    public int gemsCollected = 0; // Made public for access in Gem class
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gemCollectSound;

    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button replayButton;
    private bool isPaused = false;

    [Header("Win Panel")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI finalTimeDisplay;
    [SerializeField] private TextMeshProUGUI rankDisplay;
    [SerializeField] private TextMeshProUGUI gemCountDisplay;

    [Header("Lose Panel")]
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Button loseReplayButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private TextMeshProUGUI loseTimeDisplay;

    private void Start()
    {
        InitializeGame();
    }

    private void Update()
    {
        if (!isPaused)
        {
            timeElapsed += Time.deltaTime;
            UpdateTimeDisplay();
        }

        UpdateGemRankDisplay(); // Updated to call in Update
    }

    private void OnDestroy()
    {
        RemoveAllListeners();
    }

    private void InitializeGame()
    {
        timeElapsed = 0f;
        gemsCollected = 0;
        isPaused = false;
        Time.timeScale = 1f;

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        UpdateTimeDisplay();
        SetInitialUIStates();
        SetupButtonListeners();
    }

    private void SetInitialUIStates()
    {
        if (pauseUI != null) pauseUI.SetActive(false);
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
        if (pauseButton != null) pauseButton.interactable = true;
    }

    private void SetupButtonListeners()
    {
        if (pauseButton != null) pauseButton.onClick.AddListener(PauseGame);
        if (continueButton != null) continueButton.onClick.AddListener(ResumeGame);
        if (closeButton != null) closeButton.onClick.AddListener(ResumeGame);
        if (replayButton != null) replayButton.onClick.AddListener(ReplayLevel);
        if (loseReplayButton != null) loseReplayButton.onClick.AddListener(ReplayLevel);
        if (homeButton != null) homeButton.onClick.AddListener(ReturnToHome);
    }

    private void RemoveAllListeners()
    {
        if (pauseButton != null) pauseButton.onClick.RemoveListener(PauseGame);
        if (continueButton != null) continueButton.onClick.RemoveListener(ResumeGame);
        if (closeButton != null) closeButton.onClick.RemoveListener(ResumeGame);
        if (replayButton != null) replayButton.onClick.RemoveListener(ReplayLevel);
        if (loseReplayButton != null) loseReplayButton.onClick.RemoveListener(ReplayLevel);
        if (homeButton != null) homeButton.onClick.RemoveListener(ReturnToHome);
    }

    public void CollectGem()
    {
        gemsCollected++;

        if (audioSource != null && gemCollectSound != null)
        {
            audioSource.PlayOneShot(gemCollectSound);
        }

        if (gemsCollected >= totalGems)
        {
            LevelCompleted();
        }
    }

    public void PlayGemCollectSound()
    {
        if (audioSource != null && gemCollectSound != null)
        {
            audioSource.PlayOneShot(gemCollectSound);
        }
    }

    private void LevelCompleted()
    {
        StartCoroutine(ShowWinPanelWithDelay());
    }

    private IEnumerator ShowWinPanelWithDelay()
    {
        yield return new WaitForSeconds(4f);
        ShowWinPanel();
    }

    private string CalculateRank()
    {
        float percentageCollected = (float)gemsCollected / totalGems * 100f;

        if (percentageCollected > 70f) return "A";
        if (percentageCollected >= 35f) return "B";
        return "C";
    }

    private void SetRankColor(string rank)
    {
        if (rankDisplay == null) return;

        switch (rank)
        {
            case "A":
                rankDisplay.color = Color.green;
                break;
            case "B":
                rankDisplay.color = Color.yellow;
                break;
            case "C":
                rankDisplay.color = Color.red;
                break;
            default:
                rankDisplay.color = Color.gray;
                break;
        }
    }

    private void UpdateGemRankDisplay()
    {
        if (gemCountDisplay != null)
        {
            string rank = CalculateRank();
            gemCountDisplay.text = "Rank:" + $"{rank}";
            SetRankColor(rank);
        }
    }

    private void UpdateTimeDisplay()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timeDisplay != null)
        {
            timeDisplay.text = timeString;
        }
    }

    public void ShowLosePanel()
    {
        if (losePanel != null)
        {
            losePanel.SetActive(true);
            if (loseTimeDisplay != null)
            {
                int minutes = Mathf.FloorToInt(timeElapsed / 60);
                int seconds = Mathf.FloorToInt(timeElapsed % 60);
                loseTimeDisplay.text = $"Time: {minutes:00}:{seconds:00}";
            }
        }
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            if (finalTimeDisplay != null)
            {
                finalTimeDisplay.text = $"Time: {timeString}";
            }
        }
        Time.timeScale = 0f;
        isPaused = true;
        if (pauseButton != null)
        {
            pauseButton.interactable = false;
        }
    }

    public void ReplayLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("CrystalMap");
    }

    public void ReturnToHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Home");
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        if (pauseUI != null) pauseUI.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pauseUI != null) pauseUI.SetActive(false);
    }
}
