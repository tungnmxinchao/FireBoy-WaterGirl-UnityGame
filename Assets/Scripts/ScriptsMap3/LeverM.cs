using UnityEngine;

public class LeverM : MonoBehaviour
{
    public bool MoveLiftU;
    public bool MoveLiftD;
    public GameObject Lever_L;  // Reference to left lever
    public GameObject Lever_R;  // Reference to right lever
    private bool liftAtTargetPosition = false; // Tracks if the lift has reached the target position

    // Audio fields
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip leverSound;

    private void Start()
    {
        if (Lever_L != null) Lever_L.SetActive(false); // Start with Lever_L hidden
        if (Lever_R != null) Lever_R.SetActive(true);  // Start with Lever_R visible

        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FB") || collision.CompareTag("WG"))
        {
            PlayLeverSound(); // Play the lever sound when FB or WG collides

            if (Lever_R.activeSelf && !liftAtTargetPosition)
            {
                MoveLiftU = true;  // Set to move up
                MoveLiftD = false; // Ensure move down is cleared
                Lever_R.SetActive(false);
                Lever_L.SetActive(true);
            }
            else if (Lever_L.activeSelf && liftAtTargetPosition)
            {
                MoveLiftD = true;  // Set to move down
                MoveLiftU = false; // Ensure move up is cleared
                Lever_L.SetActive(false);
                Lever_R.SetActive(true);
            }
        }
    }

    private void PlayLeverSound()
    {
        if (audioSource != null && leverSound != null)
        {
            audioSource.PlayOneShot(leverSound);
        }
    }

    public void SetLiftAtTargetPosition(bool atTarget)
    {
        liftAtTargetPosition = atTarget;
    }
}
