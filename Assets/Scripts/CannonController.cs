using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the WADS movement of a cannon type object.
/// Use arraow keys or wads
/// Mouse mode is entered by using LEFT SHIFT
/// 
/// Instead i've opted to use an existing FPS controller that modifies the Unity standard assets fps to have smooth mouse movement.
/// 
/// Written by: ARthur wollocko (arthurw@oxyvita.us)
/// </summary>
public class CannonController : MonoBehaviour {

    public float turretRotationSpeed = 20f;

    //All manipulations apply to the turret
    public Transform turret;

	void Start () {
		
	}
	
	void Update () {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * turretRotationSpeed;
            float vertical = Input.GetAxis("Vertical") * Time.deltaTime * turretRotationSpeed * -1f;

            turret.Rotate(new Vector3(vertical, horizontal, 0) * Time.deltaTime * turretRotationSpeed, Space.World);
        } else
        {
            turretRotationSpeed = 150f; //adjustment for mouse movement
            float verticalMouse = Input.GetAxis("Mouse Y") * -1f;
            turret.Rotate(new Vector3(verticalMouse, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * turretRotationSpeed, Space.World);
        }

    }
}
