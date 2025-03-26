using System.Collections;
using UnityEngine;

namespace Game.Batyr.TrackableObject
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class TrackableObject : MonoBehaviour
    {
        private Vector3 _startPosition;
        private bool _isTracking;
        private WaitForSeconds _waitForSeconds;
        private float _delta;

        [SerializeField] private float delayBeforeTracking = 2.5f;

        protected virtual void Awake()
        {
            _waitForSeconds = new WaitForSeconds(delayBeforeTracking);
            _startPosition = transform.position;
        }

        public virtual void StartTracking()
        {
            if (_isTracking) return;
            StartCoroutine(TrackCoroutine());
            _isTracking = true;
        }

        protected abstract void OnTrackingComplete(float distance);

        private IEnumerator TrackCoroutine()
        {
            yield return _waitForSeconds;
            _delta = Vector3.Distance(transform.position, _startPosition);
            OnTrackingComplete(_delta);
            _isTracking = false;
        }
    }
}