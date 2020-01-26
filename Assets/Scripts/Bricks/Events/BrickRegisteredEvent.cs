using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    public class BrickRegisteredEvent : CustomEvent
    {
        public BrickMainComponent Brick;
    }
}
