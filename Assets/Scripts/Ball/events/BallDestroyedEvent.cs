using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    public class BallDestroyedEvent : CustomEvent
    {
        public BallMainComponent Ball;
    }
}
