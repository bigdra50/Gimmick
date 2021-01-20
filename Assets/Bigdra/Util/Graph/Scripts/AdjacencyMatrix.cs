namespace Bigdra.Util.Graph.Scripts
{
    public class AdjacencyMatrix
    {
        public int N => _n;

        public bool[,] AdjMat => _adjMat;

        private readonly int _n;
        private readonly bool[,] _adjMat;

        public AdjacencyMatrix(int n)
        {
            _n = n;
            _adjMat = new bool[n, n];
        }

        public AdjacencyMatrix(bool[,] adjMat)
        {
            _n = adjMat.GetLength(0);
            _adjMat = adjMat;
        }
    }
}