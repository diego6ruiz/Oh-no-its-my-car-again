using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Make sure to include this for working with UI elements

public class JaviScript : MonoBehaviour
{
    public GameObject uiPanel; // Assign the UI Panel in the inspector
    public GameObject hintPanel; // Assign the Text element from the canvas that you want to show temporarily
    private bool isFirstTime = true; // Flag to check if it's the first time entering the trigger

    private void Start()
    {
        if (hintPanel != null)
        {
            hintPanel.gameObject.SetActive(false); // Ensure the text is hidden at start
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isFirstTime)
        {
            uiPanel.SetActive(true);
            isFirstTime = false; 

            if (hintPanel != null)
            {
                StartCoroutine(ShowHintText()); 
            }

        } else if (other.CompareTag("Player") && !isFirstTime) 
        {
            uiPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiPanel.SetActive(false);
        }
    }

    IEnumerator ShowHintText()
    {
        hintPanel.SetActive(true); 
        yield return new WaitForSeconds(5); 
        hintPanel.SetActive(false); 
    }
}
