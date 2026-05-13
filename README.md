# Fibonacci Acoustic Scanner for Unity & Wwise

**Procedural acoustic analysis system that drives Wwise RTPCs based on environmental geometry and surface materials.**

## Overview
This tool automates the acoustic properties of a scene by using Fibonacci raycasting to "sense" the surroundings. It calculates weighted absorption and average distance, sending these values directly to Wwise to control reverb decay, LPF, and dry/wet levels.

## Key Features
* **Fibonacci Sphere Distribution:** Uniform raycasting for accurate 360° environment sensing.
* **Material Awareness:** Detects Unity Tags (Wood, Carpet, Glass) and maps them to acoustic coefficients.
* **Sky Absorption:** Intelligently handles open-world scenarios by drying out reverb when rays hit the sky.
* **Dynamic Wwise Integration:** Real-time updates for `RTPC_AverageDistance` and `RTPC_AverageAbsorption`.

## How to Use
1.  Add `FibonacciAcousticScanner` to your Player/Listener.
2.  Assign an `AcousticMaterialConfig` (ScriptableObject) with your defined tags.
3.  Ensure your Wwise project has the corresponding RTPCs.
