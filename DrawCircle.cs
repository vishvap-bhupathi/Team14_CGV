using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawCircle : MonoBehaviour
{
    public LineRenderer lineRenderer;  // Line Renderer component
    public int segments = 360;         // Number of line segments for smoothness
    public float radius = 5f;          // Radius of the circle

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        DrawCircleRenderer();
    }

    void DrawCircleRenderer()
    {
        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, 0, z));
            angle += (360f / segments);
        }
    }
}
