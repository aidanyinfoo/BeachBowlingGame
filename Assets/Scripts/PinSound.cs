using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSound : MonoBehaviour {

    private AudioSource hitSound;
    private bool hasPlayedSound;
	// Use this for initialization
	void Start () {
        hitSound = GetComponent<AudioSource>();
        hasPlayedSound = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "ball" && !hasPlayedSound)
        {
            hitSound.Play();
            hasPlayedSound = true;
        }
    }
}
