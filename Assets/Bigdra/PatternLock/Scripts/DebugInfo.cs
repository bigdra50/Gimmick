using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Bigdra.PatternLock.Scripts
{
    public class DebugInfo : MonoBehaviour
    {
        [SerializeField] private PatternLock _patternLock;
        private List<TextMeshPro> _tmPros = new List<TextMeshPro>();

        private void Start()
        {
            if (!_patternLock) _patternLock = GetComponent<PatternLock>();
            foreach (var nodeController in _patternLock.NodeControllers)
            {
                var tmPro = nodeController.GetComponentInChildren<TextMeshPro>();
                tmPro.text = _patternLock.NodeMap[nodeController].Name;
                _tmPros.Add(tmPro);
            }
        }

        private void OnEnable()
        {
            foreach (var tmPro in _tmPros)
            {
                tmPro.enabled = true;
            }
        }

        private void OnDisable()
        {
            foreach (var tmPro in _tmPros)
            {
                tmPro.enabled = false;
            }
        }
    }
}