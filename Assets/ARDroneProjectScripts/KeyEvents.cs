using UnityEngine;
using UnityEngine.VR;

using Oculus2ARDrone;

public class KeyEvents : MonoBehaviour
{
    private bool flying;
    private bool oculusControl;
    private Drone drone;
    private Orientation oriantation;

    // Use this for initialization
    void Start()
    {
        flying = false;
        oculusControl = false;
        drone = new Drone();
        if (drone.connect())
            Debug.Log("Connected");
        oriantation = new Orientation();
        Debug.Log("Start finished");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 eulerAngles = InputTracking.GetLocalRotation(VRNode.Head).eulerAngles;

        //Change If the oculus is contorling the drone or keypresses
        if (Input.GetKeyDown(KeyCode.E))
        {
            oculusControl = !oculusControl;
            Debug.Log("Switch Control");
        }

        //Take and Land the drone with the space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (flying)
            {
                Debug.Log("Land");
                drone.land();
                flying = false;
            }
            else
            {
                Debug.Log("Takeoff");
                if (drone.takeoff())
                    Debug.Log("Taking off");
                flying = true;
            }
            
            //#########################################OVR THING################################################
            OVRManager.display.RecenterPose();
        }

        //Enter hover mode
        if (Input.GetKeyDown(KeyCode.Q))
        {
            drone.enterHoverMode();
            Debug.Log("Hover");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            oculusControl = false;
            drone.enterHoverMode();
            Debug.Log("Shift");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            drone.reconnect();
            Debug.Log("Return");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            oriantation.Gaz = .2f;
            drone.move(oriantation);
            Debug.Log("Up");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            oriantation.Gaz = -.2f;
            drone.move(oriantation);
            Debug.Log("Down");
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            oriantation.Gaz = 0.0f;
            drone.move(oriantation);
            Debug.Log("Down/Up");
        }

        //
        if (oculusControl)
        {
            //0,0,0 is origen stright ahead
            float roll;
            float pitch;
            float yaw;

            //Y is Yaw right is increase left is decrease
            if (eulerAngles.y >= 180)
                yaw = (eulerAngles.y - 360) / 180;
            else
                yaw = eulerAngles.y / 180;

            //X in Pitch all up 275 all down 60  275 - 60 = 145
            if (eulerAngles.x >= 180)
                pitch = -((eulerAngles.x - 360) / 180);
            else
                pitch = -(eulerAngles.x / 180);

            //Z is Roll right decrease 280 left is increase 70 = 150
            if (eulerAngles.z >= 180)
                roll = (eulerAngles.z - 360) / 180;
            else
                roll = eulerAngles.z / 180;

            oriantation.Roll = roll;
            oriantation.Pitch = pitch;
            oriantation.Yaw = yaw;

            drone.move(oriantation);
            Debug.Log("I am Oculus");
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                oriantation.Pitch = .2f;
                drone.move(oriantation);
                Debug.Log("not W");
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                oriantation.Roll = -.2f;
                drone.move(oriantation);
                Debug.Log("A");
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                oriantation.Pitch = -.2f;
                drone.move(oriantation);
                Debug.Log("not S");
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                oriantation.Roll = .2f;
                drone.move(oriantation);
                Debug.Log("D");
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                oriantation.Yaw = -.2f;
                drone.move(oriantation);
                Debug.Log("Left");
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                oriantation.Yaw = .2f;
                drone.move(oriantation);
                Debug.Log("Right");
            }

            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W))
            {
                oriantation.Pitch = 0.0f;
                drone.move(oriantation);
                Debug.Log("S");
            }

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                oriantation.Roll = 0.0f;
                drone.move(oriantation);
                Debug.Log("D");
            }

            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                oriantation.Yaw = 0.0f;
                drone.move(oriantation);
                Debug.Log("Right");
            }
        }

    }

}