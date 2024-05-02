using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float scrollSpeed = 10f;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void LateUpdate() {
        transform.position = new Vector3(target.transform.position.x,transform.position.y,target.transform.position.z);
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        Vector3 position = transform.position;

        // Change the Y position of the camera based on mouse scroll
        position.y -= scrollData * scrollSpeed;
        transform.position = position;
    }


}
