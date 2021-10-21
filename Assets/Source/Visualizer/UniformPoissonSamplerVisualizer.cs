using System.Diagnostics;

namespace VertexFragment
{
    public sealed class UniformPoissonSamplerVisualizer : PoissonSamplerVisualizerBase
    {
        public float Radius = 25.0f;

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
    }
}
