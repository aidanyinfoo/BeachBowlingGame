using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    BallController ballController;
    public Transform target;
    public bool moveCam = true;
    public bool canEnter = true;
    private float smoothing = 5.0f;

    // Use this for initialization
    void Start () {
        ballController = target.GetComponent<BallController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!ballController.hitPins && target.position.z < -10f)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + new Vector3(0, 3.5f, -5.5f), smoothing * Time.deltaTime);
        }
        else if(ballController.hitPins && moveCam || target.position.z > -10f && moveCam)
        {
            
            transform.position = Vector3.Lerp(transform.position, new Vector3 (1.2f, transform.position.y + 0.5f, -7f), 1f *  Time.deltaTime);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(45, transform.eulerAngles.y, transform.eulerAngles.z), 1f* Time.deltaTime);
           if(canEnter)
            {
                Invoke("stopCam", 2f);
                canEnter = false;
            }
        }
       
    }

    void stopCam()
    {
        moveCam = false;
    }

    public void resetRotation()
    {
        transform.eulerAngles = new Vector3(30, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
