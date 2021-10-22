using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace VertexFragment
{
    public sealed class UniformPoissonSamplerNDVisualizer : PoissonSamplerVisualizerBase
    {
        public float Radius = 25.0f;

        public void GenerateUniform()
        {
            System.Random rng = new System.Random(Seed);
            UniformPoissonSamplerND sampler = new UniformPoissonSamplerND(rng, Radius, new float[2] { Width, Height }, RejectionLimit);

            Stopwatch sw = Stopwatch.StartNew();
            sampler.Generate();
            sw.Stop();

            UnityEngine.Debug.Log($"Poisson Generation complete in {sw.Elapsed.TotalMilliseconds} ms");

            List<Vector2> vectorSamplesList = new List<Vector2>(sampler.SamplesList.Count);

            foreach (var sample in sampler.SamplesList)
            {
                vectorSamplesList.Add(new Vector2(sample[0], sample[1]));
            }

            SaveToTexture(Width, Height, vectorSamplesList, "UniformPoissonDiskND");

            if (GenerateRandomComparison)
            {
                RandomComparison(rng, (int)sampler.GridDimensions[0], (int)sampler.GridDimensions[1], vectorSamplesList);
            }
        }
    }
}
