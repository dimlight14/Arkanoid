using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    public static class CustomArkanoidPhysics
    {
        private static List<Collider2D> wallColliders = new List<Collider2D>();
        private static BoxCollider2D paddleCollider;

        public static void SetUp() {
            EventBus.Subscribe<BrickRegisteredEvent>(RegisterBrick);
            EventBus.Subscribe<BrickDestroyedEvent>(OnBrickDestroyed);
            EventBus.Subscribe<InitializeWallEvent>(RegisterWall);
            EventBus.Subscribe<PaddleRegisteredEvent>(RegisterPaddle);
        }

        private static void RegisterPaddle(PaddleRegisteredEvent customEvent) {
            paddleCollider = customEvent.PaddleObject.GetComponent<BoxCollider2D>();
        }
        private static void OnBrickDestroyed(BrickDestroyedEvent customEvent) {
            wallColliders.Remove(customEvent.Brick.GetComponent<Collider2D>());
        }
        private static void RegisterBrick(BrickRegisteredEvent customEvent) {
            wallColliders.Add(customEvent.Brick.GetComponent<Collider2D>());
        }
        private static void RegisterWall(InitializeWallEvent customEvent) {
            wallColliders.Add(customEvent.Initializer.GetComponent<Collider2D>());
        }

        public static Collider2D[] GetWallTypeColliders() {
            return wallColliders.ToArray();
        }
        public static BoxCollider2D GetPaddleCollider() {
            return paddleCollider;
        }

        public static void ClearAll() {
            wallColliders.Clear();
            paddleCollider = null;
        }
    }
}
