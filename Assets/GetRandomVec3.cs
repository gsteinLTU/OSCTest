using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GetRandomVec3 : MonoBehaviour
{
    public UnityEvent<Vector3> onGetRandomVec3;
    public UnityEvent<Vector3[]> onGetRandomVec3s;

    public void GetRandomVec3Value()
    {
        Vector3 randomVec3 = Random.insideUnitSphere;
        onGetRandomVec3?.Invoke(randomVec3);
    }

    public void GetRandomVec3Values(int count)
    {
        List<Vector3> randomVec3s = new List<Vector3>();
        for (int i = 0; i < count; i++)
        {
            randomVec3s.Add(Random.insideUnitSphere);            
        }
        onGetRandomVec3s?.Invoke(randomVec3s.ToArray());
    }
}
