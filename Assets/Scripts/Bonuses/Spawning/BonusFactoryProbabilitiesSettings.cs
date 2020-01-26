using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    [CreateAssetMenu(menuName = "Bonus Probabilities Settings")]
    public class BonusFactoryProbabilitiesSettings : ScriptableObject
    {
        [System.Serializable]
        private struct ValuePair
        {
            public BonusType key;
            [Range(0, 100)]
            public int value;
        }

        [SerializeField]
        private List<ValuePair> bonusProbabilities;

        [HideInInspector]
        public Dictionary<BonusType, int> bonusesProbabilitiesMap {
            get {
                var dictionary = new Dictionary<BonusType, int>();
                foreach (var keyPair in bonusProbabilities) {
                    dictionary.Add(keyPair.key, keyPair.value);
                }
                return dictionary;
            }
        }
    }
}
