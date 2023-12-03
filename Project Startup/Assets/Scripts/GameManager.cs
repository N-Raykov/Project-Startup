using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public string miniGameInProgress { get; set; }

    GameObject[] minigames;
    Dictionary<string, bool> miniGameStatus;

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void CompleteCurrentMinigame()
    {
        // Check if miniGameInProgress is not null and if the key exists in the dictionary
        if (!string.IsNullOrEmpty(miniGameInProgress) && miniGameStatus.ContainsKey(miniGameInProgress))
        {
            miniGameStatus[miniGameInProgress] = true;

            miniGameInProgress = null;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            // Fill the minigames array with gameobjects with tag "Interactable" and component "MiniGame"
            minigames = GameObject.FindGameObjectsWithTag("Interactable");
            minigames = minigames.Where(go => go.GetComponent<MiniGame>() != null).ToArray();

            // Iterate through minigames and set the public bool based on the dictionary
            foreach (GameObject minigame in minigames)
            {
                MiniGame miniGameScript = minigame.GetComponent<MiniGame>();
                if (miniGameScript != null)
                {
                    // Check if the public string is not in the dictionary and add it
                    if (!miniGameStatus.ContainsKey(miniGameScript.miniGameName))
                    {
                        miniGameStatus.Add(miniGameScript.miniGameName, false);
                    }

                    // Set the public bool based on the dictionary
                    miniGameScript.completed = miniGameStatus[miniGameScript.miniGameName];
                }
            }
        }
    }

    void Awake()
    {
        // Ensure there is only one instance of UIManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        miniGameStatus = new Dictionary<string, bool>();
    }
}
