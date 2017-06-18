using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject followTarget;//Marine is follow target
    public float moveSpeed;//We set 20 because Marine always goes faster than Camera
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget != null)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.transform.position, Time.deltaTime * moveSpeed);
            //Lerp have a start position and end position in 3D. Camera to Marine 
            //makes the camera mount position smoothly move to where the player is over time.

        }
    }
}
