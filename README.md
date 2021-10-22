# Variable Poisson-Disk Sampler

<p align="center">
  <img width="512" height="512" src="https://media.githubusercontent.com/media/ssell/VariablePoissonSampler/main/Media/PoissonDiskVariable_StanfordBunny4096_6p5_50.png?token=AAFTHQOY3P4BBY4DCWVIWVTBOJC7I">
</p>

This repository contains a reference implementation of several Poisson-Disk Samplers implemented in C# and using Unity3D to visualize them. It contains the following samplers:

* `UniformPoissonSampler2D`
* `VariablePoissonSampler2D`
* `UniformPoissonSamplerND`

This is an accompaniment to the article: 

https://www.vertexfragment.com/ramblings/variable-density-poisson-sampler

Note that the 2D samplers make use of certain Unity classes and utilities, whereas the N-Dimension sampler is completely standalone and can be added to any C# project. However the 2D specializations run about 4x faster for that dimension.

__The code is for reference and educational purposes only. No guarantee is given towards the stability, correctness, or well any other characteristic by which it could be measured.__