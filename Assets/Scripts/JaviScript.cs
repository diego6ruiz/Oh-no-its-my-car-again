using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaviScript : MonoBehaviour
{
    public GameObject uiPanel; // Assign the UI Panel in the inspector

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Activate the UI Panel
            uiPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Deactivate the UI Panel when player leaves the trigger
            uiPanel.SetActive(false);
        }
    }
}