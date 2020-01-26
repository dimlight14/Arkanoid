using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    public class BonusMovementComponent : MonoBehaviour
    {
        [SerializeField]
        private float speed = 3;

        private void Update() {
            transform.Translate(new Vector3(0, speed * Time.deltaTime * -1), 0);
        }

    }
}
