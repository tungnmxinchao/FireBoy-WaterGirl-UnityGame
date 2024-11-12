using UnityEngine;

public class Dead : MonoBehaviour
{
    public GameObject wg; // Reference to the "WG" GameObject
    public GameObject dead1; // Reference to the "Dead1" GameObject

    private AudioSource deadAudio;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        deadAudio = GetComponent<AudioSource>();
    }

    public void ActivateDead(Vector3 position)
    {
        // Deactivate the "WG" GameObject
        wg.SetActive(false);

        // Set the position of the "Dead1" GameObject to the same position as "WG"
        dead1.transform.position = position;

        // Activate the "Dead1" GameObject
        dead1.SetActive(true);

        // Play the death sound
        PlayDeathSound();
    }

    public void PlayDeathSound()
    {
        // Check if the AudioSource component is available
        if (deadAudio != null && deadAudio.clip != null)
        {
            // Play the death sound
            deadAudio.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource component or audio clip is missing on the 'Dead' GameObject.");
        }
    }
}