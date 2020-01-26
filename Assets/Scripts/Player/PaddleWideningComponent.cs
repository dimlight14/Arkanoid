using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    public class PaddleWideningComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject rendererObject;
        [SerializeField]
        private BoxCollider2D paddleCollider;
        [Space(20)]
        [Range(1, 2)]
        [SerializeField]
        private float wideningCoeff = 1.3f;
        [SerializeField]
        private uint wideningThreshold = 3;
        private int timesWidened = 0;

        private void Awake() {
            EventBus.Subscribe<BonusWidenPaddleEvent>(OnWidening);
        }

        private void OnWidening(BonusWidenPaddleEvent customEvent) {
            timesWidened++;
            if (timesWidened > wideningThreshold) return;

            float newXSCale = rendererObject.transform.localScale.x * wideningCoeff;
            rendererObject.transform.localScale = new Vector3(newXSCale, rendererObject.transform.localScale.y, 1);

            newXSCale = paddleCollider.size.x * wideningCoeff;
            paddleCollider.size = new Vector2(newXSCale, paddleCollider.size.y);
        }
    }
}
