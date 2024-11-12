using UnityEngine;

public class LiftM : MonoBehaviour
{
    [SerializeField] LeverM LR; // Right lever
    [SerializeField] LeverM LL; // Left lever
    Vector3 newPosition;
    Vector3 oldPosition;

    void Start()
    {
        newPosition = new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z);
        oldPosition = transform.position;
    }

    void Update()
    {
        // Kiểm tra nếu MoveLiftU được kích hoạt, lift sẽ di chuyển lên
        if (LR.MoveLiftU || LL.MoveLiftU)
        {
            if (transform.position != newPosition)
            {
                transform.position = Vector3.Lerp(transform.position, newPosition, 0.01f);

                if (Vector3.Distance(transform.position, newPosition) < 0.01f)
                {
                    LR.SetLiftAtTargetPosition(true);
                    LL.SetLiftAtTargetPosition(true);
                    LR.MoveLiftU = false; // Tắt MoveLiftU khi đã đạt đến đỉnh
                    LL.MoveLiftU = false;
                }
            }
        }
        // Kiểm tra nếu MoveLiftD được kích hoạt, lift sẽ di chuyển xuống
        else if (LR.MoveLiftD || LL.MoveLiftD)
        {
            if (transform.position != oldPosition)
            {
                transform.position = Vector3.Lerp(transform.position, oldPosition, 0.01f);

                if (Vector3.Distance(transform.position, oldPosition) < 0.01f)
                {
                    LR.SetLiftAtTargetPosition(false);
                    LL.SetLiftAtTargetPosition(false);
                    LR.MoveLiftD = false; // Tắt MoveLiftD khi đã đạt đến vị trí ban đầu
                    LL.MoveLiftD = false;
                }
            }
        }
    }
}
