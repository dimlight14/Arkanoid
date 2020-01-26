using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    public class BonusSlowDownActive : BonusActiveComponent
    {
        [SerializeField]
        private float slowDownAmount = 1;

        protected override void ActivateBonus() {
            EventBus.FireEvent<BallSlowDownEvent>(new BallSlowDownEvent() { Amount = slowDownAmount });
            base.ActivateBonus();
        }
    }
}
