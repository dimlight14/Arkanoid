using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    public class WallInitializer : MonoBehaviour
    {

        private void Start() {
            EventBus.FireEvent<InitializeWallEvent>(new InitializeWallEvent() { Initializer = this });
        }

    }
}
