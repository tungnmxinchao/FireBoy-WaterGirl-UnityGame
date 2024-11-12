using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public GameObject Dead;
    public GameObject PushUp;
    public Animator animator;
    private Rigidbody2D rd;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D collider2 = PushUp.GetComponent<Collider2D>();
        Bounds WGbounds = GameObject.FindGameObjectWithTag("WG").GetComponent<Collider2D>().bounds;
        Bounds FBbounds = GameObject.FindGameObjectWithTag("FB").GetComponent<Collider2D>().bounds;
        Vector2[] WGcorners = new Vector2[4]
        {
        new Vector2(WGbounds.min.x, WGbounds.min.y), // Góc dưới trái
        new Vector2(WGbounds.max.x, WGbounds.min.y), // Góc dưới phải
        new Vector2(WGbounds.min.x, WGbounds.max.y), // Góc trên trái
        new Vector2(WGbounds.max.x, WGbounds.max.y)  // Góc trên phải
        };
        Vector2[] FBcorners = new Vector2[4]
        {
        new Vector2(FBbounds.min.x, FBbounds.min.y), // Góc dưới trái
        new Vector2(FBbounds.max.x, FBbounds.min.y), // Góc dưới phải
        new Vector2(FBbounds.min.x, FBbounds.max.y), // Góc trên trái
        new Vector2(FBbounds.max.x, FBbounds.max.y)  // Góc trên phải
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
			//Dead.transform.position = GameObject.FindGameObjectWithTag("WG").transform.position;
			//GameObject.FindGameObjectWithTag("WG").SetActive(false);
			//Dead.SetActive(true);
			rd = GameObject.FindGameObjectWithTag("WG").GetComponent<Rigidbody2D>();
			rd.AddForce(Vector2.up * 0.04f, ForceMode2D.Impulse);
        }
        if (FBIsTouching)
        {
            //Dead.transform.position = GameObject.FindGameObjectWithTag("FB").transform.position;
            //GameObject.FindGameObjectWithTag("FB").SetActive(false);
            //Dead.SetActive(true);

            rd = GameObject.FindGameObjectWithTag("FB").GetComponent<Rigidbody2D>();
            rd.AddForce(Vector2.up * 0.04f, ForceMode2D.Impulse);
        }
        //if (animator.isActiveAndEnabled && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        //{
        //    Debug.Log(1);
        //    Time.timeScale = 0f;
        //}
    }
}
