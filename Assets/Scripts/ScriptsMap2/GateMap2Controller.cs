using UnityEngine;

public class GateMap2Controller : MonoBehaviour
{
    private bool isFireCharacterInFireGate = false;
    private bool isWaterCharacterInWaterGate = false;
    private FGate fGate;
    private WGate wGate;
    private UIManager uiManager;
    private AudioManager audioManager;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        fGate = FindObjectOfType<FGate>();
        wGate = FindObjectOfType<WGate>();
        audioManager = FindObjectOfType<AudioManager>();

    }
    private void Update()
    {
        CheckAndCloseGates();
    }
    public void FireIn()
    {
        isFireCharacterInFireGate= true;
        CheckAndCloseGates();
    }
    public void WaterIn()
    {
        isWaterCharacterInWaterGate = true;
        CheckAndCloseGates();
    }
    public void WaterOut()
    {
        isWaterCharacterInWaterGate = false;
        CheckAndCloseGates();
    }
    public void FireOut()
    {
        isFireCharacterInFireGate = false;
        CheckAndCloseGates();
    }

    // Kiểm tra điều kiện và đóng cổng nếu tất cả điều kiện đều đúng
    private void CheckAndCloseGates()
    {
        if (isFireCharacterInFireGate && isWaterCharacterInWaterGate)
        {
            fGate.CloseGate();
            wGate.CloseGate();
            audioManager.PlayGoInGate();
            uiManager.Win();
            isWaterCharacterInWaterGate= false;
            isFireCharacterInFireGate = false;
        }
    }
}
