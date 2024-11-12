using UnityEngine;
using System.Collections;

public class HotWater : MonoBehaviour
{
    private GM gameManager;

    void Start()
    {
        // Find the Game Manager in the scene
        gameManager = FindObjectOfType<GM>();
        if (gameManager == null)
        {
            Debug.LogError("Game Manager not found in the scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has the "WG" tag
        if (collision.CompareTag("WG"))
        {
            // Get the Dead script component attached to the colliding object
            Dead deadScript = collision.GetComponent<Dead>();
            if (deadScript != null)
            {
                // Call the ActivateDead() method to handle the transition
                deadScript.ActivateDead(collision.transform.position);
            }

            // Start the delay coroutine to show the lose panel after 2 seconds
            if (gameManager != null)
            {
                StartCoroutine(DelayedLosePanel());
            }
            else
            {
                Debug.LogError("Game Manager reference is missing!");
            }
        }
    }

    

    private IEnumerator DelayedLosePanel()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        gameManager.ShowLosePanel(); // Show LoseUI after delay
    }
}