using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public GameObject PushUp;
    public GameObject Wind; // Thêm tham chiếu đến đối tượng Wind
    public GameObject Animation; // Thêm tham chiếu đến đối tượng Animation
    public GameObject Body; // Thêm tham chiếu đến đối tượng Body
    private Rigidbody2D rd;

    private bool isActive = false;

    void Start()
    {
        if (PushUp == null)
        {
            Debug.Log("PushUp object is not assigned!");
        }

        if (Wind == null)
        {
            Debug.Log("Wind object is not assigned!");
        }
        else
        {
            Wind.SetActive(false); // Đảm bảo Wind không hoạt động khi bắt đầu
        }

        if (Animation == null)
        {
            Debug.Log("Animation object is not assigned!");
        }
        else
        {
            Animation.SetActive(false); // Đảm bảo Animation không hoạt động khi bắt đầu
        }

        if (Body == null)
        {
            Debug.Log("Body object is not assigned!");
        }
    }

    void Update()
    {
        if (!isActive) return;

        Collider2D collider2 = PushUp.GetComponent<Collider2D>();
        Bounds WGbounds = GameObject.FindGameObjectWithTag("WG").GetComponent<Collider2D>().bounds;
        Bounds FBbounds = GameObject.FindGameObjectWithTag("FB").GetComponent<Collider2D>().bounds;

        Vector2[] WGcorners = new Vector2[4]
        {
            new Vector2(WGbounds.min.x, WGbounds.min.y),
            new Vector2(WGbounds.max.x, WGbounds.min.y),
            new Vector2(WGbounds.min.x, WGbounds.max.y),
            new Vector2(WGbounds.max.x, WGbounds.max.y)
        };

        Vector2[] FBcorners = new Vector2[4]
        {
            new Vector2(FBbounds.min.x, FBbounds.min.y),
            new Vector2(FBbounds.max.x, FBbounds.min.y),
            new Vector2(FBbounds.min.x, FBbounds.max.y),
            new Vector2(FBbounds.max.x, FBbounds.max.y)
        };

        bool WGIsTouching = false;
        bool FBIsTouching = false;

        foreach (var corner in WGcorners)
        {
            if (collider2.OverlapPoint(corner))
            {
                WGIsTouching = true;
                break;
            }
        }

        foreach (var corner in FBcorners)
        {
            if (collider2.OverlapPoint(corner))
            {
                FBIsTouching = true;
                break;
            }
        }

        if (WGIsTouching)
        {
            rd = GameObject.FindGameObjectWithTag("WG").GetComponent<Rigidbody2D>();
            rd.AddForce(Vector2.up * 0.08f, ForceMode2D.Impulse);

            Debug.Log("PushUp WG");
        }

        if (FBIsTouching)
        {
            rd = GameObject.FindGameObjectWithTag("FB").GetComponent<Rigidbody2D>();
            rd.AddForce(Vector2.up * 0.08f, ForceMode2D.Impulse);

            Debug.Log("PushUp FB");
        }
    }

    // Phương thức kích hoạt/tắt fan
    public void ActivateFan(bool active)
    {
        isActive = active;

        if (Wind != null)
        {
            Wind.SetActive(active); // Kích hoạt/tắt đối tượng Wind
        }

        if (Animation != null)
        {
            Animation.SetActive(active); // Kích hoạt/tắt đối tượng Animation
        }

        if (Body != null)
        {
            Body.SetActive(!active); // Ẩn Body nếu Fan hoạt động
        }
    }
}
