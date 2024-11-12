using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_1 : MonoBehaviour
{
    public bool MoveELe1;
    private int objectsOnButton = 0; // Tracks the number of objects on the button

    // Audio fields
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonSound;

    private void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object has tag "FB" or "WG"
        if (collision.CompareTag("FB") || collision.CompareTag("WG"))
        {
            // Increment the counter when an object enters
            objectsOnButton++;

            // Activate MoveELe1 only if this is the first object stepping on the button
            if (objectsOnButton == 1)
            {
                MoveELe1 = true;
                PlayButtonSound(); // Play the button sound
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the object has tag "FB" or "WG"
        if (collision.CompareTag("FB") || collision.CompareTag("WG"))
        {
            // Decrement the counter when an object exits
            objectsOnButton--;

            // Deactivate MoveELe1 only if no objects are left on the button
            if (objectsOnButton == 0)
            {
                MoveELe1 = false;
            }
        }
    }

    private void PlayButtonSound()
    {
        if (audioSource != null && buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound);
        }
    }
}
