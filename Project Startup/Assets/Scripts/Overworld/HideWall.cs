using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWall : MonoBehaviour
{
    [SerializeField] GameObject[] walls;
    [SerializeField] float fadeSpeed;

    private Color visibleColor;
    private Color hiddenColor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach(GameObject wall in walls)
            {
                visibleColor = wall.GetComponent<MeshRenderer>().material.color;
                hiddenColor = new Color(visibleColor.r, visibleColor.g, visibleColor.b, 0);
                StartCoroutine(FadeOverTime(wall, visibleColor, hiddenColor, fadeSpeed));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StopAllCoroutines();
            foreach (GameObject wall in walls)
            {
                hiddenColor = wall.GetComponent<MeshRenderer>().material.color;
                visibleColor = new Color(hiddenColor.r, hiddenColor.g, hiddenColor.b, 1);
                StartCoroutine(FadeOverTime(wall, hiddenColor, visibleColor, fadeSpeed));
            }
        }
    }

    IEnumerator FadeOverTime(GameObject wall, Color start, Color end, float duration)
    {
        Color currentColorValue;
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            currentColorValue = Color.Lerp(start, end, normalizedTime);
        wall.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", currentColorValue);
            Debug.Log(wall.GetComponent<Renderer>().material.color);
            yield return null;
        }
        currentColorValue = end; //without this, the value will end at something like 0.9992367
        wall.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", currentColorValue);

    }
}
