using DG.Tweening;
using Game.Batyr.Phrases_System;
using Game.Batyr.Task_System;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityServiceLocator;

namespace Game.Batyr
{
    public class ScoreMenu : MonoBehaviour
    {
        private UITimer _uiTimer;
        private int _score;
        private FirstPersonController _firstPersonController;
        private PhraseSystem _phraseSystem;

        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text surviveText;
        [SerializeField] private TMP_Text catText;
        [SerializeField] private TMP_Text gasText;
        [SerializeField] private TMP_Text electricityText;
        [SerializeField] private TMP_Text safeText;
        [SerializeField] private TMP_Text gradeText;

        private void Start()
        {
            _firstPersonController = FindObjectOfType<FirstPersonController>();
            _phraseSystem = ServiceLocator.ForSceneOf(this).Get<PhraseSystem>();
            _uiTimer = ServiceLocator.ForSceneOf(this).Get<UITimer>();
            _uiTimer.TimerEnded += OnEndRound;
        }

        private void OnDestroy()
        {
            _uiTimer.TimerEnded -= OnEndRound;
        }

        private void OnEndRound()
        {
            _firstPersonController.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            bool isSurvive = ServiceLocator.ForSceneOf(this).Get<SurviveTask>().IsCompleted();
            bool isCatSafe = ServiceLocator.ForSceneOf(this).Get<RescueCatTask>().IsCompleted();
            bool isGasDisabled = ServiceLocator.ForSceneOf(this).Get<TurnOffGasTask>().IsCompleted();
            bool isElectricityDisabled = ServiceLocator.ForSceneOf(this).Get<TurnOffElectricity>().IsCompleted();
            bool isSafeRetrieve = ServiceLocator.ForSceneOf(this).Get<RetrieveSafeTask>().IsCompleted();
            surviveText.color = isSurvive ? Color.green : Color.red;
            catText.color = isCatSafe ? Color.green : Color.red;
            catText.gameObject.SetActive(true);

            if (isGasDisabled)
            {
                gasText.color = Color.green;
                gasText.gameObject.SetActive(true);
            }
            else
            {
                gasText.color = Color.red;
            }

            if (isElectricityDisabled)
            {
                electricityText.color = Color.green;
                electricityText.gameObject.SetActive(true);
            }
            else
            {
                electricityText.color = Color.red;
            }

            if (isSafeRetrieve)
            {
                safeText.color = Color.green;
                safeText.gameObject.SetActive(true);
            }
            else
            {
                safeText.color = Color.red;
            }

            transform.DOMoveX(0, 1f).SetEase(Ease.OutBounce).SetDelay(6f).OnComplete(() =>
            {
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
                if (!isSurvive)
                {
                    _phraseSystem.PlayPhrase(PhraseKey.ResultGenericFail);
                    return;
                }

                if (!isCatSafe)
                {
                    _phraseSystem.PlayPhrase(PhraseKey.AboutCat);
                    return;
                }

                if (!isGasDisabled)
                {
                    gasText.gameObject.SetActive(true);
                    _phraseSystem.PlayPhrase(PhraseKey.AboutGas);
                    return;
                }

                if (!isElectricityDisabled)
                {
                    electricityText.gameObject.SetActive(true);
                    _phraseSystem.PlayPhrase(PhraseKey.AboutElectricity);
                    return;
                }

                if (!isSafeRetrieve)
                {
                    safeText.gameObject.SetActive(true);
                    _phraseSystem.PlayPhrase(PhraseKey.AboutSafe);
                    return;
                }

                _phraseSystem.PlayPhrase(PhraseKey.ResultAllTasksComplete);
            });
        }
    }
}