using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.VR;

public class UITextUpdater : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 eulerAngles = InputTracking.GetLocalRotation(VRNode.Head).eulerAngles;
        GetComponent<Text>().text = "OCULUS:\n" + "X: " + eulerAngles.x + "\n" + "Y: " + eulerAngles.y + "\n" + "Z: " + eulerAngles.z;
    }
}
