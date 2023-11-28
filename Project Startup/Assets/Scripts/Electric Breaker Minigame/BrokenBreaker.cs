using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBreaker : MonoBehaviour
{
    private List<GameObject> screws = new List<GameObject>();

    [SerializeField] GameObject emptySpotPrefab;

    void Start()
    {
        // Find all child screws and add them to the list
        FindChildScrews();
    }

    void Update()
    {
        // Check if all screws are removed
        if (AreAllScrewsRemoved())
        {
            NotifyBreakerRemoved();
        }
    }

    void FindChildScrews()
    {
        // Find all child GameObjects with the Screw script
        Screw[] screwScripts = GetComponentsInChildren<Screw>();

        // Add each screw to the list
        foreach (Screw screwScript in screwScripts)
        {
            screws.Add(screwScript.gameObject);
        }
    }

    bool AreAllScrewsRemoved()
    {
        // Iterate through the screws and check if any of them still exist
        foreach (GameObject screw in screws)
        {
            if (screw != null)
            {
                // At least one screw is still present, so return false
                return false;
            }
        }

        // All screws are removed, so return true
        return true;
    }

    void NotifyBreakerRemoved()
    {
        // Notify the BreakerManager that the broken breaker is removed
        BreakerManager breakerManager = FindObjectOfType<BreakerManager>();
        if (breakerManager != null)
        {
            CreateEmptySpot();
            breakerManager.HandleBrokenBreakerRemoved(new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f));
        }

        // Optionally, you might want to perform other actions related to the broken breaker removal
        Destroy(gameObject);
    }

    void CreateEmptySpot()
    {
        // Instantiate the empty spot prefab at the position of the broken breaker
        Instantiate(emptySpotPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f), Quaternion.identity, this.transform.parent);
    }
}
