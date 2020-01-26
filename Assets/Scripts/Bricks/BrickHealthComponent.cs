using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    [RequireComponent(typeof(BrickMainComponent))]
    public class BrickHealthComponent : MonoBehaviour
    {
        [SerializeField]
        private uint brickHealth = 1;

        private BrickMainComponent brickMain;

        private void Awake() {
            if (brickHealth == 0) brickHealth = 1;

            brickMain = GetComponent<BrickMainComponent>();
        }

        public void LoseHealth() {
            brickHealth--;
            if (brickHealth == 0) {
                DestroySelf();
            }
        }
        private void DestroySelf() {
            brickMain.GetDestroyed();
        }
    }
}
