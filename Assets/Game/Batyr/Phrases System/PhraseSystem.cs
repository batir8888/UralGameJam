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
        private Dictionary<PhraseKey, List<AudioClip>> _phraseMap;
        private float _lastPlayedTime;

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private PhraseDatabase phraseDatabase;
        [SerializeField] private float globalCooldown = 0.5f;

        private void Awake()
        {
            if (audioSource == null)
            {
                audioSource = gameObject.GetComponent<AudioSource>();
                if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
            }

            InitializePhraseMap();

            ServiceLocator.ForSceneOf(this).Register(this);
        }

        private void InitializePhraseMap()
        {
            _phraseMap = new Dictionary<PhraseKey, List<AudioClip>>();
            if (phraseDatabase != null)
            {
                foreach (var phraseEntry in phraseDatabase.phrases)
                {
                    if (phraseEntry.clips != null && phraseEntry.clips.Any(clip => clip != null))
                    {
                        _phraseMap[phraseEntry.key] = phraseEntry.clips.Where(clip => clip != null).ToList();
                    }
                    else
                    {
                        Debug.LogWarning($"PhraseEntry для ключа {phraseEntry.key} не содержит валидных AudioClip'ов.");
                    }
                }
            }
            else
            {
                Debug.LogError("PhraseDatabase не назначен в PhraseManager!");
            }
        }

        public void PlayPhrase(PhraseKey key, bool interruptCurrent = true, bool ignoreCooldown = false)
        {
            if (!ignoreCooldown && Time.time < _lastPlayedTime + globalCooldown)
            {
                Debug.Log($"Phrase {key} skipped due to global cooldown.");
                return;
            }

            if (_phraseMap.TryGetValue(key, out List<AudioClip> clips))
            {
                if (clips != null && clips.Count > 0)
                {
                    AudioClip clipToPlay = clips[Random.Range(0, clips.Count)];

                    if (clipToPlay != null)
                    {
                        if (interruptCurrent || !audioSource.isPlaying)
                        {
                            if (audioSource.isPlaying && interruptCurrent)
                            {
                                audioSource.Stop();
                            }

                            audioSource.clip = clipToPlay;
                            audioSource.Play();
                            Debug.Log($"Playing phrase: {key} (Clip: {clipToPlay.name})");

                            _lastPlayedTime = Time.time;
                        }
                        else
                        {
                            Debug.Log($"Skipping phrase {key} because another is playing and interrupt=false.");
                        }
                    }
                }
            }
            else
            {
                Debug.LogWarning($"Ключ фразы не найден в phraseMap: {key}");
            }
        }

        public void PlayTutorialStartPhrase()
        {
            PlayPhrase(PhraseKey.TutorialStart, true, true);
        }

        public void PlayActionPhrase(PhraseKey actionKey)
        {
            PlayPhrase(actionKey, false);
        }

        public void PlayResultPhrase(List<string> failedTaskIDs)
        {
            PhraseKey keyToPlay;

            if (failedTaskIDs == null || failedTaskIDs.Count == 0)
            {
                keyToPlay = PhraseKey.ResultAllTasksComplete;
            }
            else if (failedTaskIDs.Count == 1)
            {
                string taskID = failedTaskIDs[0];
                string specificEnumName = $"Result_Task_{taskID}_Failed";
                PhraseKey specificFailKey;

                if (System.Enum.TryParse(specificEnumName, out specificFailKey))
                {
                    if (_phraseMap.ContainsKey(specificFailKey))
                    {
                        keyToPlay = specificFailKey;
                    }
                    else
                    {
                        Debug.LogWarning(
                            $"Не найдена фраза для специфичного ключа {specificFailKey}, используется {PhraseKey.ResultGenericFail}");
                        keyToPlay = PhraseKey.ResultGenericFail;
                    }
                }
                else
                {
                    Debug.LogWarning(
                        $"Не удалось найти Enum для {specificEnumName}, используется {PhraseKey.ResultGenericFail}");
                    keyToPlay = PhraseKey.ResultGenericFail;
                }
            }
            else
            {
                keyToPlay = PhraseKey.ResultGenericFail;
            }

            PlayPhrase(keyToPlay, true, true);
        }
    }

    public enum PhraseKey
    {
        TutorialStart,
        ResultAllTasksComplete,
        ResultGenericFail,
        ThrowDynamite,
        ThrowObject
    }
}