using Bigdra.PatternLock.Scripts;
using UnityEditor;
using UnityEngine;

namespace Bigdra.PatternLock.Editor
{
    [CustomEditor(typeof(Pattern))]
    public class CustomScriptInspector : UnityEditor.Editor
    {
        private Pattern _pattern;

        private void OnEnable()
        {
            _pattern = target as Pattern;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.LabelField("隣接行列");

            Pattern.N = EditorGUILayout.IntSlider("N", Pattern.N, 2, 10);
            GUILayout.Space(20);

            if (_pattern.columns.Length < Pattern.N)
            {
                _pattern.columns = new Pattern.Column[Pattern.N];
            }

            EditorGUILayout.BeginHorizontal();
            for (var x = 0; x < Pattern.N; x++)
            {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField(x.ToString(), GUILayout.Width(15));
                for (var y = 0; y < Pattern.N; y++)
                {
                    if (_pattern.columns[x] == null) continue;

                    if (x == y)
                    {
                        _pattern.columns[x].rows[y] = EditorGUILayout.Toggle(true);
                    }
                    else if (x > y)
                    {
                        _pattern.columns[x].rows[y] = EditorGUILayout.Toggle(_pattern.columns[x].rows[y]);
                        _pattern.columns[y].rows[x] = _pattern.columns[x].rows[y];
                    }
                }

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndHorizontal();
            GUILayout.Space(20);

            EditorGUILayout.BeginHorizontal();
            {
                ResetButton();
                SelectAllButton();
            }
            EditorGUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_pattern, _pattern.name);
                EditorUtility.SetDirty(_pattern);
            }
        }

        private void ResetButton()
        {
            if (!GUILayout.Button("Reset", EditorStyles.miniButtonLeft)) return;
            EditorGUILayout.BeginHorizontal();
            for (var y = 0; y < Pattern.N; y++)
            {
                EditorGUILayout.BeginVertical();
                for (var x = 0; x < Pattern.N; x++)
                {
                    if (_pattern.columns[x] == null) continue;

                    if (x == y)
                    {
                        _pattern.columns[x].rows[y] = EditorGUILayout.Toggle(true);
                    }

                    _pattern.columns[x].rows[y] = EditorGUILayout.Toggle(false);
                }

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndHorizontal();
        }

        private void SelectAllButton()
        {
            if (!GUILayout.Button("Select All", EditorStyles.miniButtonRight)) return;
            EditorGUILayout.BeginHorizontal();
            for (var y = 0; y < Pattern.N; y++)
            {
                EditorGUILayout.BeginVertical();
                for (var x = 0; x < Pattern.N; x++)
                {
                    if (_pattern.columns[x] == null) continue;

                    _pattern.columns[x].rows[y] = EditorGUILayout.Toggle(true);
                }

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}