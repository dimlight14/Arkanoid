using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Text levelName;

        private void Awake() {
            EventBus.Subscribe<GameStartedEvent>(OnGameStarted);
        }

        private void OnGameStarted(GameStartedEvent customeEvent) {
            levelName.gameObject.SetActive(false);
        }
    }
}
