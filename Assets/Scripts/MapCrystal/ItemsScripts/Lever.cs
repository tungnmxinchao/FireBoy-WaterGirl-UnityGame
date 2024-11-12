using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
	public float rotationSpeed = 2f;  // Tốc độ xoay của cần gạt
	public float targetAngle = 45f;   // Góc mục tiêu khi cần gạt được đẩy
	public string playerTag = "WG"; // Tag của người chơi để phát hiện va chạm

	private bool isPushed = false;    // Kiểm tra nếu cần gạt đã bị đẩy
	private bool isReturning = false; // Kiểm tra cần gạt có đang quay lại vị trí ban đầu

	private float originalAngle;      // Lưu góc ban đầu của cần gạt

	void Start()
	{
		// Lưu lại góc ban đầu của cần gạt
		originalAngle = transform.rotation.eulerAngles.z;
	}

	void Update()
	{
		if (isPushed)
		{
			// Xoay cần gạt từ từ đến góc mục tiêu
			float angle = Mathf.LerpAngle(transform.rotation.eulerAngles.z, targetAngle, Time.deltaTime * rotationSpeed);
			transform.rotation = Quaternion.Euler(0, 0, angle);

			// Kiểm tra nếu cần gạt đã đến vị trí mục tiêu
			if (Mathf.Abs(transform.rotation.eulerAngles.z - targetAngle) < 0.1f)
			{
				isPushed = false; // Ngừng xoay khi đến góc mục tiêu
			}
		}
		else if (isReturning)
		{
			// Xoay cần gạt từ từ về vị trí ban đầu
			float angle = Mathf.LerpAngle(transform.rotation.eulerAngles.z, originalAngle, Time.deltaTime * rotationSpeed);
			transform.rotation = Quaternion.Euler(0, 0, angle);

			// Kiểm tra nếu cần gạt đã quay về vị trí ban đầu
			if (Mathf.Abs(transform.rotation.eulerAngles.z - originalAngle) < 0.1f)
			{
				isReturning = false; // Ngừng xoay khi đã quay về vị trí ban đầu
			}
		}
	}

	// Xử lý va chạm với người chơi
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(playerTag) && !isPushed && !isReturning)
		{
			isPushed = true; // Bắt đầu xoay cần gạt khi người chơi va chạm
			Debug.Log("Player pushed the lever.");
		}
	}

	// Xử lý khi người chơi rời khỏi vùng va chạm (có thể cho cần gạt trở về vị trí ban đầu)
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag(playerTag))
		{
			isReturning = true; // Quay cần gạt về vị trí ban đầu sau khi người chơi rời đi
			Debug.Log("Lever is returning to its original position.");
		}
	}
}
