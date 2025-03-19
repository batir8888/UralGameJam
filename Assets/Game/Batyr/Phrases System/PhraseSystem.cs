using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityServiceLocator;
using Random = UnityEngine.Random;

namespace Game.Batyr.Phrases_System
{
    [RequireComponent(typeof(AudioSource))]
    public class PhraseSystem : MonoBehaviour
    {
        public enum PlayerAction
        {
            ThrowDynamite,
            ThrowObject,
            AboutCat,
            AboutGas,
            AboutElectricity,
        }

        [Header("Phrases Configs")] [SerializeField]
        private List<ActionPhraseConfig> actionPhraseConfigs;

        [SerializeField] private TimedPhrasesConfig timedPhrasesConfig;
        [Range(0f, 1f)] [SerializeField] private float timedPhraseProbability = 0.5f;

        [Header("Time Settings")] [SerializeField]
        private float minTimeBetweenPhrases = 5f;

        [SerializeField] private float maxTimeBetweenPhrases = 5f;

        private AudioSource _audioSource;
        private float _nextTimedPhraseTime;
        private readonly HashSet<AudioClip> _recentlyPlayedTimedPhrases = new();
        private const int MaxRecentlyPlayedTimedPhrases = 5;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            ServiceLocator.ForSceneOf(this).Register(this);

            ScheduleNextTimedPhrase();
        }

        private void Update()
        {
            if (!(Time.time >= _nextTimedPhraseTime)) return;
            if (Random.value <= timedPhraseProbability)
            {
                PlayRandomTimedPhrase();
            }

            ScheduleNextTimedPhrase();
        }

        private void ScheduleNextTimedPhrase()
        {
            _nextTimedPhraseTime = Time.time + Random.Range(minTimeBetweenPhrases, maxTimeBetweenPhrases);
        }

        private void PlayRandomTimedPhrase()
        {
            if (_audioSource.isPlaying)
            {
                return;
            }

            if (!timedPhrasesConfig || timedPhrasesConfig.phrases.Count == 0)
            {
                return;
            }

            AudioClip phraseToPlay;
            var availablePhrases = timedPhrasesConfig.phrases.Except(_recentlyPlayedTimedPhrases).ToList();

            if (availablePhrases.Count > 0)
            {
                phraseToPlay = availablePhrases[Random.Range(0, availablePhrases.Count)];
            }
            else
            {
                _recentlyPlayedTimedPhrases.Clear();
                phraseToPlay = timedPhrasesConfig.phrases[Random.Range(0, timedPhrasesConfig.phrases.Count)];
            }

            if (!phraseToPlay) return;
            _audioSource.PlayOneShot(phraseToPlay);

            _recentlyPlayedTimedPhrases.Add(phraseToPlay);
            if (_recentlyPlayedTimedPhrases.Count > MaxRecentlyPlayedTimedPhrases)
            {
                _recentlyPlayedTimedPhrases.Remove(_recentlyPlayedTimedPhrases.First());
            }
        }

        public void TriggerPhrase(PlayerAction playerAction)
        {
            if (_audioSource.isPlaying)
            {
                return;
            }

            ActionPhraseConfig category = actionPhraseConfigs.FirstOrDefault(c => c.action == playerAction);
            if (category && category.phrases.Count > 0)
            {
                PlayRandomPhrase(category.phrases);
            }
            else
            {
                Debug.LogWarning($"Нет фраз для действия: {playerAction}");
            }
        }

        private void PlayRandomPhrase(List<AudioClip> phrases)
        {
            if (_audioSource.isPlaying)
            {
                return;
            }

            if (phrases.Count > 0)
            {
                _audioSource.PlayOneShot(phrases[Random.Range(0, phrases.Count)]);
            }
        }
    }
}