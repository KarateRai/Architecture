using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SkyBoxBG : MonoBehaviour
{
    public PolygonCollider2D cinemachineConfiner;
    
    private Transform cameraTarget;
    private SpriteRenderer background;
    private Vector3 bgPos;
    private Vector2[] boundingBox; // structure: (left, right, top, bottom)
    private Vector2 ratioBG; // ratio of distances of free space within boundingbox between bg and cam.
    private Vector2 camSize;

    private void Awake()
    {
        background = GetComponent<SpriteRenderer>();
        cameraTarget = Camera.main.transform;
    }
    private void Start()
    {
        boundingBox = GetBoundingBox();
        ratioBG = GetRatio();
        bgPos = Vector3.zero;
        camSize = new Vector2(Camera.main.orthographicSize * Camera.main.aspect * 2, Camera.main.orthographicSize * 2);
    }

    private Vector2 GetRatio()
    {
        float bBoxWidth = Vector2.Distance(boundingBox[0], boundingBox[1]);
        float bBoxHeight = Vector2.Distance(boundingBox[2], boundingBox[3]);

        float camDistanceX = bBoxWidth - camSize.x;
        float camDistanceY = bBoxHeight - camSize.y;
        float bgDistanceX = bBoxWidth - background.size.x / 2;
        float bgDistanceY = bBoxHeight - background.size.y / 2;

        float widthRatio = bgDistanceX / camDistanceX;
        float heightRatio = bgDistanceY / camDistanceY;

        return new Vector2(widthRatio, heightRatio);
    }

    private Vector2[] GetBoundingBox()
    {
        Vector2[] box = new Vector2[4];
        Vector2 leftEdge = Vector2.zero;
        Vector2 rightEdge = Vector2.zero;
        Vector2 topEdge = Vector2.zero;
        Vector2 bottomEdge = Vector2.zero;

        foreach (Vector2 point in cinemachineConfiner.points)
        {
            if (point.x < leftEdge.x)
                leftEdge.x = point.x;
            if (point.x > rightEdge.x)
                rightEdge.x = point.x;
            if (point.y > topEdge.y)
                topEdge.y = point.y;
            if (point.y < bottomEdge.y)
                bottomEdge.y = point.y;
        }
        //force boundingbox to be centered at world center:
        if (Mathf.Abs(leftEdge.x) > Mathf.Abs(rightEdge.x))
            rightEdge.x = leftEdge.x + Vector2.Distance(leftEdge, Vector3.zero) * 2;
        else
            leftEdge.x = rightEdge.x - Vector2.Distance(Vector3.zero, rightEdge) * 2;
        if (Mathf.Abs(bottomEdge.y) > Mathf.Abs(topEdge.y))
            topEdge.y = bottomEdge.y + Vector2.Distance(bottomEdge, Vector3.zero) * 2;
        else
            bottomEdge.y = topEdge.y - Vector2.Distance(Vector3.zero, topEdge) * 2;
        return new Vector2[] { leftEdge, rightEdge, topEdge, bottomEdge };
    }
    private void Update()
    {
        bgPos = GetNewPosition();
        transform.position = bgPos;
    }

    private Vector3 GetNewPosition()
    {

        float posX = cameraTarget.position.x * ratioBG.x;
        float posY = cameraTarget.position.y * ratioBG.y;
        return new Vector3(posX, posY, 0);
    }
}
