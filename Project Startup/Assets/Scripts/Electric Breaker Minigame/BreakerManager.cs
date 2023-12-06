using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerManager : MonoBehaviour
{
    [SerializeField] GameObject normalBreakerPrefab;
    [SerializeField] GameObject brokenBreakerPrefab;
    [SerializeField] GameObject movingBreakerPrefab;
    GameManager gameManager;

    [SerializeField] int minMoneyOnWin = 520;
    [SerializeField] int maxMoneyOnWin = 650;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        // Instantiate normal breakers, choosing one as the broken breaker
        InstantiateNormalAndBrokenBreakers();
    }

    void InstantiateNormalAndBrokenBreakers()
    {
        GameObject[] breakerObjects = GameObject.FindGameObjectsWithTag("Breaker");

        // Choose one breaker to be broken
        GameObject brokenBreakerObject = breakerObjects[Random.Range(0, breakerObjects.Length)];

        foreach (GameObject breakerObject in breakerObjects)
        {
            // Skip the broken breaker
            if (breakerObject == brokenBreakerObject)
                continue;

            // Instantiate a new normal breaker at the position and rotation of the existing breakerObject
            GameObject normalBreaker = Instantiate(normalBreakerPrefab, breakerObject.transform.position, breakerObject.transform.rotation, breakerObject.transform.parent);

            // Destroy the existing breakerObject
            Destroy(breakerObject);
        }

        // Instantiate the broken breaker at the position of the chosen broken breaker
        GameObject brokenBreaker = Instantiate(brokenBreakerPrefab, brokenBreakerObject.transform.position, brokenBreakerObject.transform.rotation, brokenBreakerObject.transform.parent);

        // Destroy the chosen broken breaker
        Destroy(brokenBreakerObject);
    }

    public void HandleBrokenBreakerRemoved(Vector3 position)
    {
        Instantiate(movingBreakerPrefab, position, Quaternion.identity);
    }

    public void HandleMovingBreakerPlaced(Vector3 position)
    {
        Instantiate(normalBreakerPrefab, position, Quaternion.identity);
        gameManager.money += Random.Range(minMoneyOnWin, maxMoneyOnWin);
        gameManager.CompleteCurrentMinigame();
    }
}




