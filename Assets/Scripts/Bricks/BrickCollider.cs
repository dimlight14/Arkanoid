using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    [RequireComponent(typeof(BrickHealthComponent))]
    public class BrickCollider : CustomBoxCollider
    {
        private BrickHealthComponent healthComponent;

        protected override void GetComponents() {
            base.GetComponents();
            healthComponent = GetComponent<BrickHealthComponent>();
        }


        public override Vector2 ResolveCollision(Vector2 ballDirection, Vector3 ballPrevPosition) {
            Vector2 returnValue = base.ResolveCollision(ballDirection, ballPrevPosition);
            healthComponent.LoseHealth();
            return returnValue;
        }

    }
}
