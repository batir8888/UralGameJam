using System.Collections;
using UnityEngine;

namespace Game.Batyr.Dynamite
{
    [RequireComponent(typeof(AudioSource))]
    public class DynamiteSfx : MonoBehaviour
    {
        private AudioSource _audioSource;
        private Coroutine _coroutine;

        [SerializeField] private AudioClip sfx;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play()
        {
            _coroutine = StartCoroutine(PlaySfx());
        }

        private IEnumerator PlaySfx()
        {
            _audioSource.PlayOneShot(sfx);
            yield return new WaitUntil(() => !_audioSource.isPlaying);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (_coroutine != null) StopCoroutine(_coroutine);
        }
    }
}