using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float rotationSpeed = 100f;
    public float rayLength = 1.5f;
    public float lineHeight = 0.1f;

    public bool canAim = true;
    private float angle = 0f;

    void Start()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = true;
    }

    void Update()
    {
        if (!canAim) return;

        angle += rotationSpeed * Time.deltaTime;
        float rad = angle * Mathf.Deg2Rad;

        Vector3 start = transform.position + new Vector3(0, lineHeight, 0);
        Vector3 direction = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad));

        Vector3 end = start + direction * rayLength;

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

    public Vector3 GetDirection()
    {
        Vector3 start = transform.position + new Vector3(0, lineHeight, 0);
        Vector3 end = lineRenderer.GetPosition(1);
        return (end - start).normalized;
    }
}
