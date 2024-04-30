using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float speed = 10.0f; // Speed of the movement
    public float distance = 5.0f; // Distance to move in units

    private Vector3 startPos;
    private Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; // Save the start position
        endPos = startPos + new Vector3(distance, 0, 0); // Calculate the end position
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new position
        float t = (Mathf.Sin(Time.time * speed) + 1) / 2; // t goes from 0 to 1
        transform.position = Vector3.Lerp(startPos, endPos, t); // Interpolate between startPos and endPos
    }

}
