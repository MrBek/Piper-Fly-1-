using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{
    public static int oneGamePlay = 0, killCount = 0;

    public static PlayerMovement player;
    public float ratio = 0.0f, rotSpeed;
    Rigidbody rb;
    public float forwardSpeed;
    public bool playerKill = false, isVisible = false;
    public static bool isStarted = false;
    public GameObject particleSimple, particleDie;
    public bool isTutorial = false;
    public float timeDeltaTime = 0;
    Vector3 gravity;

    private void Awake()
    {
        if (player == null)
            player = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!isTutorial)
            gameObject.SetActive(false);

        isStarted = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted)
        {
            /*if (isTutorial)
            {
                if(Tutorial.tutorial.canRotate)
                ratio = Mathf.Clamp(Input.GetMouseButton(0) ? ratio - rotSpeed * (Time.deltaTime * 0.9f) : ratio + rotSpeed * (Time.deltaTime * 0.9f), -45.0f, 45.0f);
                if (Tutorial.tutorial.canMove)
                    rb.velocity = new Vector3(forwardSpeed, ratio * (forwardSpeed / 45.0f), 0);
                else
                    rb.velocity = Vector3.zero;
            }
            else
            */
            if (isTutorial)
            {
                if (Tutorial.tutorial.endTutorial)
                {
                    ratio = Mathf.Lerp(ratio, 0.0f, rotSpeed * (timeDeltaTime * 0.9f));
                }
                else
                if(Time.timeScale!=0)
                ratio = Mathf.Clamp(!Tutorial.tutorial.isUp ? ratio - rotSpeed * (timeDeltaTime * 0.9f) : ratio + rotSpeed * (timeDeltaTime * 0.9f), -45.0f, 45.0f);
                rb.velocity = new Vector3(forwardSpeed, ratio * (forwardSpeed / 45.0f), 0);
            }
            else
            {
                ratio = Mathf.Clamp(Input.GetMouseButton(0) ? ratio - rotSpeed * (Time.deltaTime * 0.9f) : ratio + rotSpeed * (Time.deltaTime * 0.9f), -45.0f, 45.0f);
                rb.velocity = new Vector3(forwardSpeed, ratio * (forwardSpeed / 45.0f), 0);
            }
            transform.rotation = Quaternion.Euler(0, 0, ratio);
        }
    }

    public void PlayerKill()
    {
        if (!isTutorial)
        {
            isStarted = false;
            LevelGenerator.levelGenerator.isStart = false;
            rb.velocity = new Vector2(0, 0);
            transform.rotation = Quaternion.identity;
            ChangingPlayer.changingPlayer.trails[PlayerPrefs.GetInt("enabled_")].SetActive(false);
            LevelGenerator.levelGenerator.allas.SetActive(true);
            Instantiate(particleDie, transform.position, Quaternion.identity);
            LevelGenerator.levelGenerator.SetPointText(PlayerPrefs.GetInt("point"));
            playerKill = true;
            AudioManager.audioManager.PlaySound("playerKill");

            LevelGenerator.levelGenerator.collectedPointAds.text = "you collected " + Point.pointCount + "<sprite=0>";
            LevelGenerator.levelGenerator.noBtnText.text = "get + " + Point.pointCount + "<sprite=0>";
            LevelGenerator.levelGenerator.mainPanel.SetActive(true);
            LevelGenerator.levelGenerator.scorePanel.SetActive(false);
            LevelGenerator.levelGenerator.currentScore.gameObject.SetActive(true);
            LevelGenerator.levelGenerator.bestScoreReplay.gameObject.SetActive(true);
            LevelGenerator.levelGenerator.bestScore.gameObject.SetActive(false);
            LevelGenerator.levelGenerator.SetBestScore(PlayerPrefs.GetInt("bestScore"));

            if (Point.pointCount > 0)
                LevelGenerator.levelGenerator.Corotine();

            gameObject.SetActive(false);
            LevelGenerator.levelGenerator.isStarted = true;

            killCount++;
            if (killCount == 7)
            {
                killCount = 0;
            }

        }
        else
        {
            isStarted = false;
            rb.velocity = new Vector2(0, 0);
            transform.rotation = Quaternion.identity;
            Instantiate(particleDie, transform.position, Quaternion.identity);
            playerKill = true;
            gameObject.SetActive(false);
        }
    }
}
