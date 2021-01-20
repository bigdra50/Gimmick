using Bigdra.Util.Graph.Scripts;
using UnityEngine;

namespace Bigdra.PatternLock.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class NodeController : MonoBehaviour
    {
        private Node _node;
        private Collider _collider;
        private PatternLock _patternLock;
        private bool _isSelecting;

        private void Start()
        {
            _collider = GetComponent<Collider>();
            _patternLock = transform.parent.parent.GetComponent<PatternLock>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_patternLock.HandTagName)) return;
            //if (_isSelecting) return;
            _isSelecting = true;
            _patternLock.SelectNode(this, other.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(_patternLock.HandTagName)) return;
        }

        public void Release()
        {
            _isSelecting = false;
        }
    }
}