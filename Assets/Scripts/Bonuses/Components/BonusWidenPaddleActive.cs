using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    public class BonusWidenPaddleActive : BonusActiveComponent
    {
        protected override void ActivateBonus() {
            EventBus.FireEvent<BonusWidenPaddleEvent>();
            base.ActivateBonus();
        }
    }
}
