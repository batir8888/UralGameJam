using UnityEngine;
using TMPro;

public class UITimer : MonoBehaviour
{
    [SerializeField] private int bombСounter;
    public float timeRemaining = 60;
    public bool timerIsRunning;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bombText;

    private void Start()
    {
        timerIsRunning = true;
    }

    private void Update()
    {
        bombText.text = bombСounter.ToString();

        if (!timerIsRunning) return;
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            timeRemaining = 0;
            timerIsRunning = false;
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = $"{minutes:00}:{seconds:00}";
    }
}