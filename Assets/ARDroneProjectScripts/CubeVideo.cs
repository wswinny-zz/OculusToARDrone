using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using Oculus2ARDrone;

public class CubeVideo : MonoBehaviour {

    private Texture2D cameraTexture;
    public Renderer MainRenderer;

    // Use this for initialization
    void Start () 
    {
        cameraTexture = new Texture2D(GlobalVariables.width, GlobalVariables.height);
        MainRenderer.material.mainTexture = cameraTexture;
    }
	
	// Update is called once per frame
	void Update () 
    {
        updateTexture();
    }

    private void updateTexture()
    {
        int r = 0;
        int g = 0;
        int b = 0;
        int total = 0;

        Color32[] colorArray = new Color32[GlobalVariables.videoData.Length / 3];

        for (var i = 0; i < GlobalVariables.videoData.Length; i += 3)
        {
            colorArray[i / 3] = new Color32(GlobalVariables.videoData[i + 2], GlobalVariables.videoData[i + 1], GlobalVariables.videoData[i + 0], 1);
            //colorArray[i / 3] = new Color32(1, 0, 0, 1);

            r += GlobalVariables.videoData[i + 2];
            g += GlobalVariables.videoData[i + 1];
            b += GlobalVariables.videoData[i + 0];

            total++;
        }

        r /= total;
        g /= total;
        b /= total;

        cameraTexture.SetPixels32(colorArray);
        cameraTexture.Apply();
        MainRenderer.material.mainTexture = cameraTexture;
    }
}
