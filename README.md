#  Fibonacci Acoustic Scanner for Unity & Wwise

**A procedural acoustic analysis system that drives Wwise RTPCs based on environmental geometry and surface materials.**

---

##  Overview
This tool automates acoustic properties by using **Fibonacci raycasting** to "sense" the surroundings. It calculates weighted absorption and average distance, sending these values directly to Wwise.

![Acoustic Rays Demo](<img width="993" height="719" alt="Снимок экрана 2026-05-13 в 15 07 25" src="https://github.com/user-attachments/assets/4f099c52-535a-4e23-b9b9-0108e6d50d47" />
)

##  Key Features
* **Fibonacci Sphere Distribution:** Uniform 360° environment sensing.
* **Material Awareness:** Detects Unity Tags (Wood, Carpet, Glass, etc.) and maps them to absorption coefficients.
* **Sky Absorption:** Intelligent logic to prevent indoor reverb tails in outdoor areas.
* **Real-time Wwise Sync:** Drives `RTPC_AverageDistance` and `RTPC_AverageAbsorption`.

## Wwise Setup
To make it work, create two RTPCs in your Wwise project:
1. `RTPC_AverageDistance` (Range: 0 to Max Scanning Distance)
2. `RTPC_AverageAbsorption` (Range: 0.0 to 1.0)

## Installation
1.  Add `FibonacciAcousticScanner.cs` to your Player/Listener.
2.  Create an `AcousticMaterialConfig` ScriptableObject.
3.  Assign tags and their respective absorption values in the inspector.
4.  ## Demo Video
[![Watch the Video](https://www.youtube.com/watch?v=rV1Xj3n49dY)
