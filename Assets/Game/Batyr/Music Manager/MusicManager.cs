using UnityEngine;

namespace Game.Batyr.Music_Manager
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        private AudioSource _audioSource;

        [SerializeField] private AudioClip[] musicClips;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _audioSource.PlayOneShot(musicClips[Random.Range(0, musicClips.Length)]);
        }

        private void Update()
        {
            if (!_audioSource.isPlaying) _audioSource.PlayOneShot(musicClips[Random.Range(0, musicClips.Length)]);
        }
    }
}