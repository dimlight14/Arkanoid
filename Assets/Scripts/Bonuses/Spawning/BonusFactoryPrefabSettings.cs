using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{

    [CreateAssetMenu(menuName = "Bonus Prefab Settings")]
    public class BonusFactoryPrefabSettings : ScriptableObject
    {
        [System.Serializable]
        private struct ValuePair
        {
            public BonusType key;
            public GameObject value;
        }

        [SerializeField]
        private List<ValuePair> bonusPrefabs;

        [HideInInspector]
        public Dictionary<BonusType, GameObject> bonusesPrefabMap {
            get {
                var dictionary = new Dictionary<BonusType, GameObject>();
                foreach (var keyPair in bonusPrefabs) {
                    dictionary.Add(keyPair.key, keyPair.value);
                }
                return dictionary;
            }
        }
    }
}
