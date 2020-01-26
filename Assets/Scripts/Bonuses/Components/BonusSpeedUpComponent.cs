using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    public class BonusSpeedUpComponent : BonusActiveComponent
    {
        [SerializeField]
        private float speedChange = 0.5f;

        protected override void ActivateBonus() {
            EventBus.FireEvent<BallSpeedUpEvent>(new BallSpeedUpEvent() { Amount = speedChange });
            base.ActivateBonus();
        }
    }
}
