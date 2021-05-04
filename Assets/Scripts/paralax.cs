using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralax : MonoBehaviour
{
    public float lenght, startpos;
    public Camera cam;
    public float parallaxEffect;
    public int multiplied;
    public PolygonCollider2D cinemachineConfiner;
    Vector4 boundingBox;
    public bool isSkybox;

    void Start()
    {
        startpos = transform.position.x;
        lenght = ((GetComponent<SpriteRenderer>().bounds.size.x / 2)/* - ExtensionsMeasures.NominalScreenWidthAt(cam, cam.transform) / 2)*/);
        boundingBox = GetBoundingBox();
    }

    private Vector4 GetBoundingBox()
    {
        float tempLeft = 0, tempRight = 0, tempUp = 0, tempDown = 0;
        if(isSkybox == true)
        {
            foreach(Vector2 points in cinemachineConfiner.points)
            {
                if(points.x < tempLeft)
                {
                    tempLeft = points.x;
                }
                if (points.x > tempRight)
                {
                    tempRight = points.x;
                }
                if (points.y < tempDown)
                {
                    tempDown = points.y;
                }
                if (points.y > tempUp)
                {
                    tempUp = points.y;
                }
            }
        }
        return new Vector4(tempLeft, tempRight, tempUp, tempDown);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

        if (temp > startpos + lenght) startpos += lenght * multiplied;
        else if (temp < startpos - lenght) startpos -= lenght * multiplied;
    }
}

public static class ExtensionsMeasures
{
    public static float NominalScreenWidthAt(this Camera c, Transform t)
    {
        float yFromCamera = t.transform.position.z - c.transform.position.z;

        return
            c.ViewportToWorldPoint(new Vector3(1f, 1f, yFromCamera)).x
            - c.ViewportToWorldPoint(new Vector3(0f, 1f, yFromCamera)).x;
    }
}
