using System.Collections.Generic;
using OscCore;
using UnityEngine;

public class SendOSCMessage : MonoBehaviour
{
    public string address = "text";

    public OscSender oscSender;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        if (oscSender == null)
        {
            oscSender = FindFirstObjectByType<OscSender>();
        }
        if (oscSender == null)
        {
            Debug.LogError("No OscSender found in the scene.");
        }
    }

    // Generic method to send OSC messages of various types
    public void Send<T>(string address, T message)
    {
        Debug.Log($"Sending OSC message to {address}: {message}");
        
        if (message is int intValue)
            oscSender.Client.Send(address, intValue);
        else if (message is long longValue)
            oscSender.Client.Send(address, longValue);
        else if (message is double doubleValue)
            oscSender.Client.Send(address, doubleValue);
        else if (message is char charValue)
            oscSender.Client.Send(address, charValue);
        else if (message is float floatValue)
            oscSender.Client.Send(address, floatValue);
        else if (message is string stringValue)
            oscSender.Client.Send(address, stringValue);
        else if (message is bool boolValue)
            oscSender.Client.Send(address, boolValue);
        else if (message is Vector2 vector2Value)
            oscSender.Client.Send(address, vector2Value);
        else if (message is Vector3 vector3Value)
            oscSender.Client.Send(address, vector3Value);
        else if (message is Color colorValue)
            oscSender.Client.Send(address, colorValue);
        else if (message is byte[] byteArrayValue)
            oscSender.Client.Send(address, byteArrayValue, byteArrayValue.Length);
        else if (message is MidiMessage midiMessage)
            oscSender.Client.Send(address, midiMessage);
        else if (message is float[] floats){
            if (floats.Length == 0)
                return;
            if (floats.Length == 1)
                oscSender.Client.Send(address, floats[0]);
            else if (floats.Length == 2)
                oscSender.Client.Send(address, floats[0], floats[1]);
            else if (floats.Length == 3)
                oscSender.Client.Send(address, floats[0], floats[1], floats[2]);
            else if (floats.Length == 4)
                oscSender.Client.Send(address, floats[0], floats[1], floats[2], floats[3]);
            else
                Debug.LogError($"Unsupported float array length: {floats.Length}");
        }
        else
            Debug.LogError($"Unsupported type for OSC message: {typeof(T)}");
    }

    // Non-generic methods for specific types (to allow use in Unity UI events)
    public void SendInt(int message) => Send(address, message);
    public void SendLong(long message) => Send(address, message);
    public void SendDouble(double message) => Send(address, message);
    public void SendChar(char message) => Send(address, message);
    public void SendFloat(float message) => Send(address, message);
    public void SendString(string message) => Send(address, message);
    public void SendBool(bool message) => Send(address, message);
    public void SendVector2(Vector2 message) => Send(address, message);
    public void SendVector3(Vector3 message) => Send(address, message);
    public void SendColor(Color message) => Send(address, message);
    public void SendFloatAsInt(float message) => Send(address, (int)message);

    public void SendXYZVectors(Vector3[] message) {
        if (message.Length == 0)
            return;

        for (int i = 0; i < message.Length; i++)
        {
            List<object> values = new List<object>
            {
                "xyz",
                i,
                message[i].x,
                message[i].z,
                message[i].y,
                0
            };
            oscSender.Client.SendCustomFormat(address, ",sifffi", values);
        }
    }

    public void SendTransformAsXYZ(Transform message, int index, int group = 0){
        if (message == null)
            return;
        
        
        List<object> values = new List<object>
        {
            "xyz",
            index,
            message.position.x,
            message.position.z,
            message.position.y,
            group
        };
        oscSender.Client.SendCustomFormat(address, ",sifffi", values);
    }

    public void SendTransformAsXYZ(Transform message){
        SendTransformAsXYZ(message, 0);
    }

    public void SendTransformAsAED(Transform message, Transform reference, int index, int group = 0){
        if (message == null || reference == null)
            return;
            
        Vector3 difference = message.position - reference.position;
        float distance = difference.magnitude;
        Vector3 normalizedDifference = difference.normalized;
        float azimuth = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        float elevation = Mathf.Asin(normalizedDifference.y) * Mathf.Rad2Deg;

        List<object> values = new List<object>
        {
            "aed",
            index,
            azimuth,
            elevation,
            distance,
            group
        };
        oscSender.Client.SendCustomFormat(address, ",sifffi", values);
    }
}
