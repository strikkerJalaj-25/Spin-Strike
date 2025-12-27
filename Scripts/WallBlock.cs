using UnityEngine;

public class WallBlock : MonoBehaviour
{
    public GameObject fxPrefab;

    public void DestroyWall()
    {
        Debug.Log("WallBlock: DestroyWall called");
        Instantiate(fxPrefab, transform.position + Vector3.up * 0.5f, Quaternion.Euler(-90f, 0f, 0f));
        
    }
}
