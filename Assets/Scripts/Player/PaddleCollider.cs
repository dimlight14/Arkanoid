using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{

    public class PaddleCollider : CustomBoxCollider
    {
        [SerializeField]
        private float reflectionCoeff = 1.3f;

        public override Vector2 ResolveCollision(Vector2 ballDirection, Vector3 previousBallPos) {
            float center = transform.position.x;
            float leftEdge = center - boxCollider.size.x / 2;
            float rightEdge = center + boxCollider.size.x / 2;
            float difference = previousBallPos.x - center;
            float ratio = difference / (boxCollider.size.x / 2);
            ratio *= reflectionCoeff;
            ratio = Mathf.Clamp(ratio, -reflectionCoeff, reflectionCoeff);
            return new Vector2(ratio, 1);
        }
    }
}
