using UnityEngine;

public class RotatingLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float rotateSpeed = 100f;
    public float lineLength = 5f;
    public bool canAim = true;
    private float angle = 0f;

    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        // Keeps increasing the angle every frame
        angle += rotateSpeed * Time.deltaTime;

        // Create a rotating direction vector
        Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));

        // End position of the line
        Vector3 endPoint = transform.position + direction * lineLength;

        // Assign positions to the LineRenderer
        lineRenderer.SetPosition(0, transform.position); // Start at player
        lineRenderer.SetPosition(1, endPoint); // Rotate around player
    }
    public Vector3 GetDirection()
    {
        return (lineRenderer.GetPosition(1) - transform.position).normalized;
    }

}
