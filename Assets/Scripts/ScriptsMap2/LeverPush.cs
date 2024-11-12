using System.Collections;
using UnityEngine;

public class LeverPush : MonoBehaviour
{
    private AudioManager audioManager;
    // Đối tượng di chuyển khi chạm vào lever
    public GameObject door; // Kéo thả đối tượng cửa vào đây trong Unity

    // Trạng thái kích hoạt của lever
    private bool isActivated = false;

    // Thời gian chuyển động cho góc quay
    public float transitionTime = 1.0f;

    // Góc ban đầu và góc khi đã kích hoạt của lever (Rotation Z)
    private Quaternion initialRotation = Quaternion.Euler(0f, 0f, 29f);
    private Quaternion activatedRotation = Quaternion.Euler(0f, 0f, -17.9f);

    // Vị trí ban đầu và vị trí cục bộ sau khi kích hoạt của lever
    private Vector3 initialLocalPosition = new Vector3(-8.9f, -2.7f, -2f);
    private Vector3 activatedLocalPosition = new Vector3(-8.6f, -2.7f, -2f);

    // Biến để theo dõi trạng thái hiện tại của lever (ban đầu hay kích hoạt)
    private bool isInInitialPosition = true;

    // Vị trí Y ban đầu và vị trí Y khi cửa được mở
    private float doorClosedY = -2.4f; // Vị trí khi đóng
    private float doorOpenY = -1.78f; // Vị trí khi mở

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        // Thiết lập góc quay và vị trí ban đầu
        transform.localRotation = initialRotation;
        transform.localPosition = initialLocalPosition;
    }

    // Hàm kích hoạt khi đối tượng va chạm
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("WG") || collision.CompareTag("FB")) && !isActivated)
        {
            // Bắt đầu coroutine để chuyển đổi trạng thái lever và di chuyển cửa
            StartCoroutine(ToggleLeverPosition());
            audioManager.PlaySFX(audioManager.hitLever);
        }
    }

    // Coroutine để chuyển đổi giữa hai trạng thái góc quay và vị trí của lever
    private IEnumerator ToggleLeverPosition()
    {
        isActivated = true;
        float elapsedTime = 0f;

        // Xác định góc quay và vị trí mục tiêu dựa trên trạng thái hiện tại
        Quaternion startRotation = transform.localRotation;
        Quaternion targetRotation = isInInitialPosition ? activatedRotation : initialRotation;

        Vector3 startPosition = transform.localPosition;
        Vector3 targetPosition = isInInitialPosition ? activatedLocalPosition : initialLocalPosition;

        // Chuyển đổi góc quay và vị trí một cách mượt mà
        while (elapsedTime < transitionTime)
        {
            transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / transitionTime);
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / transitionTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Đảm bảo góc quay và vị trí đạt đúng giá trị cuối cùng
        transform.localRotation = targetRotation;
        transform.localPosition = targetPosition;

        // Di chuyển cửa từ Y -2.4 sang Y -1.78
        Vector3 doorStartPosition = door.transform.localPosition;
        Vector3 doorTargetPosition = new Vector3(doorStartPosition.x, isInInitialPosition ? doorOpenY : doorClosedY, doorStartPosition.z);

        // Di chuyển cửa một cách mượt mà
        elapsedTime = 0f; // Reset thời gian để di chuyển cửa
        while (elapsedTime < transitionTime)
        {
            door.transform.localPosition = Vector3.Lerp(doorStartPosition, doorTargetPosition, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Đảm bảo cửa đạt đúng vị trí cuối cùng
        door.transform.localPosition = doorTargetPosition;

        // Đảo ngược trạng thái lever để sẵn sàng cho lần chuyển đổi tiếp theo
        isInInitialPosition = !isInInitialPosition;

        // Cho phép lever được kích hoạt lại
        isActivated = false;
    }
}
