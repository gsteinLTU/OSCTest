using UnityEngine;

[RequireComponent(typeof(SendOSCMessage))]
public class SendTransformList : MonoBehaviour
{
    public Transform[] transforms;
    public int UpdateRate = 30;
    
    public enum CoordinateSystem
    {
        XYZ,
        AED,
    }

    [Tooltip("Coordinate system to use for sending transforms.")]
    public Transform ReferenceTransform;

    public CoordinateSystem coordinateSystem = CoordinateSystem.XYZ;

    private float lastUpdateTime;
    private SendOSCMessage sendOSCMessage;

    void Start()
    {
        lastUpdateTime = Time.time;
        sendOSCMessage = GetComponent<SendOSCMessage>();
        if (transforms == null || transforms.Length == 0)
        {
            Debug.LogError("No transforms assigned to SendTransformList.");
        }
        if (sendOSCMessage == null)
        {
            Debug.LogError("No SendOSCMessage component found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastUpdateTime >= 1f / UpdateRate)
        {
            SendTransforms();
            lastUpdateTime = Time.time;
        }        
    }

    void SendTransforms()
    {
        if (transforms == null || transforms.Length == 0)
        {
            Debug.LogError("No transforms assigned to SendTransformList.");
            return;
        }

        if (coordinateSystem == CoordinateSystem.XYZ)
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                sendOSCMessage.SendTransformAsXYZ(transforms[i], i, i);
            }
        } else if (coordinateSystem == CoordinateSystem.AED)
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                sendOSCMessage.SendTransformAsAED(transforms[i], ReferenceTransform ?? this.transform, i, i);
            }
        }
    }
}
