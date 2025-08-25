using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Image progressBarFill;
    public Button startButton;
    public Button stopButton;
    public Button resetButton;

    public float totalTime = 60f; // 60秒タイマー
    private float currentTime;
    private bool isRunning = false;

    void Start()
    {
        startButton.onClick.AddListener(StartTimer);
        stopButton.onClick.AddListener(StopTimer);
        resetButton.onClick.AddListener(ResetTimer);
        ResetTimer(); // 初期化
    }

    void Update()
    {
        if (isRunning && currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();
        }
    }

    void StartTimer() => isRunning = true;
    void StopTimer() => isRunning = false;

    void ResetTimer()
    {
        isRunning = false;
        currentTime = totalTime;
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";

        float fillAmount = Mathf.Clamp01(currentTime / totalTime);
        progressBarFill.fillAmount = fillAmount;
    }
}
