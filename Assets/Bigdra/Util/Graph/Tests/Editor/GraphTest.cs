using Bigdra.Util.Graph.Scripts;
using NUnit.Framework;

namespace Tests
{
    public class GraphTest
    {
        [Test]
        public void GraphConstructionFromAdjMatrixTest1()
        {
            var adjMat = new AdjacencyMatrix(new[,]
            {
                {false, true, false, false},
                {true, false, true, true},
                {false, true, false, false},
                {false, true, false, false}
            });
            var graph = new UndirectedGraph(adjMat);
            Assert.AreEqual(4, graph.Nodes.Count);
            Assert.AreEqual(1, graph.AdjList[graph.Nodes[0]].Count);
            Assert.AreEqual(3, graph.AdjList[graph.Nodes[1]].Count);
            Assert.AreEqual(1, graph.AdjList[graph.Nodes[2]].Count);
            Assert.AreEqual(1, graph.AdjList[graph.Nodes[3]].Count);
            Assert.AreEqual(3, graph.EdgesCount);
        }

        [Test]
        public void GraphConstructionFromAdjMatrixTest2()
        {
            var adjMat = new AdjacencyMatrix(new[,]
            {
                {false, true, false, true, true, false, false, false, false},
                {true, false, true, true, true, true, false, false, false},
                {false, true, false, false, true, true, false, false, false},
                {true, true, false, false, true, false, true, true, false},
                {true, true, true, true, false, true, true, true, true},
                {false, true, true, false, true, false, false, true, true},
                {false, false, false, true, true, false, false, true, false},
                {false, false, false, true, true, true, true, false, true},
                {false, false, false, false, true, true, false, true, false}
            });

            var graph = new UndirectedGraph(adjMat);
            Assert.AreEqual(20, graph.EdgesCount);
        }

        [Test]
        public void AddNodeTest()
        {
            var graph = new UndirectedGraph();
            graph.TryAddNode(new Node(0, 0.ToString()));
            graph.TryAddNode(new Node(1, 1.ToString()));
            graph.TryAddNode(new Node(2, 2.ToString()));
            graph.TryAddNode(new Node(3, 3.ToString()));
            Assert.AreEqual(4, graph.Nodes.Count);
            graph.TryAddNode(new Node(0, 0.ToString()));
            Assert.AreEqual(4, graph.Nodes.Count);
        }

        [Test]
        public void AddEdgeTest()
        {
            var graph = new UndirectedGraph();
            var n0 = new Node(0, 0.ToString());
            var n1 = new Node(1, 1.ToString());
            var n2 = new Node(2, 2.ToString());
            var n3 = new Node(3, 3.ToString());
            graph.TryAddNode(n0);
            graph.TryAddNode(n1);
            graph.TryAddNode(n2);
            graph.TryAddNode(n3);

            graph.TryAddEdge(n0, n1);
            Assert.AreEqual("1", graph.AdjList[n0][0].Name);
            Assert.AreEqual(n1, graph.AdjList[n0][0]);
            Assert.AreEqual("0", graph.AdjList[n1][0].Name);
            Assert.AreEqual(n0, graph.AdjList[n1][0]);
            Assert.AreNotEqual("n1", graph.AdjList[n1][0]);
            Assert.AreNotEqual(n1, graph.AdjList[n1][0]);
            Assert.AreEqual(1, graph.EdgesCount);
            graph.TryAddEdge(n1, n2);
            Assert.AreEqual("2", graph.AdjList[n1][1].Name);
            Assert.AreEqual(n2, graph.AdjList[n1][1]);
            Assert.AreEqual("1", graph.AdjList[n2][0].Name);
            Assert.AreEqual(n1, graph.AdjList[n2][0]);
            Assert.AreEqual(2, graph.EdgesCount);
            graph.TryAddEdge(n1, n3);
            Assert.AreEqual("3", graph.AdjList[n1][2].Name);
            Assert.AreEqual(n3, graph.AdjList[n1][2]);
            Assert.AreEqual("1", graph.AdjList[n3][0].Name);
            Assert.AreEqual(n1, graph.AdjList[n3][0]);
            Assert.AreEqual(3, graph.EdgesCount);
        }

