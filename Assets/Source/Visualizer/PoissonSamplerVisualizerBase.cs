using System.Collections.Generic;
using UnityEngine;

namespace VertexFragment
{
    public abstract class PoissonSamplerVisualizerBase : MonoBehaviour
    {
        public int Width = 1024;
        public int Height = 1024;
        public int RejectionLimit = 30;
        public int Seed = 1337;
        public bool GenerateRandomComparison = true;

        protected void SaveToTexture(int width, int height, List<Vector2> samples, string filename)
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

        protected void RandomComparison(System.Random rng, int width, int height, List<Vector2> samples)
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
