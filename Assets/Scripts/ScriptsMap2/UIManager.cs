using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private AudioManager audioManager;
    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel;
    public GameObject setting;
    public GameObject music;
    public GameObject sfx;
    public Button continueButton;
    public Button nextButton;
    public Button pause;
    public Button MusicOn;
    public Button MusicOff;
    public Button SFXOn;
    public Button SFXOff;
    public Button settingButton;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI pauseText;
    public GameObject RankA;
    public GameObject ARankA;
    public GameObject RankB;
    public GameObject ARankB;
    public GameObject RankC;
    public GameObject ARankC;
    public GameObject Unrank;


    private float elapsedTime;
    private int totalCollectedGems;   // Tổng số gem đã thu thập bởi cả hai player

    public int totalWaterGems;        // Tổng số lượng WaterGem có trong game
    public int totalFireGems;         // Tổng số lượng FireGem có trong game
    private bool isGameOver = false;
    private bool isWin = false;
    private bool isPaused = false;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        elapsedTime = 0f;
        totalCollectedGems = 0;
        ARankA.SetActive(false);
        ARankB.SetActive(false);
        ARankC.SetActive(false);
        RankA.SetActive(false);
        RankB.SetActive(false);
        RankC.SetActive(false);
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (!isGameOver && !isWin && !isPaused)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    // Hàm xử lý khi một player thu thập gem
    public void CollectGem(string gemTag)
    {
        if (gemTag == "WaterGem" || gemTag == "FireGem")
        {
            totalCollectedGems++;
            UpdateRank();  // Cập nhật Rank mỗi khi ăn gem
        }
    }

    void UpdateRank()
    {
        // Tổng số gem cần thu thập
        int totalGems = totalWaterGems + totalFireGems;

        // Tính phần trăm gem đã thu thập
        float percentage = (totalGems > 0) ? ((float)totalCollectedGems / totalGems) * 100f : 0f;

        // Xếp hạng dựa trên phần trăm
        string rank = (percentage >= 70f) ? "A" : (percentage >= 35f) ? "B" : "C";
        PlayerPrefs.SetString("Rank", rank);
    }
    public void GameOver()
    {
        isGameOver = true;
        pause.gameObject.SetActive(false);
        ShowGameOverPanel(); 
    }
    private void ShowGameOverPanel()
    {
        // Cập nhật text cho panel
        settingButton.gameObject.SetActive(true);
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        timeText.text = $"Time: {minutes:00}:{seconds:00}";
        rankText.text = "Rank: " + PlayerPrefs.GetString("Rank", "N/A");
        loseText.gameObject.SetActive(true);
        Time.timeScale = 0f;
        // Kích hoạt panel kết thúc game
        gameOverPanel.SetActive(true);
        PlayerPrefs.DeleteKey("Rank");
    }
    public void Win()
    {
        isGameOver = true;
        pause.gameObject.SetActive(false);
        ShowWinPanel();
    }
    private void ShowWinPanel()
    {
        // Cập nhật text cho panel
        settingButton.gameObject.SetActive(true);
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        timeText.text = $"Time: {minutes:00}:{seconds:00}";
        string rank = PlayerPrefs.GetString("Rank", "N/A");
        rankText.text = "Rank: " + rank;
        Unrank.SetActive(false);
        StartCoroutine(PlayWinAnimation(rank));
        winText.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        // Kích hoạt panel kết thúc game
        gameOverPanel.SetActive(true);
        PlayerPrefs.DeleteKey("Rank");
    }
    private IEnumerator PlayWinAnimation(string rank)
    {
        GameObject rankAnimation = null;
        GameObject rankFinal = null;
        switch (rank)
        {
            case "A":
                ARankA.SetActive(true);  // Bật animation A
                rankAnimation = ARankA;   // Lưu animation vào biến
                rankFinal = RankA;
                break;
            case "B":
                ARankB.SetActive(true);  // Bật animation B
                rankAnimation = ARankB;   // Lưu animation vào biến
                rankFinal = RankB;
                break;
            case "C":
                ARankC.SetActive(true);  // Bật animation C
                rankAnimation = ARankC;   // Lưu animation vào biến
                rankFinal = RankC;
                break;
        }

        if (rankAnimation != null)
        {
            yield return new WaitForSeconds(1f);
            rankAnimation.SetActive(false);
            rankFinal.SetActive(true);
        }
    }
        public void PauseGame()
    {
        isPaused = true;
        audioManager.PlaySFX(audioManager.click);
        audioManager.PlayMenu();
        ShowPausePanel();
        Time.timeScale = 0f;
    }

    private void ShowPausePanel()
    {
        // Cập nhật text cho panel
        settingButton.gameObject.SetActive(true);
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        timeText.text = $"Time: {minutes:00}:{seconds:00}";
        rankText.text = "Rank: " + PlayerPrefs.GetString("Rank", "N/A");
        pauseText.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
        // Kích hoạt panel kết thúc game
        gameOverPanel.SetActive(true);
    }
    private void ClosePause()
    {
        pauseText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
    }
    public void Retry()
    {
        audioManager.PlaySFX(audioManager.click);
        // Đặt thời gian chạy bình thường
        Time.timeScale = 1f;
        PlayerPrefs.DeleteKey("Rank");
        // Đặt lại các biến về trạng thái ban đầu
        elapsedTime = 0f;
        totalCollectedGems = 0;
        isGameOver = false;
        isWin = false;
        isPaused = false;

        // Ẩn các UI panel và cập nhật lại text
        gameOverPanel.SetActive(false);
        loseText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);

        // Tải lại scene nếu cần thiết để đặt lại toàn bộ trạng thái
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void ContinueGame()
    {
        audioManager.PlaySFX(audioManager.click);
        isPaused = false;
        gameOverPanel.SetActive(false);
        settingButton.gameObject.SetActive(false);
        audioManager.Continue();
        ClosePause();
        Time.timeScale = 1f; 
    }
    public void Setting()
    {
        pause.gameObject.SetActive(false);
        audioManager.PlaySFX(audioManager.click);
        gameOverPanel.SetActive(false);
        settingButton.gameObject.SetActive(false);
        setting.SetActive(true);
    }
    public void OpenMusic()
    {
        audioManager.PlaySFX(audioManager.click);
        MusicOff.gameObject.SetActive(false);
        MusicOn.gameObject.SetActive(true);
        music.SetActive(true);
    }
    public void OffMusic()
    {
        audioManager.PlaySFX(audioManager.click);
        MusicOn.gameObject.SetActive(false);
        MusicOff.gameObject.SetActive(true);
        music.SetActive(false);
    }
    public void OpenSFX()
    {
        SFXOff.gameObject.SetActive(false);
        SFXOn.gameObject.SetActive(true);
        sfx.SetActive(true);
    }
    public void OffSFX()
    {
        audioManager.PlaySFX(audioManager.click);
        SFXOn.gameObject.SetActive(false);
        SFXOff.gameObject.SetActive(true);
        sfx.SetActive(false);
    }
    public void Back()
    {
        audioManager.PlaySFX(audioManager.click);
        setting.SetActive(false);
        settingButton.gameObject.SetActive(true);
        pause.gameObject.SetActive(true);
        gameOverPanel.SetActive(true);
    }
    public void gotoHome()
    {
        SceneManager.LoadScene("Home");
    }
    public void nextToMap2()
    {
        SceneManager.LoadScene("Map 3");
    }

}
