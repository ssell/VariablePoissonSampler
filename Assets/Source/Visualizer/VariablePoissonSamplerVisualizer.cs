using System.Diagnostics;
using UnityEngine;

namespace VertexFragment
{
    public sealed class VariablePoissonSamplerVisualizer : PoissonSamplerVisualizerBase
    {
        public Texture2D DensityMap;
        public float MinRadius = 10.0f;
        public float MaxRadius = 50.0f;


        public void GenerateVariable()
        {
            System.Random rng = new System.Random(Seed);
            VariablePoissonSampler2D noise = new VariablePoissonSampler2D(rng, Width, Height, RejectionLimit);

            Stopwatch sw = Stopwatch.StartNew();
            noise.Generate(GetVariableRadiusValue, MinRadius, MaxRadius);
            sw.Stop();

            UnityEngine.Debug.Log($"Poisson Generation complete in {sw.Elapsed.TotalMilliseconds} ms");

            SaveToTexture((int)noise.Width, (int)noise.Height, noise.Samples, "VariablePoissonDisk");

            if (GenerateRandomComparison)
            {
                RandomComparison(rng, (int)noise.Width, (int)noise.Height, noise.Samples);
            }
        }

        private float GetVariableRadiusValue(float x, float y)
        {
            if (DensityMap == null)
            {
                return MaxRadius;
            }

            float u = (x / (float)Width);
            float v = (y / (float)Height);
            float r = DensityMap.GetPixelBilinear(u, v).r;

            return Mathf.Lerp(MinRadius, MaxRadius, r);
        }
    }
}
