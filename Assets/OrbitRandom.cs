using UnityEngine;

public class OrbitRandom : MonoBehaviour
{
    Vector3 nextPos;
    public float speed = 1f;
    public float radius = 1f;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = Random.onUnitSphere * radius;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, nextPos, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, nextPos) < 0.1f)
        {
            nextPos = Random.onUnitSphere * radius;
        }
        
    }
}
