using UnityEngine;

public class BallLight : MonoBehaviour
{
    Transform target;

    void Start()
    {
        target = GameObject.Find("Ball").transform;
    }

    void Update()
    {
        transform.position = target.position;
    }
}
