using System;
using UnityEngine;

namespace Bigdra.PatternLock.Scripts
{
    public class DrawableArea : MonoBehaviour
    {
        public event Action ReleaseHand;
        public bool CanDraw { get; private set; }
        [SerializeField] private string _handTagName = "Hand";
        private void OnReleaseHand() => ReleaseHand?.Invoke();

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_handTagName)) return;
            CanDraw = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(_handTagName)) return;
            CanDraw = false;
            OnReleaseHand();
        }
    }
}