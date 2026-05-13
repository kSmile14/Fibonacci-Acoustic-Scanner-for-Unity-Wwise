using UnityEngine;

[ExecuteInEditMode]
public class FibonacciAcousticScanner : MonoBehaviour
{
    public AcousticMaterialConfig config;
    
    [Header("Wwise Integration")]
    public AK.Wwise.RTPC averageDistanceRTPC; 
    public AK.Wwise.RTPC averageAbsorptionRTPC; 
    public bool updateWwise = true;

    [Header("Scan Settings")]
    [Range(16, 512)] public int rayCount = 64;
    public float maxDistance = 50f;
    [Range(0.01f, 0.5f)] public float scanInterval = 0.05f; 
    public LayerMask geometryMask;
    
    [Header("Debug Status")]
    public bool showGizmos = true;
    [ReadOnly] public float averageDistance;
    [ReadOnly] public float averageAbsorption; 

    private Vector3[] _rayDirections;
    private float _timer;

    void OnValidate() { _rayDirections = GenerateFibonacciDirections(rayCount); }

    void Update()
    {
        if (_rayDirections == null || _rayDirections.Length != rayCount)
            _rayDirections = GenerateFibonacciDirections(rayCount);

        _timer += Time.deltaTime;
        if (_timer >= scanInterval)
        {
            CalculateAcoustics();
            _timer = 0;
            
            if (Application.isPlaying && updateWwise)
            {
                averageDistanceRTPC?.SetGlobalValue(averageDistance);
                averageAbsorptionRTPC?.SetGlobalValue(averageAbsorption);
            }
        }
    }

    private void CalculateAcoustics()
    {
        if (config == null) return;

        float totalDist = 0;
        float totalAbs = 0;
        Vector3 origin = transform.position;

        for (int i = 0; i < rayCount; i++)
        {
            Vector3 worldDir = transform.TransformDirection(_rayDirections[i]);
            
            if (Physics.Raycast(origin, worldDir, out RaycastHit hit, maxDistance, geometryMask, QueryTriggerInteraction.Ignore))
            {
                totalDist += hit.distance;
                string currentTag = hit.collider.tag;

                
                if (i == 0 && Application.isPlaying) 
                    Debug.Log($"Ray hit: {hit.collider.name} with Tag: [{currentTag}]");

                Color dColor;
                float abs = config.GetAbsorption(currentTag, out dColor);
                
                
                if (currentTag != "Untagged" && dColor == Color.white) 
                {
                    dColor = Color.magenta; 
                }

                totalAbs += abs;
                if (showGizmos) Debug.DrawLine(origin, hit.point, dColor);
            }
            else
            {
                totalDist += maxDistance;
                totalAbs += config.skyAbsorption;
                if (showGizmos) Debug.DrawRay(origin, worldDir * maxDistance, Color.black);
            }
        }
        averageDistance = totalDist / rayCount;
        averageAbsorption = totalAbs / rayCount;
    }

    private Vector3[] GenerateFibonacciDirections(int count)
    {
        Vector3[] directions = new Vector3[count];
        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < count; i++)
        {
            float t = (float)i / count;
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = angleIncrement * i;
            directions[i] = new Vector3(Mathf.Sin(inclination) * Mathf.Cos(azimuth), 
                                        Mathf.Cos(inclination), 
                                        Mathf.Sin(inclination) * Mathf.Sin(azimuth));
        }
        return directions;
    }
}

public class ReadOnlyAttribute : PropertyAttribute { }