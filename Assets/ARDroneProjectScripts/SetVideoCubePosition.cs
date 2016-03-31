using UnityEngine;
using System.Collections;
using System;

public class SetVideoCubePosition : MonoBehaviour
{

    public float FRUSTDIST = 1000;
    public Camera mainCamera;

    public float aspectRatio = 1;
    public float fov = 110;

    // Use this for initialization
    void Start()
    {
        float frustWidth = FRUSTDIST * (float)Math.Tan(ConvertToRadians(fov / 2)) * 2;
        float frustHeight = frustWidth * (1 / aspectRatio);

        gameObject.transform.localScale = new Vector3(-frustWidth, frustHeight, 1);
        gameObject.transform.position = new Vector3(0, 0, FRUSTDIST + 100);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float ConvertToRadians(float angle)
    {
        return ((float)Math.PI / 180) * angle;
    }
}
