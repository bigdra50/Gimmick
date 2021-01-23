using System;
using System.Collections.Generic;
using Bigdra.Util.Graph.Scripts;
using UnityEngine;

namespace Bigdra.PatternLock.Scripts
{
    [RequireComponent(typeof(NodeController))]
    public class PatternLock : MonoBehaviour
    {
        public IGraph CurrentGraph => _currentGraph;
        public string HandTagName => _handTagName;
        public event Action Unlock;
        public bool IsUnlocked { get; private set; }
        public IReadOnlyList<NodeController> NodeControllers => _nodeControllers;
        public IReadOnlyDictionary<NodeController, Node> NodeMap => _nodeMap;

        [SerializeField] private DrawableArea _drawableArea;
        [SerializeField] private string _handTagName = "Hand";
        [SerializeField] private Pattern _keyPattern;
        [SerializeField] private GameObject _edgesSpawnRoot;
        [SerializeField] private List<NodeController> _nodeControllers;
        [SerializeField] private Material _edgeMaterial;
        [SerializeField] private Material _unlockedEdgeMaterial;
        [SerializeField] private SeHandler _seHandler;

        private IGraph _targetGraph; // 正解パターンのグラフ
        private IGraph _currentGraph; // 現在描いているグラフ
        private Transform _handTransform;
        private NodeController _lastNode; // 最後に触れたノード
        private LineRenderer _handDrawingLine; // lastNodeと指の間のLineRenderer private void OnUnlock() => Unlock?.Invoke();
        private void OnUnlock() => Unlock?.Invoke();

        private List<(LineRenderer line, Transform[] nodes)>
            _updateEdgeList = new List<(LineRenderer, Transform[])>(); // 頂点位置が確定した辺のノードとLineRendererのペア

        private Dictionary<NodeController, Node> _nodeMap = new Dictionary<NodeController, Node>();

        private void Awake()
        {
            _targetGraph = new UndirectedGraph(_keyPattern.adjacencyMatrix);
            _currentGraph = new UndirectedGraph();
            for (var i = 0; i < _targetGraph.Nodes.Count; i++)
            {
                var targetGraphNode = _targetGraph.Nodes[i];
                _currentGraph.TryAddNode(targetGraphNode); // 正解のグラフに含まれるノードをこれから描くのに使うグラフのノードに追加
                _nodeMap[_nodeControllers[i]] = targetGraphNode; // ノードのモデルとコントローラーの対応付け
            }
        }

        private void Start()
        {
            Unlock += () =>
            {
                Debug.Log("Unlock");
                foreach (var edge in _updateEdgeList)
                {
                    edge.line.material = _unlockedEdgeMaterial;
                }
                _seHandler.PlayUnlockAudio();
                Destroy(_handDrawingLine);
            };
            _drawableArea.ReleaseHand += () => ReleaseHand();
        }

        private void Update()
        {
            UpdateDrawingLineByHand();
            UpdateConfirmedEdgeLine();
        }

        private void UpdateDrawingLineByHand()
        {
            if (IsUnlocked) return;
            if (_lastNode == null || _handTransform == null) return;
            if (!_drawableArea.CanDraw) return;
            _handDrawingLine.SetPositions(new[]
            {
                _lastNode.transform.position, _handTransform.position
            });
        }

        private void UpdateConfirmedEdgeLine()
        {
            if (_updateEdgeList.Count < 1) return;
            foreach (var edge in _updateEdgeList)
            {
                edge.line.SetPositions(new[]
                {
                    edge.nodes[0].position, edge.nodes[1].position
                });
            }

            if (!IsUnlocked)
            {
                if (!_targetGraph.Equals(_currentGraph)) return;
                IsUnlocked = true;
                OnUnlock();
            }
        }

        public void SelectNode(NodeController nodeController, Transform hand)
        {
            _handTransform = hand;
            if (_lastNode == null)
            {
                _handDrawingLine = InitLine();
            }
            else if (_currentGraph.TryAddEdge(_nodeMap[nodeController], _nodeMap[_lastNode]))
            {
                CreateEdgeLine(nodeController.transform, _lastNode.transform);
            }

            _lastNode = nodeController;
        }

        private void CreateEdgeLine(Transform from, Transform to)
        {
            var line = InitLine();
            _updateEdgeList.Add((line, new[] {from, to}));
        }

        private LineRenderer InitLine()
        {
            var obj = new GameObject($"edge");
            obj.transform.parent = _edgesSpawnRoot.transform;
            var line = obj.AddComponent<LineRenderer>();
            line.material = _edgeMaterial;
            line.startWidth = .1f;
            line.endWidth = .1f;
            return line;
        }

        private void ReleaseHand()
        {
            if (IsUnlocked) return;
            _currentGraph.RemoveAllEdges();
            _lastNode = null;
            _handTransform = null;
            foreach (var nodeController in _nodeControllers)
            {
                nodeController.Release();
            }

            _updateEdgeList.Clear();
            foreach (Transform e in _edgesSpawnRoot.transform)
            {
                Destroy(e.gameObject);
            }
        }
    }
}