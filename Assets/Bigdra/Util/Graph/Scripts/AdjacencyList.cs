using System.Collections.Generic;

namespace Bigdra.Util.Graph.Scripts
{
    public class AdjacencyList
    {
        public IReadOnlyDictionary<Node, List<Node>> AdjList => _adjList;

        private readonly Dictionary<Node, List<Node>> _adjList;

        public AdjacencyList(int n)
        {
            _adjList = new Dictionary<Node, List<Node>>();
        }
        public AdjacencyList(Dictionary<Node, List<Node>> adjList)
        {
            _adjList = adjList;
        }
    }
}