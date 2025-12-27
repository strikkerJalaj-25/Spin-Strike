using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSwitch : MonoBehaviour
{
    public static CameraSwitch instance;

    public Camera mainCamera;
    public Camera fixedCamera;

    private bool usingMainCamera = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindCamerasInScene();
        ResetToMainCamera();
    }

    public void SwitchCamera()
    {
        if (mainCamera == null || fixedCamera == null)
        {
            FindCamerasInScene();
            if (mainCamera == null || fixedCamera == null)
            {
                Debug.LogWarning("CameraSwitch: One or both cameras are missing in this scene!");
                return;
            }
        }

        usingMainCamera = !usingMainCamera;
        mainCamera.enabled = usingMainCamera;
        fixedCamera.enabled = !usingMainCamera;
    }

    public void ResetToMainCamera()
    {
        usingMainCamera = true;

        if (mainCamera != null)
            mainCamera.enabled = true;

        if (fixedCamera != null)
            fixedCamera.enabled = false;
    }

    public void FindCamerasInScene()
    {
        GameObject mainObj = GameObject.FindWithTag("MainCamera");
        GameObject fixedObj = GameObject.FindWithTag("FixedCamera");

        if (mainObj != null)
            mainCamera = mainObj.GetComponent<Camera>();

        if (fixedObj != null)
            fixedCamera = fixedObj.GetComponent<Camera>();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
