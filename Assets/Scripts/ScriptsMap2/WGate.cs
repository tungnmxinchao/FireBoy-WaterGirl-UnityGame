using System.Collections;
using UnityEngine;

public class WGate : MonoBehaviour
{
    private AudioManager audioManager;
    public GameObject GateOpening;
    public GameObject GateClosed;
    public GameObject GateOpened;
    public GameObject WaterEnd; 
    public GameObject WaterStand;
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
        WaterEnd.SetActive(false);
        gateController = FindObjectOfType<GateMap2Controller>();
    }
    private void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("WG") && !isGateOpened && !isOpening)
        {
            openGateCoroutine = StartCoroutine(OpenGate());
        }
        if (collision.CompareTag("WG")&& isGateOpened)
        {
            gateController.WaterIn();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WG") && isOpening)
        {
            // Stop opening and reset to closed state
            if (openGateCoroutine != null)
            {
                StopCoroutine(openGateCoroutine);
                openGateCoroutine = null;
            }
            ResetGateToClosed();
        }
        if (collision.CompareTag("WG") && isGateOpened)
        {
            gateController.WaterOut();
        }
    }

    private IEnumerator OpenGate()
    {
        isOpening = true;

        // Bật trạng thái "đang mở" và tắt trạng thái "đóng"
        GateClosed.SetActive(false);
        GateOpening.SetActive(true);
        audioManager.PlaySFX(audioManager.gateMove);
        yield return new WaitForSeconds(openGateTime);
        isOpening = false;
        isGateOpened = true;
        GateOpening.SetActive(false) ;
        GateOpened.SetActive(true) ;
    }
    public void CloseGate()
    {
        StartCoroutine(CloseGateCoroutine());
    }
    private IEnumerator CloseGateCoroutine()
    {
        WaterStand.SetActive(false);
        WaterEnd.SetActive(true);
        yield return new WaitForSeconds(1f);

        // Sau khi WaterEnd hoàn tất, chuyển trạng thái về GateClosed
        WaterEnd.SetActive(false);
        GateOpened.SetActive(false);
        GateClosed.SetActive(true);
        isGateOpened = false;
    }
    private void ResetGateToClosed()
    {
        GateClosed.SetActive(true);
        GateOpening.SetActive(false);
        GateOpened.SetActive(false);
        isOpening = false;
        isGateOpened = false;
    }


}
