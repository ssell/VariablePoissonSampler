#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace VertexFragment
{
    [CustomEditor(typeof(VariablePoissonSamplerVisualizer))]
    public sealed class VariablePoissonSamplerVisualizerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Generate"))
            {
                ((VariablePoissonSamplerVisualizer)target).GenerateVariable();
            }

        }
    }
}

#endif