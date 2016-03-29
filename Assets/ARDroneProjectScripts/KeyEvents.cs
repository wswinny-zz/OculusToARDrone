using UnityEngine;
using UnityEngine.VR;
using System.Collections;

using ARDrone.Control;
using ARDrone.Control.Commands;
using ARDrone.Control.Data;
using ARDrone.Control.Events;

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
            oriantation.setGaz(.2f);
            drone.move(oriantation);
            Debug.Log("Up");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            oriantation.setGaz(-.2f);
            drone.move(oriantation);
            Debug.Log("Down");
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            oriantation.setGaz(0.0f);
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

            oriantation.setRoll(roll);
            oriantation.setPitch(pitch);
            oriantation.setYaw(yaw);

            drone.move(oriantation);
            Debug.Log("I am Oculus");
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                oriantation.setPitch(.2f);
                drone.move(oriantation);
                Debug.Log("W");
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                oriantation.setRoll(-.2f);
                drone.move(oriantation);
                Debug.Log("A");
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                oriantation.setPitch(-.2f);
                drone.move(oriantation);
                Debug.Log("S");
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                oriantation.setRoll(.2f);
                drone.move(oriantation);
                Debug.Log("D");
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                oriantation.setYaw(-.2f);
                drone.move(oriantation);
                Debug.Log("Left");
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                oriantation.setYaw(.2f);
                drone.move(oriantation);
                Debug.Log("Right");
            }

            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W))
            {
                oriantation.setPitch(0.0f);
                drone.move(oriantation);
                Debug.Log("S");
            }

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                oriantation.setRoll(0.0f);
                drone.move(oriantation);
                Debug.Log("D");
            }

            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                oriantation.setYaw(0.0f);
                drone.move(oriantation);
                Debug.Log("Right");
            }
        }

    }

}

class Orientation
{
    private float roll;
    private float pitch;
    private float yaw;
    private float gaz;

    public Orientation()
    {
        roll = 0;
        pitch = 0;
        yaw = 0;
        gaz = 0;
    }

    public void setRoll(float r)
    {
        this.roll = r;
    }

    public float getRoll()
    {
        return this.roll;
    }

    public void setPitch(float p)
    {
        this.pitch = p;
    }

    public float getPitch()
    {
        return this.pitch;
    }

    public void setYaw(float y)
    {
        this.yaw = y;
    }

    public float getYaw()
    {
        return this.yaw;
    }

    public void setGaz(float g)
    {
        this.gaz = g;
    }

    public float getGaz()
    {
        return this.gaz;
    }

}

class Drone
{
    private DroneConfig droneConfig = null;
    private DroneControl droneControl = null;

    public Drone()
    {
        droneConfig = new DroneConfig();
        droneControl = new DroneControl(droneConfig);
    }

    public bool connect()
    {
        if (droneControl.IsConnected)
        {
            return false;
        }

        droneControl.ConnectToDrone();
        return true;
    }

    public bool disconnect()
    {
        if (!droneControl.IsConnected)
        {
            return false;
        }

        droneControl.Disconnect();
        return true;
    }

    public bool reconnect()
    {
        if (disconnect())
            return false;

        if (connect())
            return false;

        return true;
    }

    public bool takeoff()
    {
        Command takeOffCommand = new FlightModeCommand(DroneFlightMode.TakeOff);

        if (!droneControl.IsCommandPossible(takeOffCommand))
            return false;

        droneControl.SendCommand(takeOffCommand);
        return true;
    }

    public bool land()
    {
        Command landCommand = new FlightModeCommand(DroneFlightMode.Land);

        if (!droneControl.IsCommandPossible(landCommand))
            return false;

        droneControl.SendCommand(landCommand);
        return true;
    }

    public bool enterHoverMode()
    {
        Command enterHoverModeCommand = new HoverModeCommand(DroneHoverMode.Hover);

        if (!droneControl.IsCommandPossible(enterHoverModeCommand))
            return false;

        droneControl.SendCommand(enterHoverModeCommand);
        return true;
    }

    public void move(Orientation oriantation)
    {
        FlightMoveCommand flightMoveCommand = new FlightMoveCommand(oriantation.getRoll(), oriantation.getPitch(), oriantation.getYaw(), oriantation.getGaz());

        if (droneControl.IsCommandPossible(flightMoveCommand))
            droneControl.SendCommand(flightMoveCommand);
        else
            Debug.Log("Move is not possible");
    }
}
