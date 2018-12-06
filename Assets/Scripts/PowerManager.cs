using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour {
    public static float power;
    private Text text;

    // Use this for initialization
    void Start()
    {
        power = 5;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Power: " + power;
    }
}
