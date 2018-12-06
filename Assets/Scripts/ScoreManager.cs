using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public static int score;
    public static int round;
    private Text scoreText;
    public Text roundText;

	// Use this for initialization
	void Start () {
        score = 0;
        scoreText = GetComponent<Text>();
        
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Score: " + score;
        roundText.text = "Round: " + round + " / 5";
	}
}
