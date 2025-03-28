using UnityEngine;

namespace Game.Batyr.Task_System
{
    public abstract class TurnOffTask : MonoBehaviour
    {
        public bool isCompleted;

        public virtual void TurnOff()
        {
            if (isCompleted) return;
            isCompleted = true;
        }
    }
}