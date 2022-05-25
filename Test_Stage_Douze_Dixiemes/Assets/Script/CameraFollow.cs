using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject toFollow;
    public float smoothTime = 0.3f;
    private Vector3 velocity= Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        Vector3 target = toFollow.transform.position + new Vector3(0, 20, -9
        );
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);

    }
}
