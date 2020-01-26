using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Arkanoid
{
    public enum BrickType
    {
        Normal,
        DoubleLife,
        Bonus,
        TripleLife
    }

    [SelectionBase]
    public class BrickMainComponent : MonoBehaviour
    {
        public BrickType type;


        private void Awake() {
            EventBus.Subscribe<GameStartedEvent>(OnGameStarted);
        }

        private void OnGameStarted(GameStartedEvent customEvent) {
            EventBus.FireEvent<BrickRegisteredEvent>(new BrickRegisteredEvent() { Brick = this });
        }

        public Action OnDestroyDelegate = delegate { };
        public void GetDestroyed() {
            EventBus.FireEvent<BrickDestroyedEvent>(new BrickDestroyedEvent() { Brick = this });
            EventBus.Unsubscribe<GameStartedEvent>(OnGameStarted);
            OnDestroyDelegate();
            Destroy(gameObject);
        }
    }
}
