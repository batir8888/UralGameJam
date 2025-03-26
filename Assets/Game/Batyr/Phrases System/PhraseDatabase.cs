using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Batyr.Phrases_System
{
    [CreateAssetMenu(fileName = "PhraseDatabase", menuName = "Phrase System/Phrase Database")]
    public class PhraseDatabase : ScriptableObject
    {
        public List<PhraseEntry> phrases = new List<PhraseEntry>();

        public List<AudioClip> GetClips(PhraseKey key)
        {
            return (from entry in phrases where entry.key == key select entry.clips).FirstOrDefault();
        }
    }

    [System.Serializable]
    public class PhraseEntry
    {
        public PhraseKey key;
        public List<AudioClip> clips;

        public PhraseEntry(PhraseKey key, List<AudioClip> clips)
        {
            this.key = key;
            this.clips = clips;
        }
    }
}