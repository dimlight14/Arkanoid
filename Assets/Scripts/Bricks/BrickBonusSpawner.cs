using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    [RequireComponent(typeof(BrickMainComponent))]
    public class BrickBonusSpawner : MonoBehaviour
    {
        private BrickMainComponent mainComponent;
        private void Awake() {
            mainComponent = GetComponent<BrickMainComponent>();
            mainComponent.OnDestroyDelegate += SpawnBonus;
        }

        private void SpawnBonus() {
            EventBus.FireEvent<SpawnBonusEvent>(new SpawnBonusEvent() { SpawningPosition = transform.position });
        }
    }
}
