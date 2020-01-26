using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    [RequireComponent(typeof(BoxCollider2D), typeof(BonusMainComponent))]
    public class BonusActiveComponent : MonoBehaviour
    {
        private BonusMainComponent mainComponent;
        private BoxCollider2D boxCollider;

        private void Awake() {
            mainComponent = GetComponent<BonusMainComponent>();
            boxCollider = GetComponent<BoxCollider2D>();
        }
        private void LateUpdate() {
            BoxCollider2D paddleCollider = CustomArkanoidPhysics.GetPaddleCollider();
            if (paddleCollider == null) return;

            if (GetThisColliderRect().Overlaps(GetOtherColliderRect(paddleCollider))) {
                ActivateBonus();
            }
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

        protected virtual void ActivateBonus() {
            mainComponent.DestroySelf();
        }
    }
}
