#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace VertexFragment
{
    [CustomEditor(typeof(UniformPoissonSamplerVisualizer))]
    public sealed class UniformPoissonSamplerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Generate"))
            {
                ((UniformPoissonSamplerVisualizer)target).GenerateUniform();
            }

         }
    }
}
#endif