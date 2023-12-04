using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public GameObject nutPrefab;
    public GameObject waterDropletPrefab;
    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 5f;

    private List<Vector3> originalNutPositions;
    private Transform[] nutPositions;
    private bool[] nutsTightened;
    private int nutsTightenedCount = 0;
    private int maxNuts;

    void Start()
    {
        Transform[] childTransforms = GetComponentsInChildren<Transform>();
        nutPositions = new Transform[childTransforms.Length - 1];
        nutsTightened = new bool[childTransforms.Length - 1];

        int nutCount = 0;
        for (int i = 1; i < childTransforms.Length; i++)
        {
            if (childTransforms[i].CompareTag("Nut"))
            {
                nutPositions[nutCount] = childTransforms[i];
                nutCount++;
            }
        }

        maxNuts = nutCount;

        if (nutPrefab != null)
        {
            CreateNuts();
        }

        StartSpawning();
    }

    void CreateNuts()
    {
        originalNutPositions = new List<Vector3>();

        for (int i = 0; i < nutPositions.Length; i++)
        {
            originalNutPositions.Add(nutPositions[i].position);

            GameObject nut = Instantiate(nutPrefab, nutPositions[i].position, Quaternion.identity, transform);
            Nut nutScript = nut.GetComponent<Nut>();

            if (nutScript != null)
            {
                nutScript.nutIndex = i;
            }

            Destroy(nutPositions[i].gameObject);
        }
    }


    void StartSpawning()
    {
        InvokeRepeating("SpawnWaterDroplet", maxSpawnInterval, GetRandomSpawnInterval());
    }

    float GetRandomSpawnInterval()
    {
        return Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void SpawnWaterDroplet()
    {
        if (nutsTightenedCount < maxNuts)
        {
            int randomIndex = GetRandomNutIndex();

            if (!nutsTightened[randomIndex])
            {
                Instantiate(waterDropletPrefab, originalNutPositions[randomIndex], Quaternion.identity);

                minSpawnInterval -= 0.1f;
                maxSpawnInterval -= 0.2f;
            }
        }
    }

    int GetRandomNutIndex()
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, originalNutPositions.Count);
        } while (nutsTightened[randomIndex]);

        return randomIndex;
    }

    public void NutTightened(int nutIndex)
    {
        nutsTightened[nutIndex] = true;
        nutsTightenedCount++;

        if (nutsTightenedCount >= maxNuts)
        {
            EndMinigame(true); 
        }
    }

    public void EndMinigame(bool playerWon)
    {
        CancelInvoke("SpawnWaterDroplet");

        if (playerWon)
        {
            Debug.Log("Minigame Completed! Player Won!");
        }
        else
        {
            Debug.Log("Minigame Failed! Player Lost!");
        }
    }
}
