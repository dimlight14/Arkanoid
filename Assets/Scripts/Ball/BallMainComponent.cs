using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    [RequireComponent(typeof(BallMovementComponent))]
    public class BallMainComponent : MonoBehaviour
    {
        [SerializeField]
        private float lowestYPossible = -2;
        [SerializeField]
        private float extraBallXDir = 0.7f;
        private float extraBallYDir = 0.8f;

        private BallMovementComponent movementComponent;

        private void Awake() {
            movementComponent = GetComponent<BallMovementComponent>();
        }

        public void Start() {
            EventBus.FireEvent<BallRegisteredEvent>(new BallRegisteredEvent() { Ball = this });
        }

        public void SetUpAsExtraBall() {
            float newBallXDir = UnityEngine.Random.Range(-extraBallXDir, extraBallXDir);
            movementComponent.Direction = new Vector2(newBallXDir, extraBallYDir);
            movementComponent.StartMoving();
        }

        private void Update() {
            if (transform.position.y < lowestYPossible) {
                DestroySelf();
            }
        }

        public Action OnDestroyDelegate;
        private void DestroySelf() {
            OnDestroyDelegate?.Invoke();
            EventBus.FireEvent<BallDestroyedEvent>(new BallDestroyedEvent() { Ball = this });
            Destroy(gameObject);
        }
    }
}
