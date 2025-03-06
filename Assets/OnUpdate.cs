using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class OnUpdate : MonoBehaviour
{
    public UnityEvent onUpdate;

    // Update is called once per frame
    void Update()
    {
        onUpdate?.Invoke();        
    }
}
