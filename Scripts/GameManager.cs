using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public AudioSource bgSound;

    void Start()
    {
        // Always unpause scene on load
        Time.timeScale = 1f;

        if (bgSound != null && !bgSound.isPlaying)
            bgSound.Play();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(ResetCameraAfterLoad());
    }

    IEnumerator ResetCameraAfterLoad()
    {
        yield return null; // wait 1 frame

        if (CameraSwitch.instance != null)
        {
            CameraSwitch.instance.FindCamerasInScene();
            CameraSwitch.instance.ResetToMainCamera();
        }
    }

    public void Retry()
    {
        Debug.Log("Retry Game");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        Debug.Log("Next Level triggered");

        Time.timeScale = 1f;

        int current = SceneManager.GetActiveScene().buildIndex;
        int next = current + 1;

        Debug.Log("Current: " + current + " Next: " + next);

        if (next < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(next);
        }
        else
        {
            Debug.Log("No more levels. Back to menu.");
            Menu();
        }
    }

    public void Menu()
    {
        Debug.Log("Loading Menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
