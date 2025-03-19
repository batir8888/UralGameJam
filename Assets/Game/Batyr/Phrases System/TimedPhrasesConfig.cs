using System.Collections.Generic;
using UnityEngine;

namespace Game.Batyr.Phrases_System
{
    [CreateAssetMenu(fileName = "TimedPhrases", menuName = "Phrases/TimedPhrasesCfg")]
    public class TimedPhrasesConfig : ScriptableObject
    {
        public List<AudioClip> phrases;
    }
}