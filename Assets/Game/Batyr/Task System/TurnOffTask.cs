using UnityEngine;

namespace Game.Batyr.Task_System
{
    public abstract class TurnOffTask : MonoBehaviour
    {
        protected bool _isCompleted = false;

        public virtual void TurnOff()
        {
            if (_isCompleted) return;
            _isCompleted = true;
        }
    }
}