using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arkanoid
{
    public class BonusFactory : MonoBehaviour
    {
        [SerializeField]
        private BonusFactoryPrefabSettings prefabSettings;
        [SerializeField]
        private BonusFactoryProbabilitiesSettings probabilitiesSettings;

        private void Awake() {
            EventBus.Subscribe<SpawnBonusEvent>(SpawnBonus);
        }

        private void SpawnBonus(SpawnBonusEvent customEvent) {
            Instantiate(prefabSettings.bonusesPrefabMap[GetRandomType()], customEvent.SpawningPosition, Quaternion.identity, transform.parent);
        }

        private BonusType GetRandomType() {
            int chance = UnityEngine.Random.Range(1, 101);
            for (int i = 0; i < probabilitiesSettings.bonusesProbabilitiesMap.Count; i++) {
                if (SumOfProbabilities(i) >= chance) {
                    return probabilitiesSettings.bonusesProbabilitiesMap.ElementAt(i).Key;
                }
            }

            return BonusType.SpeedUp;
        }
        private int SumOfProbabilities(int _elemtIndex) {
            int sum = 0;
            for (int i = _elemtIndex; i >= 0; i--) {
                sum += probabilitiesSettings.bonusesProbabilitiesMap.ElementAt(i).Value;
            }
            return sum;
        }
    }
}