        [Test]
        public void OverlappingEdgeTest1()
        {
            var graph = new UndirectedGraph();
            var n0 = new Node(0, 0.ToString());
            var n1 = new Node(1, 1.ToString());
            graph.TryAddNode(n0);
            graph.TryAddNode(n1);
            Assert.AreEqual(true, graph.TryAddEdge(n0, n1));
            Assert.AreEqual(false, graph.TryAddEdge(n1, n0));
        }

        [Test]
        public void OverlappingEdgeTest2()
        {
            var graph = new UndirectedGraph();
            var n0 = new Node(0, 0.ToString());
            var n1 = new Node(1, 1.ToString());
            graph.TryAddNode(n0);
            graph.TryAddNode(n1);
            Assert.AreEqual(true, graph.TryAddEdge(n0, n1));
            Assert.AreEqual(false, graph.TryAddEdge(n0, n1));
        }

        [Test]
        public void EqualsTest()
        {
            var graph1 = new UndirectedGraph();
            var n1_0 = new Node(0, 0.ToString());
            var n1_1 = new Node(1, 1.ToString());
            graph1.TryAddNode(n1_0);
            graph1.TryAddNode(n1_1);
            var graph2 = new UndirectedGraph();
            var n2_0 = new Node(0, 0.ToString());
            var n2_1 = new Node(1, 1.ToString());
            graph2.TryAddNode(n2_0);
            graph2.TryAddNode(n2_1);
            Assert.AreEqual(true, graph1.Equals(graph2));
            Assert.AreEqual(true, graph2.Equals(graph1));
            graph1.TryAddEdge(n1_0, n1_1);
            Assert.AreEqual(false, graph1.Equals(graph2));
            Assert.AreEqual(false, graph2.Equals(graph1));
            graph2.TryAddEdge(n2_0, n2_1);
            Assert.AreEqual(true, graph1.Equals(graph2));
            Assert.AreEqual(true, graph2.Equals(graph1));
            var graph3 = new UndirectedGraph();
            var n3_0 = new Node(0, 0.ToString());
            var n3_1 = new Node(1, 1.ToString());
            graph3.TryAddNode(n3_0);
        }

        [Test]
        public void EqualsTest2()
        {
            var adjMat1 = new AdjacencyMatrix(new[,]
            {
                {false, true, false, false},
                {true, false, true, true},
                {false, true, false, false},
                {false, true, false, false}
            });
            var graph1 = new UndirectedGraph(adjMat1);

            var adjMat2 = new AdjacencyMatrix(new[,]
            {
                {false, true, false, true},
                {true, false, true, true},
                {false, true, false, false},
                {true, true, false, false}
            });
            var graph2 = new UndirectedGraph(adjMat2);

            Assert.AreEqual(false, graph1.Equals(graph2));

            var adjMat3 = new AdjacencyMatrix(new[,]
            {
                {false, false, false, true},
                {false, false, true, true},
                {false, true, false, false},
                {true, true, false, false}
            });
            var graph3 = new UndirectedGraph(adjMat3);

            Assert.AreEqual(false, graph1.Equals(graph3));
            var graph4 = new UndirectedGraph();
            var graph5 = new UndirectedGraph();
            var n0 = new Node(0, 0.ToString());
            var n1 = new Node(1, 1.ToString());
            var n2 = new Node(2, 2.ToString());
            var n3 = new Node(3, 3.ToString());
            graph4.TryAddNode(n2);
            graph4.TryAddNode(n1);
            graph4.TryAddNode(n0);
            graph4.TryAddNode(n3);
            graph5.TryAddNode(n0);
            graph5.TryAddNode(n1);
            graph5.TryAddNode(n2);
            graph5.TryAddNode(n3);
            Assert.AreEqual(true, graph4.Equals(graph5));
            graph4.TryAddEdge(n1, n2);
            graph4.TryAddEdge(n3, n1);
            graph4.TryAddEdge(n1, n0);
            Assert.AreEqual(true, graph1.Equals(graph4));
            graph5.TryAddEdge(n0, n1);
            graph5.TryAddEdge(n2, n1);
            graph5.TryAddEdge(n1, n3);
            Assert.AreEqual(true, graph4.Equals(graph5));
        }
    }
}