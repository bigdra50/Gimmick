namespace Bigdra.Util.Graph.Scripts
{
    public class Node
    {
        public string Name => _name;
        public int Number => _number;
        private string _name;
        private int _number;

        public Node(int num, string name)
        {
            _number = num;
            _name = name;
        }

        public void Rename(string name)
        {
            _name = name;
        }
    }
}