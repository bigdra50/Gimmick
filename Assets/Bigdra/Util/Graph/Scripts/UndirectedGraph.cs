using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bigdra.Util.Graph.Scripts
{
    public class UndirectedGraph : IGraph
    {
        public IReadOnlyList<Node> Nodes => _nodes;
        public IReadOnlyDictionary<Node, List<Node>> AdjList => _adjList;

        public int EdgesCount => _edgesCount;

        public event Action EdgeCountChanged;


        private AdjacencyMatrix _adjacencyMatrix;
        private List<Node> _nodes = new List<Node>();
        private Dictionary<Node, List<Node>> _adjList = new Dictionary<Node, List<Node>>();
        private int _edgesCount;

        public UndirectedGraph()
        {
            _adjacencyMatrix = new AdjacencyMatrix(0);
        }

        public UndirectedGraph(AdjacencyMatrix adjMat)
        {
            for (var i = 0; i < adjMat.N; i++)
            {
                var node = new Node(i, i.ToString());
                TryAddNode(node);
            }

            for (var i = 0; i < adjMat.N; i++)
            {
                for (var j = i + 1; j < adjMat.N; j++)
                {
                    if (i == j) continue;
                    if (!adjMat.AdjMat[i, j]) continue;
                    TryAddEdge(_nodes[i], _nodes[j]);
                }

                _adjacencyMatrix = adjMat;
            }
        }

        private void OnEdgeCountChanged() => EdgeCountChanged?.Invoke();

        public bool TryAddNode(Node node)
        {
            // 頂点名の重複チェック
            if (_nodes.Any(n => node.Name == n.Name)) return false;

            //Debug.Log($"Add Node {node.Name}");
            _nodes.Add(node);
            return true;
        }

        public bool TryAddEdge(Node from, Node to)
        {
            if (from == to) return false;
            //Debug.Log($"Add Edge from {from.Name} to {to.Name}");
            if (!_adjList.ContainsKey(from))
            {
                _adjList[from] = new List<Node>();
            }

            if (!_adjList.ContainsKey(to))
            {
                _adjList[to] = new List<Node>();
            }

            if (_adjList[from].Contains(to) || _adjList[to].Contains(from)) return false;

            _adjList[from].Add(to);
            _adjList[to].Add(from);
            _edgesCount++;
            OnEdgeCountChanged();
            return true;
        }

        public void RemoveNode(Node node)
        {
        }

        public void RemoveAllNodes()
        {
            _nodes = new List<Node>();
        }

        public void RemoveEdge(Node from, Node to)
        {
        }

        public void RemoveAllEdges()
        {
            _adjList = new Dictionary<Node, List<Node>>();
            _edgesCount = 0;
        }

        private void UpdateAdjacencyMatrix()
        {
            if (_nodes.Count != _adjacencyMatrix.N)
            {
            }

            var adjMat = new bool[_nodes.Count, _nodes.Count];
            foreach (var adj in _adjList)
            {
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType()) return false;
            var other = (UndirectedGraph) obj;
            if (this.Nodes.Count != other._nodes.Count) return false;
            if (this.EdgesCount != other._edgesCount) return false;
            var orderedEnumerable = _adjList.OrderBy(selector => selector.Key.Number);

            var sample1 = new List<List<Node>>();
            foreach (var e in orderedEnumerable)
            {
                e.Value.Sort((a, b) => a.Number - b.Number);
                sample1.Add(e.Value);
            }

            var otherOrderedEnumerable = other.AdjList.OrderBy(selector => selector.Key.Number);
            var sample2 = new List<List<Node>>();
            foreach (var e in otherOrderedEnumerable)
            {
                e.Value.Sort((a, b) => a.Number - b.Number);
                sample2.Add(e.Value);
            }

            for (var i = 0; i < sample1.Count; i++)
            {
                if (sample1.Count != sample2.Count) return false;
                for (var j = 0; j < sample1[i].Count; j++)
                {
                    if (sample1[i][j].Number != sample2[i][j].Number) return false;
                }
            }

            return true;
        }
    }
}