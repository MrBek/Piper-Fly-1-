using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public static Tutorial tutorial;

    public GameObject[] barriers;
    List<GameObject> barrierss = new List<GameObject>();
    public GameObject barrierStart;

    public int tutorialStage = 0;
    public bool isUp = true;
    public bool endTutorial=false;
    public GameObject readyPanel, touchPanel, releasePanel,homePanel;
    public bool tutorialStarted = false;

    // Start is called before the first frame update
    private void Awake()
    {

        Application.targetFrameRate = 300;


        if (tutorial)
            Destroy(gameObject);
        tutorial = this;

    }
    void Start()
    {
        if (PlayerPrefs.HasKey("TutorialFinished"))
        {
            SceneManager.LoadScene(1);
        }
    }


    public void GenerateBarrier(GameObject barrier)
    {
        barrierss.Add(Instantiate(barriers[Random.Range(0, barriers.Length)], barrier.transform.Find("EndPos").gameObject.transform.position, Quaternion.identity));
    }
    public void startTutorial()
    {
        StartCoroutine(Start_());
    }


    IEnumerator Start_()
    {
        yield return new WaitForSeconds(1.2f);
        PlayerMovement.isStarted = true;
        tutorialStarted = true;
        readyPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialStarted)
        {
            switch (tutorialStage)
            {
                case 0:
                    if (Input.GetMouseButton(0))
                    {
                        releasePanel.SetActive(true);
                        Time.timeScale = 0;
                    }
                    else
                    {
                        releasePanel.SetActive(false);
                        Time.timeScale = 1;
                    }
                    /*                    canMove = !Input.GetMouseButton(0);
                                        canRotate = !Input.GetMouseButton(0);
                    */
                    break;
                case 1:
                    if (Input.GetMouseButton(0))
                    {
                        touchPanel.SetActive(false);
                        Time.timeScale = 1;
                    }
                    else
                    {
                        touchPanel.SetActive(true);
                        Time.timeScale = 0;
                    }
                    break;
                case 2:
                    if (Input.GetMouseButton(0))
                    {
                        releasePanel.SetActive(true);
                        Time.timeScale = 0;
                    }
                    else
                    {
                        releasePanel.SetActive(false);
                        Time.timeScale = 1;
                    }
                    break;
                case 3:
                    if (Input.GetMouseButton(0))
                    {
                        touchPanel.SetActive(false);
                        Time.timeScale = 1;
                    }
                    else
                    {
                        touchPanel.SetActive(true);
                        Time.timeScale = 0;
                    }
                    break;
                case 4:
                    if (Input.GetMouseButton(0))
                    {
                        releasePanel.SetActive(true);
                        Time.timeScale = 0;
                    }
                    else
                    {
                        releasePanel.SetActive(false);
                        Time.timeScale = 1;
                    }
                    break;
                case 5:
                    if (Input.GetMouseButton(0))
                    {
                        touchPanel.SetActive(false);
                        Time.timeScale = 1;
                    }
                    else
                    {
                        touchPanel.SetActive(true);
                        Time.timeScale = 0;
                    }
                    break;
                case 6:
                    if (Input.GetMouseButton(0))
                    {
                        releasePanel.SetActive(true);
                        Time.timeScale = 0;
                    }
                    else
                    {
                        releasePanel.SetActive(false);
                        Time.timeScale = 1;
                    }
                    break;
            }
        }
    }
    public void finishTutorial()
    {
        PlayerPrefs.SetInt("TutorialFinished", 1);
        SceneManager.LoadScene(1);
    }

    public void nextStep()
    {
        tutorialStage++;
        if (tutorialStage == 7)
        {
            homePanel.SetActive(true);
                endTutorial = true;
        }
        isUp = tutorialStage % 2 == 0;
    }
}
