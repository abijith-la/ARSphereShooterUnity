using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class hitBall : MonoBehaviour
{
    private GameObject cam;
    private Rigidbody rb;
    private Collider col;
    private bool isFirstTime;
    // Start is called before the first frame update

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        col = GetComponent<Collider>();
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        isFirstTime = true;
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if ( (Input.GetMouseButtonDown(0) && isFirstTime) || (Input.touchCount > 0 && isFirstTime && Input.touches[0].phase == TouchPhase.Began) )
        {   
            col.enabled = false;
            rb.useGravity = true;
            rb.AddForce(cam.transform.forward*800);
            isFirstTime = false;
            StartCoroutine(SelfDestruct());
            Instantiate(rb);
            col.enabled = true;

        }

        else if (isFirstTime)
        {
            rb.transform.position = cam.transform.position;
        }
    }
}
