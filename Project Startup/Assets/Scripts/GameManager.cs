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

    GameObject[] furniture;
    Dictionary<string, bool> furnitureStatus;

    public int money = 0;

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

    public void PlaceFurniture(string furnitureName)
    {
        if (furnitureStatus.ContainsKey(furnitureName))
        {
            furnitureStatus[furnitureName] = true;
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

            furniture = GameObject.FindGameObjectsWithTag("Interactable");
            furniture = furniture.Where(go => go.GetComponentInChildren<FurniturePlacement>() != null).ToArray();

            // Iterate through minigames and set the public bool based on the dictionary
            foreach (GameObject minigame in minigames)
            {
                MiniGame miniGameScript = minigame.GetComponent<MiniGame>();
                if (miniGameScript != null)
                {
                    // Check if the public string is not in the dictionary and add it
                    if (miniGameStatus.ContainsKey(miniGameScript.miniGameName) == false)
                    {
                        miniGameStatus.Add(miniGameScript.miniGameName, false);
                    }

                    // Set the public bool based on the dictionary
                    miniGameScript.completed = miniGameStatus[miniGameScript.miniGameName];
                }
            }

            foreach (GameObject furniture in furniture)
            {
                FurniturePlacement furniturePlacementScript = furniture.GetComponentInChildren<FurniturePlacement>();
                if(furniturePlacementScript != null)
                {
                    if (furnitureStatus.ContainsKey(furniturePlacementScript.furniture.name) == false)
                    {
                        furnitureStatus.Add(furniturePlacementScript.furniture.name, false);
                    }

                    if (furnitureStatus[furniturePlacementScript.furniture.name] == true)
                    {
                        furniturePlacementScript.Place();
                    }
                }
            }
        }
    }

    void Awake()
    {
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
        furnitureStatus = new Dictionary<string, bool>();
    }
}
