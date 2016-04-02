using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.VR;
using AR.Drone.Client;
using AR.Drone.Video;
using AR.Drone.Data;
using AR.Drone.Data.Navigation;
using NativeWifi;
using Oculus2ARDrone;

public class UITextUpdater : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 eulerAngles = InputTracking.GetLocalRotation(VRNode.Head).eulerAngles;
        GetComponent<Text>().text = "OCULUS:\n" + 
            "X: " + eulerAngles.x + "\n" + 
            "Y: " + eulerAngles.y + "\n" + 
            "Z: " + eulerAngles.z + "\n\n" + 
			"DRONE:\n" + 
            "Battery: " + GlobalVariables.navigationData.Battery.Percentage + "%\n" + 
            "Altitude: " + GlobalVariables.navigationData.Altitude + "m\n" + 
            determineWifiStrength() + "\n";
    }

    private string determineWifiStrength()
    {
        int signalQuality = 0;
        foreach (WlanClient.WlanInterface wlanInterface in GlobalVariables.wlanClient.Interfaces)
        {
            try
            {
                signalQuality = (int)wlanInterface.CurrentConnection.wlanAssociationAttributes.wlanSignalQuality;
            }
            catch (System.Exception e)
            {
                Debug.Log("No Wifi Connection");
            }
        }

        if(signalQuality != 0)
        {
            return "Wifi: " + signalQuality.ToString() + "%";
        }

        return "Wifi: 0%";
    }
}
