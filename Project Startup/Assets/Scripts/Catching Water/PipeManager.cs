using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] ParticleSystem completionParticles;
    [SerializeField] TextMeshProUGUI completed;

    [SerializeField] GameObject nutPrefab;
    [SerializeField] GameObject waterDropletPrefab;
    [SerializeField] float minSpawnInterval = 2f;
    [SerializeField] float maxSpawnInterval = 5f;
    [SerializeField] float minSpawnIntervalReduction = 0.5f;
    [SerializeField] float maxSpawnIntervalReduction = 1f;

    [SerializeField] int minMoneyOnWin = 520;
    [SerializeField] int maxMoneyOnWin = 650;

    private List<Vector3> originalNutPositions;
    private Transform[] nutPositions;
    private bool[] nutsTightened;
    private int nutsTightenedCount = 0;
    private int maxNuts;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        GameObject[] nuts = GameObject.FindGameObjectsWithTag("Nut");
        List<Transform> nutTransforms = new List<Transform>();

        foreach (GameObject nut in nuts)
        {
            nutTransforms.Add(nut.transform);
        }

        nutPositions = new Transform[nutTransforms.Count];
        nutsTightened = new bool[nutTransforms.Count];

        int nutCount = 0;
        for (int i = 0; i < nutTransforms.Count; i++)
        {
            if (nutTransforms[i].CompareTag("Nut"))
            {
                nutPositions[nutCount] = nutTransforms[i];
                Debug.Log(nutPositions[nutCount].name);
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

            if (randomIndex != -1)
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
            return -1; 
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
        completionParticles.Play();
        completed.enabled = true;
        gameManager.money += Random.Range(minMoneyOnWin, maxMoneyOnWin);
        gameManager.CompleteCurrentMinigame();
    }
}
