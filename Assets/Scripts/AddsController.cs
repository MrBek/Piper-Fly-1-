using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
public class AddsController : MonoBehaviour
{
    public VideoPlayer video;
    public Image slider;
    public TMPro.TMP_Text counterTxt;
    public GameObject showExit;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void startVideo()
    {
        if (LevelGenerator.levelGenerator.musicOn)
            AudioManager.audioManager.StopMusic();
        video.Stop();
        video.Play();
        StartCoroutine(showXButton());
    }
    public IEnumerator showXButton()
    {
        showExit.SetActive(false);
        yield return new WaitForSeconds(20.12f);
        if (LevelGenerator.levelGenerator.musicOn)
            AudioManager.audioManager.PlayMusic("gameplay");
        showExit.SetActive(true);
    }
    public void exitApp()
    {
        LevelGenerator.levelGenerator.RewardPanel.SetActive(true);
        LevelGenerator.levelGenerator.RewardText.text = "+" + Point.pointCount + "<sprite=0>";
        showExit.SetActive(false);
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (!(video.clip.length <= video.time))
        {
            slider.fillAmount = (float)(1.0f - video.time / video.clip.length);
            counterTxt.text = ((int)(video.clip.length - video.time)).ToString();
        }
    }
}
