using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace VertexFragment
{
    public sealed class PoissonSamplerVisualizer : MonoBehaviour
    {
        public int Width = 1024;
        public int Height = 1024;
        public int RejectionLimit = 30;
        public int Seed = 1337;
        public bool GenerateRandomComparison = true;
        public float Radius = 25.0f;
        public Texture2D DensityMap;
        public float MinRadius = 1.0f;
        public float MaxRadius = 5.0f;

        // ---------------------------------------------------------------------------------
        // Uniform Poisson
        // ---------------------------------------------------------------------------------

        public void GenerateUniform()
        {
            System.Random rng = new System.Random(Seed);
            UniformPoissonSampler2D noise = new UniformPoissonSampler2D(rng, Radius, Width, Height, RejectionLimit);

            Stopwatch sw = Stopwatch.StartNew();
            noise.Generate();
            sw.Stop();

            UnityEngine.Debug.Log($"Poisson Generation complete in {sw.Elapsed.TotalMilliseconds} ms");

            SaveToTexture((int)noise.Width, (int)noise.Height, noise.SamplesList, "PoissonDisk");

            if (GenerateRandomComparison)
            {
                RandomComparison(rng, (int)noise.Width, (int)noise.Height, noise.SamplesList);
            }
        }

        // ---------------------------------------------------------------------------------
        // Variable Poisson
        // ---------------------------------------------------------------------------------

        public void GenerateVariable()
        {
            System.Random rng = new System.Random(Seed);
            VariablePoissonSampler2D noise = new VariablePoissonSampler2D(rng, Width, Height, RejectionLimit);

            Stopwatch sw = Stopwatch.StartNew();
            noise.Generate(GetVariableRadiusValue, MinRadius, MaxRadius);
            sw.Stop();

            UnityEngine.Debug.Log($"Poisson Generation complete in {sw.Elapsed.TotalMilliseconds} ms");

            SaveToTexture((int)noise.Width, (int)noise.Height, noise.Samples, "PoissonDiskVariable");

            if (GenerateRandomComparison)
            {
                RandomComparison(rng, (int)noise.Width, (int)noise.Height, noise.Samples);
            }
        }

        private float GetVariableRadiusValue(float x, float y)
        {
            if (DensityMap == null)
            {
                return Radius;
            }

            float u = (x / (float)Width);
            float v = (y / (float)Height);
            float r = DensityMap.GetPixelBilinear(u, v).r;

            return Mathf.Lerp(MinRadius, MaxRadius, r);
        }

        // ---------------------------------------------------------------------------------
        // Utils
        // ---------------------------------------------------------------------------------

        private void SaveToTexture(int width, int height, List<Vector2> samples, string filename)
        {
            Texture2D texture = new Texture2D(width + 16, height + 16);
            TextureUtils.Clear(texture, new Color(0.98f, 0.98f, 0.98f, 0.0f));

            foreach (var point in samples)
            {
                TextureUtils.AddPoint(texture, (int)point.x + 4, (int)point.y + 4, 3, new Color(0.4f, 0.4f, 0.4f));
                TextureUtils.AddPoint(texture, (int)point.x + 4, (int)point.y + 4, 1, new Color(0.98f, 0.98f, 0.98f));
            }

            texture.Apply();

            TextureUtils.DebugSavePng(texture, filename);
        }

        private void RandomComparison(System.Random rng, int width, int height, List<Vector2> samples)
        {
            List<Vector2> random = new List<Vector2>(samples.Count);

            for (int i = 0; i < samples.Count; ++i)
            {
                random.Add(new Vector2(
                    (float)rng.NextDouble() * width,
                    (float)rng.NextDouble() * height));
            }

            SaveToTexture((int)width, (int)height, random, "PoissonDisk_RandomCompare");
        }

    }
}
