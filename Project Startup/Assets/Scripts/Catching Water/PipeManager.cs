using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] GameObject nutPrefab;
    [SerializeField] GameObject waterDropletPrefab;
    [SerializeField] float minSpawnInterval = 2f;
    [SerializeField] float maxSpawnInterval = 5f;
    [SerializeField] float minSpawnIntervalReduction = 0.5f;
    [SerializeField] float maxSpawnIntervalReduction = 1f;

    private List<Vector3> originalNutPositions;
    private Transform[] nutPositions;
    private bool[] nutsTightened;
    private int nutsTightenedCount = 0;
    private int maxNuts;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();

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

        StartCoroutine(SpawnWaterDroplets());
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

    IEnumerator SpawnWaterDroplets()
    {
        while (nutsTightenedCount < maxNuts)
        {
            yield return new WaitForSeconds(GetRandomSpawnInterval());

            int randomIndex = GetRandomNutIndex();

            if (!nutsTightened[randomIndex] && randomIndex != -1)
            {
                Instantiate(waterDropletPrefab, originalNutPositions[randomIndex], Quaternion.identity);
            }
            else
            {
                yield return null;
            }
        }
    }

    float GetRandomSpawnInterval()
    {
        return Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    int GetRandomNutIndex()
    {
        // Ensure that there's at least one nut that is not tightened.
        if (nutsTightened.All(value => value))
        {
            // All nuts are tightened, return a default value or handle it as needed.
            return -1; // For example, return -1 to indicate an error or no available index.
        }

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

        minSpawnInterval -= minSpawnIntervalReduction;
        maxSpawnInterval -= maxSpawnIntervalReduction;

        if (nutsTightenedCount >= maxNuts)
        {
            EndMinigame(); 
        }
    }

    public void EndMinigame()
    {
        StopCoroutine(SpawnWaterDroplets());
        gameManager.CompleteCurrentMinigame();
    }
}
