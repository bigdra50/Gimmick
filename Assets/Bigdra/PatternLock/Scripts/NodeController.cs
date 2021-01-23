using Bigdra.Util.Graph.Scripts;
using UnityEngine;

namespace Bigdra.PatternLock.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class NodeController : MonoBehaviour
    {
        public Node Node => _node;
        private Node _node;
        private PatternLock _patternLock;
        [SerializeField] private SeHandler seHandler;

        private void Start()
        {
            _patternLock = transform.parent.parent.GetComponent<PatternLock>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_patternLock.HandTagName)) return;
            _patternLock.SelectNode(this, other.transform);
            seHandler?.PlayTouchAudio();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(_patternLock.HandTagName)) return;
        }

        public void Release()
        {
        }
    }
}