using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

    
    public bool hasBeenBowled = false;
    public bool hitPins = false;
    public bool allowControl = false;
    public Slider powerSlider;

    public string aimingMode = "move";
    public float power = 20f;
    private LineRenderer line;
    private Ray lineRay;
    private  Rigidbody rbody;
    private bool canBowl;
    private AudioSource rollingSound;




    // Use this for initialization
    void Start () {
        rollingSound = GetComponent<AudioSource>();
        line = GetComponent<LineRenderer>();
        lineRay = new Ray();
        rbody = GetComponent<Rigidbody>();
        rbody.constraints = RigidbodyConstraints.FreezeRotation;
        canBowl = true;
        powerSlider.value = 5f;
	}

    // Update is called once per frame
    void Update()
    {
        AdjustSound();
        if (allowControl)
        {
            float x = Input.GetAxis("Horizontal") * Time.deltaTime;
            aim(x);
            setLine();

            //Switch between ball rotation and ball movement
            if (Input.GetKeyDown(KeyCode.Space))
            {
                x = 0.0f;
                if (aimingMode == "move")
                {
                    aimingMode = "rotate";
                    rbody.constraints = RigidbodyConstraints.None;
                    rbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                    
                }
                else
                {
                    aimingMode = "move";
                    rbody.constraints = RigidbodyConstraints.FreezeRotation;
                }
            }

            if (transform.rotation.eulerAngles.y > 90f && transform.rotation.eulerAngles.y < 270f)
            {
                line.startColor = Color.red;
                line.endColor = Color.red;
                canBowl = false;
            }
            else
            {
                line.startColor = Color.yellow;
                line.endColor = Color.magenta;
                canBowl = true;
            }
               

            //Bowl the ball
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (canBowl)
                {
                    rollingSound.Play();
                    rbody.constraints = RigidbodyConstraints.None;
                    line.enabled = false;
                    rbody.velocity = transform.forward * power;
                    allowControl = false;
                }
            }

        }

    }

    void AdjustSound()
    {
        rollingSound.volume = (rbody.velocity.x + rbody.velocity.y + rbody.velocity.z) / 50f;
        if(rollingSound.volume > 0.4f)
        {
            rollingSound.volume = 0.4f;
        }

    }
        
    void aim(float x)
    {

        if (aimingMode == "move")
        {
            transform.Translate(x * 3.0f, 0, 0, Space.World);
   
        }
        else if (aimingMode == "rotate")
        {
            transform.Rotate(0, x * 50.0f, 0);
        }
    }


        void setLine()
        {
        line.enabled = true;
        line.startWidth = 0.25f;
        line.endWidth = 0.1f;
        line.startColor = Color.yellow;
        line.endColor = Color.magenta;
        line.SetPosition(0, transform.position);
        lineRay.origin = transform.position;
        lineRay.direction = transform.forward;

        line.SetPosition(1, lineRay.origin + lineRay.direction * 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "pin")
        {
            hitPins = true;
        }
        
    }

    public void ChangePower()
    {
        PowerManager.power = powerSlider.value;
        power = powerSlider.value + 15;
        
    }

}
