using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    public Transform targetLocation; // Where player should teleport

    private void OnDrawGizmos()
    {
        if (targetLocation != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, targetLocation.position);
        }
    }
}
