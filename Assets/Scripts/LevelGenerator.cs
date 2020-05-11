using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UI.Extensions;
using System.Net;


public class LevelGenerator : MonoBehaviour
{
    public static int point;
    public GameObject[] barriers;
    public GameObject easyBarier;
         
    public TextMeshProUGUI Text, pointText, bestScore, costText, buyedText, currentScore, bestScoreReplay, collectedPointAds, noBtnText, RewardText;
    public static bool first = false, restart = false;
    public GameObject indicator, mainPanel, AdsPanel, scorePanel, nextBtn, prevBtn, planets, allas, barrierStart, pausePanel, RewardPanel, Canvas, doppi, readyPanel;
    public Material devorMaterial, togMaterial;
    public static LevelGenerator levelGenerator;
    public Button BuyBtn, musicBtn, soundBtn;
    public Sprite[] musicSprites, soundSprites;
    public Texture[] devorSprites, togSprites;
    List<GameObject> barrierss = new List<GameObject>();
    [HideInInspector]
    public bool musicOn = true, soundOn = true, isGamePaused = false, isStart = false;
    private static int randomMaterial;
    Vector3 pointTextScale;
    public Text timerText;
    public static int startBarierCount = 0;
    public bool isStarted = false;

    private void Awake()
    {
        if (levelGenerator == null)
            levelGenerator = this;
    }

    private void Start()
    {
        SetTexture();
        pointTextScale = pointText.gameObject.transform.localScale;

        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetInt("music", 1);
            PlayerPrefs.SetInt("sound", 1);
            AudioManager.audioManager.PlayMusic("gameplay");
        }
        else
        {
            musicBtn.GetComponent<Image>().sprite = musicSprites[PlayerPrefs.GetInt("music")];
            soundBtn.GetComponent<Image>().sprite = soundSprites[PlayerPrefs.GetInt("sound")];
            musicOn = (PlayerPrefs.GetInt("music") == 1) ? true: false;
            soundOn = (PlayerPrefs.GetInt("sound") == 1)? true: false;
            if (musicOn)
                AudioManager.audioManager.PlayMusic("gameplay");
        }

        if (!PlayerPrefs.HasKey("bestScore"))
        {
            bestScore.text = "best score:\n" + 0;
            PlayerPrefs.SetInt("bestScore", 0);
        }
        else
        {
            bestScore.text = "best score:\n" + PlayerPrefs.GetInt("bestScore");
        }

