using System.Collections;
using UnityEngine;

public class ButtonSinkEffect : MonoBehaviour
{
	private Vector3 originalButtonPosition;
	private Vector3 originalPaddlePosition;

	public GameObject paddleWhite;
	public float pressDepthButton = 0.2f;
	public float pressDepthPaddle = 0.5f;
	public float pressSpeed = 2.0f;

	private bool isPressed = false;
	private bool wasPressed = false;
	private bool isReturning = false;

	public AudioSource buttonPressSound;
	public AudioSource paddleMoveSound;

	private float returnThreshold = 0.9f;

	void Start()
	{
		originalButtonPosition = transform.position;

		if (paddleWhite != null)
		{
			originalPaddlePosition = paddleWhite.transform.position;
		}
		else
		{
			Debug.LogError("PaddleWhite object not assigned!");
		}

		if (buttonPressSound == null)
		{
			Debug.LogError("Button press sound not assigned!");
		}

		if (paddleMoveSound == null)
		{
			Debug.LogError("Paddle move sound not assigned!");
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("WG") || collision.CompareTag("FB") || collision.CompareTag("Rock"))
		{
			isPressed = true;

			if (!wasPressed && buttonPressSound != null)
			{
				buttonPressSound.Play();
			}

			if (paddleMoveSound != null && !paddleMoveSound.isPlaying)
			{
				paddleMoveSound.Play();
			}

			isReturning = false;
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("WG") || collision.CompareTag("FB") || collision.CompareTag("Rock"))
		{
			isPressed = false;
			isReturning = true;
		}
	}

	void Update()
	{
		if (isPressed)
		{
			Vector3 pressedButtonPosition = new Vector3(originalButtonPosition.x, originalButtonPosition.y - pressDepthButton, originalButtonPosition.z);
			transform.position = Vector3.Lerp(transform.position, pressedButtonPosition, Time.deltaTime * pressSpeed);

			if (paddleWhite != null)
			{
				Vector3 pressedPaddlePosition = new Vector3(originalPaddlePosition.x, originalPaddlePosition.y - pressDepthPaddle, originalPaddlePosition.z);
				paddleWhite.transform.position = Vector3.Lerp(paddleWhite.transform.position, pressedPaddlePosition, Time.deltaTime * pressSpeed);
			}
		}
		else
		{
			transform.position = Vector3.Lerp(transform.position, originalButtonPosition, Time.deltaTime * pressSpeed);

			if (paddleWhite != null)
			{
				paddleWhite.transform.position = Vector3.Lerp(paddleWhite.transform.position, originalPaddlePosition, Time.deltaTime * pressSpeed);

				if (isReturning && Vector3.Distance(paddleWhite.transform.position, originalPaddlePosition) < returnThreshold)
				{
					if (!paddleMoveSound.isPlaying)
					{
						paddleMoveSound.Play();
					}
					isReturning = false;
				}
			}
		}

		wasPressed = isPressed;
	}
}
