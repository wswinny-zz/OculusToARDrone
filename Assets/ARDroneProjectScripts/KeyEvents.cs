using UnityEngine;
using System.Collections;

using ARDrone.Control;
using ARDrone.Control.Commands;
using ARDrone.Control.Data;
using ARDrone.Control.Events;

public class KeyEvents : MonoBehaviour
{

    public DroneControl droneControl = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OVRManager.display.RecenterPose();
        }
    }
}
