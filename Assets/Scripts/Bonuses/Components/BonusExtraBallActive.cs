using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    public class BonusExtraBallActive : BonusActiveComponent
    {
        protected override void ActivateBonus() {
            EventBus.FireEvent<SpawnExtraBallEvent>();
            base.ActivateBonus();
        }
    }
}
