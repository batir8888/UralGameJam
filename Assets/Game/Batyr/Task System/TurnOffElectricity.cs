using UnityEngine;
using UnityServiceLocator;

namespace Game.Batyr.Task_System
{
    public class TurnOffElectricity : TurnOffTask, ITask
    {
        [SerializeField] private Light[] lights;

        private void Awake()
        {
            ServiceLocator.ForSceneOf(this).Register(this);
        }

        public bool IsCompleted() => isCompleted;

        public string GetDescription()
        {
            return "Выключить электричество";
        }

        public override void TurnOff()
        {
            base.TurnOff();
            foreach (var l in lights)
            {
                l.enabled = false;
            }
        }
    }
}