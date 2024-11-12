using System.Collections;
using UnityEngine;

public class FGate : MonoBehaviour
{
    private AudioManager audioManager;
    public GameObject GateOpening;
    public GameObject GateClosed;
    public GameObject GateOpened;
    public GameObject FireEnd;
    public GameObject FireStand;
    private bool isOpening = false;
    private bool isGateOpened = false;
    private const float openGateTime = 1.5f;
    private GateMap2Controller gateController;
    private Coroutine openGateCoroutine;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        // Đảm bảo trạng thái ban đầu là cửa đóng
        GateClosed.SetActive(true);
        GateOpened.SetActive(false);
        GateOpening.SetActive(false);
        FireEnd.SetActive(false);
        gateController = FindObjectOfType<GateMap2Controller>();

    }

    private void Update()
    {
        // Có thể bổ sung logic vào Update nếu cần
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("FB") && !isGateOpened && !isOpening)
        {
            openGateCoroutine = StartCoroutine(OpenGate());
        }if(collision.CompareTag("FB") && isGateOpened)
        {
            gateController.FireIn();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FB") && isOpening)
        {
            // Stop opening and reset to closed state
            if (openGateCoroutine != null)
            {
                StopCoroutine(openGateCoroutine);
                openGateCoroutine = null;
            }
            ResetGateToClosed();
        }
        if (collision.CompareTag("FB") && isGateOpened)
        {
            gateController.FireOut();
        }
    }

    private IEnumerator OpenGate()
    {
        isOpening = true;

        // Chuyển đổi trạng thái sang "đang mở" và tắt trạng thái "đóng"
        GateClosed.SetActive(false);
        GateOpening.SetActive(true);
        audioManager.PlaySFX(audioManager.gateMove);
        yield return new WaitForSeconds(openGateTime);

        isOpening = false;
        isGateOpened = true;
        GateOpening.SetActive(false);
        GateOpened.SetActive(true);
    }

    public void CloseGate()
    {
        StartCoroutine(CloseGateCoroutine());
    }
    private IEnumerator CloseGateCoroutine()
    {
        FireStand.SetActive(false);
        FireEnd.SetActive(true);
        yield return new WaitForSeconds(1f);

        FireEnd.SetActive(false);
        GateOpened.SetActive(false);
        GateClosed.SetActive(true);
        isGateOpened = false;
    }
    private void ResetGateToClosed()
    {
        // Reset all gate states to closed
        GateClosed.SetActive(true);
        GateOpening.SetActive(false);
        GateOpened.SetActive(false);
        isOpening = false;
        isGateOpened = false;
    }
}
