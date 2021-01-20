using System;
using System.Collections.Generic;

namespace Bigdra.Util.Graph.Scripts
{
    public interface IGraph
    {
        IReadOnlyList<Node> Nodes { get; }
        IReadOnlyDictionary<Node, List<Node>> AdjList { get; }
        event Action EdgeCountChanged;
        int EdgesCount { get; }
        bool TryAddNode(Node node);
        bool TryAddEdge(Node from, Node to);
        void RemoveNode(Node node);
        void RemoveAllNodes();
        void RemoveEdge(Node from, Node to);
        void RemoveAllEdges();
    }
}