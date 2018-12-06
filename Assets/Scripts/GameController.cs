using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{



    public GameObject ball;
    public GameObject pin;
    public Camera mainCamera;
    public Canvas canvas;
    public Text displayRoundScore;


    private bool gameover = false;
    public int roundNumber;
    private BallController ballController;
    private Rigidbody ballRBody;
    private bool endRound;
    private Vector3[] pinPositions = new Vector3[10];
    private Vector3[] pinPositions2 = new Vector3[10];
    private Vector3[] pinPositions3 = new Vector3[10];
    private Vector3[] pinPositions4 = new Vector3[10];



    private GameObject[] pins;
    int roundScore = 0;
    private Animator animator;


    // Use this for initialization
    void Start()
    {
        animator = canvas.GetComponent<Animator>();
        ballController = ball.GetComponent<BallController>();
        ballRBody = ball.GetComponent<Rigidbody>();
        endRound = false;

        pinPositions[0] = new Vector3(1.2f, 0.32f, -2.4f);
        pinPositions[1] = new Vector3(0.6f, 0.32f, -1.6f);
        pinPositions[2] = new Vector3(1.8f, 0.32f, -1.6f);
        pinPositions[3] = new Vector3(0f, 0.32f, -0.8f);
        pinPositions[4] = new Vector3(1.2f, 0.32f, -0.8f);
        pinPositions[5] = new Vector3(2.4f, 0.32f, -0.8f);
        pinPositions[6] = new Vector3(-0.6f, 0.32f, 0f);
        pinPositions[7] = new Vector3(0.6f, 0.32f, 0f);
        pinPositions[8] = new Vector3(1.8f, 0.32f, 0f);
        pinPositions[9] = new Vector3(3f, 0.32f, 0f);

        pinPositions2[0] = new Vector3(1.2f, 0.32f, -2.4f);
        pinPositions2[1] = new Vector3(0.6f, 0.32f, -1.6f);
        pinPositions2[2] = new Vector3(1.8f, 0.32f, -1.6f);
        pinPositions2[3] = new Vector3(0f, 0.32f, -2.4f);
        pinPositions2[4] = new Vector3(1.2f, 0.32f, -0.8f);
        pinPositions2[5] = new Vector3(2.4f, 0.32f, -2.4f);
        pinPositions2[6] = new Vector3(-0f, 0.32f, 0.8f);
        pinPositions2[7] = new Vector3(0.6f, 0.32f, 0f);
        pinPositions2[8] = new Vector3(1.8f, 0.32f, 0f);
        pinPositions2[9] = new Vector3(2.4f, 0.32f, 0.8f);

        pinPositions3[0] = new Vector3(1.2f, 0.32f, -2.4f);
        pinPositions3[1] = new Vector3(0.6f, 0.32f, -1.6f);
        pinPositions3[2] = new Vector3(1.8f, 0.32f, -1.6f);
        pinPositions3[3] = new Vector3(0f, 0.32f, -2.4f);
        pinPositions3[4] = new Vector3(1.2f, 0.32f, -0.8f);
        pinPositions3[5] = new Vector3(2.4f, 0.32f, -2.4f);
        pinPositions3[6] = new Vector3(-0.6f, 0.32f, -1.6f);
        pinPositions3[7] = new Vector3(0.6f, 0.32f, 0f);
        pinPositions3[8] = new Vector3(1.8f, 0.32f, 0f);
        pinPositions3[9] = new Vector3(3f, 0.32f, -1.6f);

        pinPositions4[0] = new Vector3(1.2f, 0.32f, -2.4f);
        pinPositions4[1] = new Vector3(0.6f, 0.32f, -1.6f);
        pinPositions4[2] = new Vector3(1.8f, 0.32f, -1.6f);
        pinPositions4[3] = new Vector3(1.2f, 0.32f, 0.8f);
        pinPositions4[4] = new Vector3(1.2f, 0.32f, -0.8f);
        pinPositions4[5] = new Vector3(0.6f, 0.32f, 1.6f);
        pinPositions4[6] = new Vector3(1.8f, 0.32f, 1.6f);
        pinPositions4[7] = new Vector3(0.6f, 0.32f, 0f);
        pinPositions4[8] = new Vector3(1.8f, 0.32f, 0f);
        pinPositions4[9] = new Vector3(1.2f, 0.32f, 2.4f);

        StartGame();

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartGame();
            }

            if (ballController.hitPins && !endRound || ball.transform.position.z > 2f && !endRound || ball.transform.position.z > -23f && ballRBody.velocity.z < 3f && !endRound)
            {
                Invoke("NextRound", 5f);
                endRound = true;

            }
        }

    }

    void StartGame()
    {
        roundNumber = 1;
        ScoreManager.round = roundNumber;
        Restart();
        SpawnPins();
        AllowControl();
    }

    void NextRound()
    {
        if (roundNumber >= 5)
        {
            checkPins();
            endRound = true;
            FinishGame();
        }
        else
        {
            checkPins();
            Restart();
            roundNumber += 1;
            SpawnPins();
            //Give the player control once the score has finished updating
            Invoke("AllowControl", 2.7f);

            ScoreManager.round = roundNumber;
            endRound = false;
        }
    }

    void FinishGame()
    {
        gameover = true;
        StopMovement();
        animator.SetTrigger("GameOver");
        //display final score, maybe highscores??
    }

    void SpawnPins()
    {
        // Instantiate the pins in their positions
        Vector3[] spawnPositions;

        if (roundNumber == 1 || roundNumber == 5)
        {
            spawnPositions = pinPositions;
        }
        else if(roundNumber == 2) {
            spawnPositions = pinPositions2;
        }
        else if (roundNumber == 3)
        {
            spawnPositions = pinPositions3;
        }
        else
        {
            spawnPositions = pinPositions4;
        }



        foreach (Vector3 position in spawnPositions)
        {
            Instantiate(pin, position, new Quaternion());
        }

    }

    void checkPins()
    {
        roundScore = 0;
        pins = GameObject.FindGameObjectsWithTag("pin");
        foreach (GameObject pin in pins)
        {
            Transform pinTransform = pin.GetComponent<Transform>();
            if (pinTransform.rotation.x > 0.1 || pinTransform.rotation.x < -0.1 || pinTransform.rotation.z > 0.1 || pinTransform.rotation.z < -0.1)
            {
                roundScore += 1;
            }


        }
        UpdateScore();
    }

    void UpdateScore()
    {
        displayRoundScore.text = "+" + roundScore;
        animator.SetTrigger("ShowRoundScore");
        Invoke("SetScore", 2.7f);
    }

    void SetScore()
    {
        ScoreManager.score += roundScore;
    }


    void Restart()
    {
        //Stop the ball from moving and reset its position
        StopMovement();
        ball.transform.SetPositionAndRotation(new Vector3(1.2f, 0.2f, -24f), Quaternion.identity);
        ballController.hitPins = false;
        ballController.aimingMode = "move";
        ballRBody.constraints = RigidbodyConstraints.FreezeRotation;

        //Remove the old pins
        pins = GameObject.FindGameObjectsWithTag("pin");
        foreach (GameObject pin in pins)
        {
            Destroy(pin);
        }

        //reset camera position, rotation
        mainCamera.GetComponent<CameraController>().moveCam = true;
        mainCamera.GetComponent<CameraController>().canEnter = true;
        mainCamera.GetComponent<CameraController>().resetRotation();

    }

    void AllowControl()
    {
        ballController.allowControl = true;
    }

    void StopMovement()
    {
        if (gameover)
        {
            pins = GameObject.FindGameObjectsWithTag("pin");
            foreach (GameObject pin in pins)
            {
                Rigidbody pinRBody = pin.GetComponent<Rigidbody>();
                pinRBody.isKinematic = true;
            }
        }

        ballRBody.velocity = Vector3.zero;
        ballRBody.angularVelocity = Vector3.zero;
    }
}
