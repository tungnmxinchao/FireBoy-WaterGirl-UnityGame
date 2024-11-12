using UnityEngine;
using System.Collections;

public class ButtonAndFanEffect : MonoBehaviour
{
	private Vector3 originalButtonPosition;

	public float pressDepthButton = 0.2f;
	public float pressSpeed = 2.0f;

	public GameObject[] fans; // Thay đổi thành mảng để chứa nhiều quạt

	private bool isPressed = false;
	private bool isFullyPressed = false;

	// Đối tượng âm thanh cho nút nhấn và âm thanh quạt
	public AudioSource buttonPressSound;
	public AudioSource fanRunningSound;

	void Start()
	{
		originalButtonPosition = transform.position;

		// Kiểm tra nếu mảng quạt được gán
		if (fans.Length == 0)
		{
			Debug.LogError("Fan objects not assigned!");
		}

		// Kiểm tra nếu các âm thanh được gán
		if (buttonPressSound == null)
		{
			Debug.LogError("Button press sound not assigned!");
		}
		if (fanRunningSound == null)
		{
			Debug.LogError("Fan running sound not assigned!");
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		// Cho phép cả nhân vật có tag "WG" và "FB" nhấn nút
		if (collision.CompareTag("WG") || collision.CompareTag("FB"))
		{
			isPressed = true;

			// Phát âm thanh khi nhấn nút
			if (!buttonPressSound.isPlaying)
			{
				buttonPressSound.Play();
			}
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		// Chỉ tắt fan khi nhân vật "WG" rời khỏi nút
		if (collision.CompareTag("WG") || collision.CompareTag("FB"))
		{
			isPressed = false;

			// Nếu không còn nhấn nút và fan đã được kích hoạt, bắt đầu đếm thời gian
			if (isFullyPressed)
			{
				StartCoroutine(DeactivateFansAfterDelay(4f)); // Tắt tất cả các quạt sau 10 giây
			}
		}
	}

	void Update()
	{
		if (isPressed)
		{
			Vector3 pressedButtonPosition = new Vector3(originalButtonPosition.x, originalButtonPosition.y - pressDepthButton, originalButtonPosition.z);
			transform.position = Vector3.Lerp(transform.position, pressedButtonPosition, Time.deltaTime * pressSpeed);

			if (Vector3.Distance(transform.position, pressedButtonPosition) < 0.01f)
			{
				isFullyPressed = true;
				ActivateFans(true); // Kích hoạt tất cả các quạt

				// Phát âm thanh quạt nếu chưa phát
				if (!fanRunningSound.isPlaying)
				{
					fanRunningSound.Play();
				}
			}
		}
		else
		{
			transform.position = Vector3.Lerp(transform.position, originalButtonPosition, Time.deltaTime * pressSpeed);
		}
	}

	private void ActivateFans(bool active)
	{
		foreach (GameObject fan in fans)
		{
			if (fan != null)
			{
				Fan fanScript = fan.GetComponent<Fan>();
				if (fanScript != null)
				{
					fanScript.ActivateFan(active);
				}
			}
		}
	}

	private IEnumerator DeactivateFansAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay); // Chờ 10 giây
		ActivateFans(false); // Tắt tất cả các quạt
		isFullyPressed = false; // Reset trạng thái

		// Dừng âm thanh quạt khi tắt
		if (fanRunningSound.isPlaying)
		{
			fanRunningSound.Stop();
		}
	}
}
