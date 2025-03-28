using UnityEngine;

namespace Game.Batyr.Task_System
{
    public class TaskInteractionSystem : MonoBehaviour
    {
        private AudioSource _audioSource;
        private Camera _camera;

        [SerializeField] private float raycastDistance = 5f;

        private void Awake()
        {
            _camera = Camera.main;
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.E)) return;
            if (!Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hit,
                    raycastDistance)) return;
            if (hit.collider.TryGetComponent(out TurnOffTask task))
            {
                if (!task.isCompleted) _audioSource.Play();
                task.TurnOff();
            }
        }
    }
}