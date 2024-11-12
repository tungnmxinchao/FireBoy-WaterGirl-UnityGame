using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ruby : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		// Kiểm tra xem đối tượng va chạm có tag "WG" hoặc "FB" không
		if (other.CompareTag("WG") || other.CompareTag("FB"))
		{
			// Hủy đối tượng này
			Destroy(gameObject);
		}
	}
}
