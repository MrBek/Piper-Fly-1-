using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LeaderItem : MonoBehaviour
{
    public TMP_Text idTxt;
    public TMP_Text userNameTxt;
    public TMP_Text scoreTxt;

    public int id;
    public string userName;
    public int score;

    public Image BG_Image;
    public bool isMine;
    public Color firstClr;
    public Color secondClr;
    public Color thirdClr;
    public Color otherClr;
    // Start is called before the first frame update
    void Start()
    {
        idTxt.text = id.ToString();
        userNameTxt.text = userName.ToString();
        scoreTxt.text = score.ToString();

    }

    public void setData(int id, string name, int score)
    {
        this.id = id;
        this.userName = name;
        this.score = score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
