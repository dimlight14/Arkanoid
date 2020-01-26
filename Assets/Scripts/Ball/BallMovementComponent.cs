using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Arkanoid
{

    public class BallMovementComponent : MonoBehaviour
    {
        [SerializeField]
        private float speed;
        public Vector2 Direction {
            get => direction;
            set {
                direction = value;
                direction.Normalize();
            }
        }
        [SerializeField]
        private float maxSpeed = 5;
        [SerializeField]
        private float minSpeed = 3.5f;

        private Vector2 previousPosition;
        public Vector2 PreviousPosition { get => previousPosition; private set => previousPosition = value; }
        private BallMainComponent mainComponent;

        private Vector2 direction = new Vector2(0.3f, 0.7f);
        private bool isMoving = false;


        private void Awake() {
            mainComponent = GetComponent<BallMainComponent>();
            mainComponent.OnDestroyDelegate += OnDestroyEvent;
            EventBus.Subscribe<GameStartedEvent>(StartLevel);
            EventBus.Subscribe<BallSpeedUpEvent>(OnSpeedUpEvent);
            EventBus.Subscribe<BallSlowDownEvent>(OnSpeedDown);
            direction.Normalize();
        }

        private void StartLevel(GameStartedEvent customEvent) {
            StartMoving();
        }

        private void OnSpeedUpEvent(BallSpeedUpEvent customEvent) {
            SetSpeed(speed + customEvent.Amount);
        }
        private void OnSpeedDown(BallSlowDownEvent customEvent) {
            SetSpeed(speed - customEvent.Amount);
        }
        private void SetSpeed(float newSpeed) {
            speed = newSpeed;
            if (speed < minSpeed) speed = minSpeed;
            if (speed > maxSpeed) speed = maxSpeed;
        }

        public void StartMoving() {
            isMoving = true;
        }

        private void Update() {
            if (!isMoving) return;

            PreviousPosition = transform.position;
            transform.Translate(direction * speed * Time.deltaTime);
            BindToSpace();
        }

        private float leftEdge = -2.9f;
        private float rightEdge = 2.9f;
        private float topEdge = 5.1f;
        private float leftSafeSpot = -2.5f;
        private float rightSafeSpot = 2.5f;
        private float topSafeSpot = 4.62f;
        private void BindToSpace() {
            if (transform.position.x < leftEdge) {
                transform.position = new Vector3(leftSafeSpot, transform.position.y, 0);
            }
            else if (transform.position.x > rightEdge) {
                transform.position = new Vector3(rightSafeSpot, transform.position.y, 0);
            }
            if (transform.position.y > topEdge) {
                transform.position = new Vector3(transform.position.x, topSafeSpot, 0);
            }
        }

        private void OnDestroyEvent() {
            EventBus.Unsubscribe<GameStartedEvent>(StartLevel);
            EventBus.Unsubscribe<BallSpeedUpEvent>(OnSpeedUpEvent);
            EventBus.Unsubscribe<BallSlowDownEvent>(OnSpeedDown);
        }
    }
}