        if (!PlayerPrefs.HasKey("point"))
        {
            SetPointText(0);
        }
        else
        {
            SetPointText(PlayerPrefs.GetInt("point"));
        }
    }
    GameObject bar;
    public void GenerateBarrier(GameObject barrier)
    {
        if (isStarted && startBarierCount < 6)
        {
            bar = Instantiate(easyBarier, barrier.transform.Find("EndPos").gameObject.transform.position, Quaternion.identity);
            startBarierCount++;
            isStarted = false;
        }
        else
        bar = Instantiate(barriers[Random.Range(0, barriers.Length)], barrier.transform.Find("EndPos").gameObject.transform.position, Quaternion.identity);
        barrierss.Add(bar);
    }

    public void SetScoreText(int k)
    {
        if (PlayerPrefs.GetInt("bestScore") <= k)
            PlayerPrefs.SetInt("bestScore", k);
        Text.text = "score:\n" + k;
        currentScore.text = "current score:\n" + k;
    }


    public void SetBestScore(int best)
    {
        bestScoreReplay.text = "best score:\n" + best;
        if (Application.internetReachability != NetworkReachability.NotReachable)
            ClientManager.clientManager.RankPostData(new RankPostRequest(LBController._lb_Controller._token,SystemInfo.deviceName, "0.0.0.0",1, best, System.DateTime.Now, System.Guid.NewGuid().ToString().Substring(0, 32)));
    }

    public void BuyCharacter()
    {
        if (PlayerPrefs.GetInt("charBuy_" + ScrollSnapBase.scrollSnapBase.CurrentPage) == 0)
        {
            BuyBtn.interactable = true;

            if (PlayerPrefs.GetInt("point") >= ScrollSnapBase.scrollSnapBase.charCost)
            {
                PlayerPrefs.SetInt("point", PlayerPrefs.GetInt("point") - ScrollSnapBase.scrollSnapBase.charCost);
                SetPointText(PlayerPrefs.GetInt("point"));
                PlayerPrefs.SetInt("charBuy_" + ScrollSnapBase.scrollSnapBase.CurrentPage, 1);
                buyedText.text = "bought";
                SetPipersName(ScrollSnapBase.scrollSnapBase.CurrentPage);
                BuyBtn.interactable = false;
            }
            else
                Debug.Log("sizda pul yetarli emas");
        }
        else
            BuyBtn.interactable = false;
    }

    public void StartGame()
    {
        if (PlayerPrefs.GetInt("charBuy_" + ScrollSnapBase.scrollSnapBase.CurrentPage) == 1)
        {
            PlayerPrefs.SetInt("point", PlayerPrefs.GetInt("point") + progress);
            isStart = true;

            readyPanel.SetActive(true);
            allas.SetActive(false);
            mainPanel.SetActive(false);
            currentScore.gameObject.SetActive(false);
            scorePanel.SetActive(true);
            StartCoroutine(changeTexture());
            bestScoreReplay.gameObject.SetActive(false);
            PlayerPrefs.SetInt("enabled_", ScrollSnapBase.scrollSnapBase.CurrentPage);
            bestScore.gameObject.SetActive(true);
            barrierStart.SetActive(true);
            PlayerMovement.player.gameObject.SetActive(true);
            indicator.SetActive(true);

            Wheel.k = 0;
            Point.pointCount = 0;
            SetScoreText(0);
            SetPointText(0);

            if (ScrollSnapBase.scrollSnapBase.CurrentPage == 1)
                doppi.SetActive(true);
            else
                doppi.SetActive(false);

            if (PlayerMovement.player.playerKill)
            {
                SetTexture();

                pointText.gameObject.transform.localScale = pointTextScale;

                restart = true;
                PlayerMovement.player.gameObject.transform.position = Vector3.zero;
                foreach (GameObject bar in barrierss)
                {
                    if (bar != null)
                        Destroy(bar.gameObject);
                }
            }

            PlayerMovement.player.playerKill = false;

            StartCoroutine(Start_());
        }
    }


    IEnumerator Start_()
    {
        yield return new WaitForSeconds(1.2f);
            PlayerMovement.isStarted = true;

        readyPanel.SetActive(false);
    }

    public void BtnActivity(bool next, bool prev)
    {
        nextBtn.SetActive(next);
        prevBtn.SetActive(prev);
    }

    IEnumerator changeTexture()
    {
        yield return new WaitForSeconds(0.01f);
        ChangingPlayer.changingPlayer.ChangeTexture();
    }

    public void SetPipersName_(int i)
    {
        switch (i)
        {
            case 0:buyedText.text = "piper";
                break;
            case 1:
                buyedText.text = "piper bobo";
                break;
            case 2:
                buyedText.text = "fire";
                break;
            case 3:
                buyedText.text = "captain";
                break;
            case 4:
                buyedText.text = "bumblbee";
                break;
            case 5:
                buyedText.text = "hulk";
                break;
            case 6:
                buyedText.text = "bee";
                break;
            case 7:
                buyedText.text = "spider";
                break;
            case 8:
                buyedText.text = "red";
                break;
            case 9:
                buyedText.text = "superman";
                break;
            case 10:
                buyedText.text = "military";
                break;
            case 11:
                buyedText.text = "tron";
                break;
        }
    }

    public void SetPipersName(int i)
    {
        switch (i)
        {
            case 0:costText.text = "piper";
                break;
            case 1:
                costText.text = "piper bobo";
                break;
            case 2:
                costText.text = "fire";
                break;
            case 3:
                costText.text = "captain";
                break;
            case 4:
                costText.text = "bumblbee";
                break;
            case 5:
                costText.text = "hulk";
                break;
            case 6:
                costText.text = "bee";
                break;
            case 7:
                costText.text = "spider";
                break;
            case 8:
                costText.text = "red";
                break;
            case 9:
                costText.text = "superman";
                break;
            case 10:
                costText.text = "military";
                break;
            case 11:
                costText.text = "tron";
                break;
        }
    }

    public void Corotine()
    {
        StartCoroutine(TimerToAds());
    }

    public IEnumerator TimerToAds()
    {
        AdsPanel.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        AdsPanel.SetActive(false);
    }
    public AddsController addsPanel;

    public void WatchAds()
    {
        addsPanel.gameObject.SetActive(true);
        addsPanel.startVideo();

    }

    public void GeneratePlanet(GameObject planet)
    {
        Instantiate(planets, planet.gameObject.transform.Find("EndPos").transform.position, Quaternion.identity);
    }

    public void Music()
    {
        if (musicOn)
        {
            musicOn = false;
            AudioManager.audioManager.StopMusic();
            musicBtn.GetComponent<Image>().sprite = musicSprites[0];
            PlayerPrefs.SetInt("music", 0);
        }
        else
        {
            musicOn = true;
            AudioManager.audioManager.PlayMusic("gameplay");
            musicBtn.GetComponent<Image>().sprite = musicSprites[1];
            PlayerPrefs.SetInt("music", 1);
        }
    }
    public void Sound()
    {
        if (soundOn)
        {
            soundOn = false;
            soundBtn.GetComponent<Image>().sprite = soundSprites[0];
            PlayerPrefs.SetInt("sound", 0);
        }
        else
        {
            soundOn = true;
            soundBtn.GetComponent<Image>().sprite = soundSprites[1];
            PlayerPrefs.SetInt("sound", 1);
        }
    }

    void SetTexture()
    {
        randomMaterial = Random.Range(0, devorSprites.Length);
        devorMaterial.mainTexture = devorSprites[randomMaterial];
        togMaterial.mainTexture = togSprites[randomMaterial];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused && mainPanel.active == false)
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        pausePanel.SetActive(true);
        isGamePaused = true;
        AudioManager.audioManager.StopMusic();
        Time.timeScale = 0;
    }
    
    public void Resume()
    {
        if (isGamePaused)
        {
            pausePanel.SetActive(false);
            isGamePaused = false;
            AudioManager.audioManager.PlayMusic("gameplay");
            Time.timeScale = 1;
        }
    }
    
    public void CloseMoneyGivenPanel()
    {
        
        StartCoroutine(MoneyAdd());
    }

    int progress;

    public void SetPointText(int point)
    {
        pointText.text = "<sprite=0>" + point;
    }

    IEnumerator MoneyAdd()
    {
        progress = Point.pointCount;
        iTween.ScaleTo(pointText.gameObject, iTween.Hash("time", 0.25f, "scale", pointTextScale * 1.2f));
        
        yield return new WaitForSeconds(0.2f);


        while (progress > 0)
        {

            AudioManager.audioManager.PlaySound("ting");
            progress--;
            if (!isStart)
            {
                SetPointText(PlayerPrefs.GetInt("point") + 1);
                PlayerPrefs.SetInt("point", PlayerPrefs.GetInt("point") + 1);
            }
            else
                SetPointText(0);


            if(!isStart)
                yield return new WaitForSeconds(0.5f / Point.pointCount);
        }
        
        iTween.ScaleTo(pointText.gameObject, iTween.Hash("time", 0.25f, "scale", pointTextScale));
    }
}
