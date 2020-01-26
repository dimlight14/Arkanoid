using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{

    public class Paddle : MonoBehaviour
    {
        private void Start() {
            EventBus.FireEvent<PaddleRegisteredEvent>(new PaddleRegisteredEvent { PaddleObject = gameObject });
        }
    }
}
