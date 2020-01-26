using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PaddleMovementComponent : MonoBehaviour
    {
        [SerializeField]
        private float speed = 2;
        [SerializeField]
        private float leftEdge, rightEdge;
        private BoxCollider2D boxCollider;

        private bool recievingInput = false;

        private void Awake() {
            EventBus.Subscribe<GameStartedEvent>(OnGameStarted);
            boxCollider = GetComponent<BoxCollider2D>();
        }

        private void OnGameStarted(GameStartedEvent customEvent) {
            recievingInput = true;
        }

        private void Update() {
            if (recievingInput) Move();
        }
        private void Move() {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, transform.position.y, 0);
            CheckBoundaries();
        }
        private void CheckBoundaries() {
            if (transform.position.x - boxCollider.size.x / 2 < leftEdge) {
                transform.position = new Vector3(leftEdge + boxCollider.size.x / 2, transform.position.y, 0);
            }
            else if (transform.position.x + boxCollider.size.x / 2 > rightEdge) {
                transform.position = new Vector3(rightEdge - boxCollider.size.x / 2, transform.position.y, 0);
            }
        }
    }
}
