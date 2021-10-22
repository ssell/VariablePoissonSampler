#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace VertexFragment
{
    [CustomEditor(typeof(UniformPoissonSamplerNDVisualizer))]
    public sealed class UniformPoissonSamplerNDInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Generate"))
            {
                ((UniformPoissonSamplerNDVisualizer)target).GenerateUniform();
            }

        }
    }
}
#endif