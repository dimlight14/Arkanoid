using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CustomBoxCollider : MonoBehaviour
    {
        protected BoxCollider2D boxCollider;

        private float RightEdge {
            get => transform.position.x + boxCollider.size.x / 2;
        }
        private float TopEdge {
            get => transform.position.y + boxCollider.size.y / 2;
        }
        private Vector2 cachedDiagonal = Vector2.zero;
        private Vector2 UpRightDiagonal {
            get {
                if (cachedDiagonal == Vector2.zero) {
                    Vector2 corner = new Vector2(RightEdge, TopEdge);
                    Vector2 diagonal = corner - (Vector2)transform.position;
                    diagonal.Normalize();
                    cachedDiagonal = diagonal;
                }
                return cachedDiagonal;
            }
        }

        private void Awake() {
            GetComponents();
        }
        protected virtual void GetComponents() {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        public virtual Vector2 ResolveCollision(Vector2 ballDirection, Vector3 ballPrevPosition) {
            Vector2 fromCenterToBall = ballPrevPosition - transform.position;
            fromCenterToBall.Normalize();

            if (fromCenterToBall.x > UpRightDiagonal.x || fromCenterToBall.x < UpRightDiagonal.x * -1) {
                return SideCollision(ballDirection);
            }
            else {
                return BottomTopCollision(ballDirection);
            }
        }

        private Vector2 SideCollision(Vector2 ballDirection) {
            return new Vector2(ballDirection.x * -1, ballDirection.y);
        }
        private Vector2 BottomTopCollision(Vector2 ballDirection) {
            return new Vector2(ballDirection.x, ballDirection.y * -1);
        }
    }
}
