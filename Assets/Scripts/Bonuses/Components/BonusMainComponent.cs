using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    public enum BonusType
    {
        SpeedUp,
        SpeedDown,
        ExtraBall,
        WidenPaddle
    }
    public class BonusMainComponent : MonoBehaviour
    {
        [SerializeField]
        private BonusType type;
        [SerializeField]
        private float destructionPoint = -5;

        private void Update() {
            if (transform.position.y < destructionPoint) {
                DestroySelf();
            }
        }

        public Action onDestroyDelegate;
        public void DestroySelf() {
            onDestroyDelegate?.Invoke();
            Destroy(gameObject);
        }
    }
}
