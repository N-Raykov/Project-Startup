using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //[SerializeField] GameObject[] uiPrefabArray;
    [SerializeField] GameObject pauseMenuPrefab;

    //private GameObject[] uiInstances;
    private GameObject pauseMenu;
    private bool isPaused = false;

    void Awake()
    {
        //uiInstances = new GameObject[uiPrefabArray.Length];
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        ActivateSceneUI(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ActivateSceneUI(scene.buildIndex);
    }

    void ActivateSceneUI(int sceneID)
    {
        /*
        if (uiInstances != null && sceneID >= 0 && sceneID < uiInstances.Length)
        {
            if (uiPrefabArray[sceneID] != null)
            {
                uiInstances[sceneID] = Instantiate(uiPrefabArray[sceneID], transform.parent);
            }
            else
            {
                Debug.LogWarning("UIManager: Missing prefab for scene ID");
            }

        }
        else
        {
            Debug.LogWarning("UIManager: Scene ID out of range");
        }
        */
        if (pauseMenuPrefab != null && pauseMenu == null)
        {
            pauseMenu = Instantiate(pauseMenuPrefab, transform.parent);
            pauseMenu.SetActive(false);
            isPaused = false;
        }

        isPaused = false;
        pauseMenu.SetActive(false);
    }

    public void TogglePauseMenu()
    {
        if (pauseMenu != null)
        {
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);

            Time.timeScale = isPaused ? 0f : 1f; // Freeze time when paused
        }
    }

 
}
