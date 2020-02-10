using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    [RequireComponent(typeof(BoxCollider2D), typeof(BallMovementComponent))]
    public class BallColliderComponent : MonoBehaviour
    {
        private BoxCollider2D boxCollider;
        private BallMovementComponent movementComponent;
        private bool blockCollisions = false;
        private BoxCollider2D colliderInContact = null;

        private void Awake() {
            boxCollider = GetComponent<BoxCollider2D>();
            movementComponent = GetComponent<BallMovementComponent>();
        }

        private void FixedUpdate() {
            if (colliderInContact) {
                if (!CollidersAreTouching(GetOtherColliderRect(colliderInContact))) {
                    colliderInContact = null;
                }
            }

            foreach (Collider2D collider in CustomArkanoidPhysics.GetWallTypeColliders()) {
                if (collider == null) {
                    return;
                }

                if (collider != colliderInContact && CollidersAreTouching(GetOtherColliderRect((BoxCollider2D)collider))) {
                    colliderInContact = (BoxCollider2D)collider;
                    CustomBoxCollider wallScript = collider.gameObject.GetComponent<CustomBoxCollider>();
                    movementComponent.Direction = wallScript.ResolveCollision(movementComponent.Direction, movementComponent.PreviousPosition);
                    return;
                }
            }
        }


        private bool CollidersAreTouching(Rect otherColliderRect) {
            return GetThisColliderRect().Overlaps(otherColliderRect);
        }
        private Rect GetThisColliderRect() {
            Rect newRect = new Rect();
            newRect.width = boxCollider.size.x;
            newRect.height = boxCollider.size.y;
            Vector2 newPosition;
            newPosition.x = transform.position.x - boxCollider.size.x / 2;
            newPosition.y = transform.position.y - boxCollider.size.y / 2;
            newRect.position = newPosition;

            return newRect;
        }
        private Rect GetOtherColliderRect(BoxCollider2D otherBoxCollider) {
            Rect newRect = new Rect();
            newRect.width = otherBoxCollider.size.x;
            newRect.height = otherBoxCollider.size.y;
            Vector2 newPosition;
            newPosition.x = otherBoxCollider.transform.position.x - otherBoxCollider.size.x / 2;
            newPosition.y = otherBoxCollider.transform.position.y - otherBoxCollider.size.y / 2;
            newRect.position = newPosition;

            return newRect;
        }
    }
}
