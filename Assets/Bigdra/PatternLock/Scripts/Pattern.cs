using System;
using Bigdra.Util.Graph.Scripts;
using UnityEngine;

namespace Bigdra.PatternLock.Scripts
{
    [CreateAssetMenu(fileName = "PatternData", menuName = "Gimmick/PatternLock", order = 0)]
    public class Pattern : ScriptableObject
    {
        [Range(2, 10)] public static int N = 9;
        public Column[] columns = new Column[N];

        private bool[,] pattern;

        //public bool[,] pattern =
        //{
        //    {false, true, false, true, true, false, false, false, false},
        //    {true, false, true, true, true, true, false, false, false},
        //    {false, true, false, false, true, true, false, false, false},
        //    {true, true, false, false, true, false, true, true, false},
        //    {true, true, true, true, false, true, true, true, true},
        //    {false, true, true, false, true, false, false, true, true},
        //    {false, false, false, true, true, false, false, true, false},
        //    {false, false, false, true, true, true, true, false, true},
        //    {false, false, false, false, true, true, false, true, false}
        //};

        // 後でエディタ拡張で作成できるツールを作る
        public AdjacencyMatrix adjacencyMatrix;

        private void OnEnable()
        {
            Debug.Log("Awake");
            pattern = new bool[N, N];
            for (var i = 0; i < N; i++)
            {
                for (var j = 0; j < N; j++)
                {
                    pattern[i, j] = columns[i].rows[j];
                    Debug.Log(pattern[i, j]);
                }
            }

            adjacencyMatrix = new AdjacencyMatrix(pattern);
        }

        public void UpdateMatrix(bool[,] updatePattern)
        {
            pattern = updatePattern;
        }

        [Serializable]
        public class Column
        {
            public bool[] rows = new bool[N];
        }
    }
}