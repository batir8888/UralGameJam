using DG.Tweening;
using Game.Batyr.Task_System;
using TMPro;
using UnityEngine;
using UnityServiceLocator;

namespace Game.Batyr
{
    public class ScoreMenu : MonoBehaviour
    {
        private UITimer _uiTimer;
        private int _score;

        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text surviveText;
        [SerializeField] private TMP_Text catText;
        [SerializeField] private TMP_Text gasText;
        [SerializeField] private TMP_Text electricityText;
        [SerializeField] private TMP_Text gradeText;

        private void Start()
        {
            _uiTimer = ServiceLocator.ForSceneOf(this).Get<UITimer>();
            _uiTimer.TimerEnded += OnEndRound;
        }

        private void OnDestroy()
        {
            _uiTimer.TimerEnded -= OnEndRound;
        }

        private void OnEndRound()
        {
            transform.DOMoveX(0, 1f).SetEase(Ease.OutBounce).SetDelay(3f).OnComplete(() =>
            {
                surviveText.color =
                    ServiceLocator.ForSceneOf(this).Get<SurviveTask>().IsCompleted() ? Color.green : Color.red;
                catText.color = ServiceLocator.ForSceneOf(this).Get<RescueCatTask>().IsCompleted()
                    ? Color.green
                    : Color.red;
                gasText.color = ServiceLocator.ForSceneOf(this).Get<TurnOffGasTask>().IsCompleted()
                    ? Color.green
                    : Color.red;
                electricityText.color = ServiceLocator.ForSceneOf(this).Get<TurnOffElectricity>().IsCompleted()
                    ? Color.green
                    : Color.red;
                _score = ServiceLocator.ForSceneOf(this).Get<ScoreManager.ScoreManager>().Score;
                scoreText.text = $"{_score}";
                if (_score > 5000000)
                    gradeText.text = "А";
                else if (_score > 4000000)
                    gradeText.text = "Б";
                else if (_score > 3000000)
                    gradeText.text = "В";
                else if (_score > 2000000)
                    gradeText.text = "Г";
                else
                    gradeText.text = "Д";
            });
        }
    }
}