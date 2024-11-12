using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOnDoublePaddle : MonoBehaviour
{
	private Vector3 originalButtonPosition;
	private Vector3 originalPaddleBlueOnePosition;
	private Vector3 originalPaddleBlueSecondPosition;

	public GameObject paddleBlueOne;
	public GameObject paddleBlueSecond;

	public float pressDepthButton = 0.2f;
	public float liftDepthPaddleBlueOne = 0.3f;
	public float pressDepthPaddleBlueSecond = 0.3f;
	public float pressSpeed = 2.0f;

	private bool isPressed = false;
	private bool isReturning = false; // Flag to check if the paddles are returning
	private bool hasPlayedSound = false; // Flag to check if sound has played during pressing
	private bool hasPlayedReturnSound = false; // Flag for sound playback during return

	public AudioSource buttonPressSound;
	public AudioSource paddleMoveSound;

	// Threshold to check how close paddles are to their original positions
	private float returnThreshold = 0.9f;

	void Start()
	{
		originalButtonPosition = transform.position;

		if (paddleBlueOne != null)
		{
			originalPaddleBlueOnePosition = paddleBlueOne.transform.position;
		}
		else
		{
			Debug.LogError("PaddleBlueOne object not assigned!");
		}

		if (paddleBlueSecond != null)
		{
			originalPaddleBlueSecondPosition = paddleBlueSecond.transform.position;
		}
		else
		{
			Debug.LogError("PaddleBlueSecond object not assigned!");
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("WG") || collision.CompareTag("FB"))
		{
			isPressed = true;

			if (buttonPressSound != null)
			{
				buttonPressSound.Play();
			}
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("WG") || collision.CompareTag("FB"))
		{
			isPressed = false;
			isReturning = true; // Start returning when the button is released
			hasPlayedSound = false; // Reset sound played flag for pressing
			hasPlayedReturnSound = false; // Reset sound played flag for returning
		}
	}

	void Update()
	{
		if (isPressed)
		{
			Vector3 pressedButtonPosition = new Vector3(originalButtonPosition.x, originalButtonPosition.y - pressDepthButton, originalButtonPosition.z);
			transform.position = Vector3.Lerp(transform.position, pressedButtonPosition, Time.deltaTime * pressSpeed);

			if (paddleBlueOne != null)
			{
				Vector3 liftedPaddleBlueOnePosition = new Vector3(originalPaddleBlueOnePosition.x, originalPaddleBlueOnePosition.y + liftDepthPaddleBlueOne, originalPaddleBlueOnePosition.z);
				paddleBlueOne.transform.position = Vector3.Lerp(paddleBlueOne.transform.position, liftedPaddleBlueOnePosition, Time.deltaTime * pressSpeed);

				// Play sound only if it hasn't played yet during this press
				if (paddleMoveSound != null && !hasPlayedSound)
				{
					paddleMoveSound.Play();
					hasPlayedSound = true; // Set the flag to true after playing the sound
				}
			}

			if (paddleBlueSecond != null)
			{
				Vector3 pressedPaddleBlueSecondPosition = new Vector3(originalPaddleBlueSecondPosition.x, originalPaddleBlueSecondPosition.y - pressDepthPaddleBlueSecond, originalPaddleBlueSecondPosition.z);
				paddleBlueSecond.transform.position = Vector3.Lerp(paddleBlueSecond.transform.position, pressedPaddleBlueSecondPosition, Time.deltaTime * pressSpeed);

				// Play sound only if it hasn't played yet during this press
				if (paddleMoveSound != null && !hasPlayedSound)
				{
					paddleMoveSound.Play();
					hasPlayedSound = true; // Set the flag to true after playing the sound
				}
			}
		}
		else
		{
			transform.position = Vector3.Lerp(transform.position, originalButtonPosition, Time.deltaTime * pressSpeed);

			if (paddleBlueOne != null)
			{
				paddleBlueOne.transform.position = Vector3.Lerp(paddleBlueOne.transform.position, originalPaddleBlueOnePosition, Time.deltaTime * pressSpeed);

				// Check if returning and play sound when near original position
				if (isReturning && Vector3.Distance(paddleBlueOne.transform.position, originalPaddleBlueOnePosition) < returnThreshold)
				{
					if (paddleMoveSound != null && !paddleMoveSound.isPlaying && !hasPlayedReturnSound)
					{
						paddleMoveSound.Play();
						hasPlayedReturnSound = true; // Set the flag to true after playing the sound
					}
				}
			}

			if (paddleBlueSecond != null)
			{
				paddleBlueSecond.transform.position = Vector3.Lerp(paddleBlueSecond.transform.position, originalPaddleBlueSecondPosition, Time.deltaTime * pressSpeed);

				// Check if returning and play sound when near original position
				if (isReturning && Vector3.Distance(paddleBlueSecond.transform.position, originalPaddleBlueSecondPosition) < returnThreshold)
				{
					if (paddleMoveSound != null && !paddleMoveSound.isPlaying && !hasPlayedReturnSound)
					{
						paddleMoveSound.Play();
						hasPlayedReturnSound = true; // Set the flag to true after playing the sound
					}
				}
			}
		}
	}
}
