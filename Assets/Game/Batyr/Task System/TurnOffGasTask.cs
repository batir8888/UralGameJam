using UnityServiceLocator;

namespace Game.Batyr.Task_System
{
    public class TurnOffGasTask : TurnOffTask, ITask
    {
        private Dynamite.Dynamite _dynamite;

        private void Awake()
        {
            ServiceLocator.ForSceneOf(this).Register(this);
            _dynamite = GetComponent<Dynamite.Dynamite>();
        }

        public bool IsCompleted() => _isCompleted;

        public string GetDescription()
        {
            return "Выключить газ";
        }

        public override void TurnOff()
        {
            base.TurnOff();
            if (_dynamite) _dynamite.enabled = false;
        }
    }
}