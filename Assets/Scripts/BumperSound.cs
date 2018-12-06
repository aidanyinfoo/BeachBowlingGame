using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperSound : MonoBehaviour {
    private AudioSource hitSound;
	// Use this for initialization
	void Start () {
        hitSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        hitSound.Play();
    }
}
