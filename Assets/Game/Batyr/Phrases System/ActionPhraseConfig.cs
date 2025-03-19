using System.Collections.Generic;
using UnityEngine;

namespace Game.Batyr.Phrases_System
{
    [CreateAssetMenu(fileName = "ActionPhrase", menuName = "Phrases/ActionPhraseCfg")]
    public class ActionPhraseConfig : ScriptableObject
    {
        public PhraseSystem.PlayerAction action;
        public List<AudioClip> phrases;
    }
}