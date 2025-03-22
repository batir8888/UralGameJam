using System;
using Game.Batyr.Inventory_System;
using UnityEngine;
using TMPro;
using UnityServiceLocator;

public class UITimer : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bombText;
    public event Action TimerEnded;

    private bool _timerIsRunning;
    private InventoryManager _inventoryManager;

    [SerializeField] private float timeRemaining = 60;

    private void Awake()
    {
        ServiceLocator.ForSceneOf(this).Register(this);
    }

    private void Start()
    {
        _inventoryManager = ServiceLocator.ForSceneOf(this).Get<InventoryManager>();
        _inventoryManager.DynamiteCountChanged += OnDynamiteCountChanged;

        bombText.text = _inventoryManager.DynamitesCount.ToString();
    }

    private void Update()
    {
        if (!_timerIsRunning) return;
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            TimerEnded?.Invoke();
            timeRemaining = 0;
            _timerIsRunning = false;
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = $"{minutes:00}:{seconds:00}";
    }

    private void OnDynamiteCountChanged(int count)
    {
        bombText.text = count.ToString();
    }

    public void StartTimer()
    {
        _timerIsRunning = true;
    }
}