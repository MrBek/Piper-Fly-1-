using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderBoard : MonoBehaviour
{
    [Header("LeaderItems")]
    public LeaderItem myItem;
    public LeaderItem userItem;
    //public UnityEngine.UI.Extensions.VerticalScrollSnap snap;
    public Animator anim;
    public GameObject leaderBoardContent;
    public GameObject internetChecker;
    public GameObject lbBT;
    public GameObject loginPage, tutorialPage;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (lbBT)
            lbBT.SetActive(!PlayerMovement.isStarted);
    }


    public void LoginOrRegister(TMPro.TMP_InputField nameIF)
    {
        if(Application.internetReachability!=NetworkReachability.NotReachable)
        {
            LBController._lb_Controller._name = nameIF.text;
            ClientManager.clientManager.RegistrationProtses(nameIF.text, nameIF.text, (string token) => {
                if (string.IsNullOrEmpty(token))
                {
                    ClientManager.clientManager.LoginProtses(nameIF.text, nameIF.text, (string token1) =>
                    {
                        loginPage.SetActive(false); tutorialPage.SetActive(true);
                        LBController._lb_Controller._token = token1; PlayerPrefs.SetString("Token", LBController._lb_Controller._token); PlayerPrefs.SetString("Name", nameIF.text);
                    });
                }
                else
                {
                    loginPage.SetActive(false); tutorialPage.SetActive(true);
                    LBController._lb_Controller._token = token; PlayerPrefs.SetString("Token", LBController._lb_Controller._token); PlayerPrefs.SetString("Name", nameIF.text);
                }
            });
        }
        else
        {
            internetChecker.SetActive(true);
        }
    }
    public void getLeaderBoard()
    {
        //if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            anim.SetBool("och", true);
            ClientManager.clientManager.GetTopRankData(LBController._lb_Controller._token, ShowLeaderBoard);
        }
    }
    private GameObject[] removedObj;
    public void ShowLeaderBoard(LeaderBoardData leaders)
    {
        //Debug.Log("111");
        //snap.RemoveAllChildren(out removedObj);
        foreach (Transform game in leaderBoardContent.transform)
            Destroy(game.gameObject);
        leaders.top = leaders.top.OrderBy(o => o.score).ToArray();
        if (leaders.user_top != null)
        {
            for (int i = leaders.user_top.Length - 1; i >= 0; i--)
            {
                if (leaders.user_top[i].username == LBController._lb_Controller._name)
                {

                    myItem.setData(leaders.user_top.Length - i, leaders.user_top[i].username, leaders.user_top[i].score);
                    //snap.AddChild(Instantiate(myItem, leaderBoardContent.transform).gameObject);
                }
                else
                {
                    userItem.setData(leaders.user_top.Length - i, leaders.user_top[i].username, leaders.user_top[i].score);
                    //snap.AddChild(Instantiate(userItem, leaderBoardContent.transform).gameObject);
                }
            }
        }
        for (int i = leaders.top.Length-1; i >=0 ; i--)
        {
            if (leaders.top[i].username == LBController._lb_Controller._name)
            {

                myItem.setData(leaders.top.Length - i, leaders.top[i].username, leaders.top[i].score);
                Instantiate(myItem, leaderBoardContent.transform);
                //snap.AddChild(Instantiate(myItem, leaderBoardContent.transform).gameObject);
            }
            else
            {
                userItem.setData(leaders.top.Length - i, leaders.top[i].username, leaders.top[i].score);
                Instantiate(userItem, leaderBoardContent.transform);
                //snap.AddChild(Instantiate(userItem, leaderBoardContent.transform).gameObject);
            }
        }
        //Debug.Log(snap.ChildObjects.Length);
        //snap.GoToScreen(snap.ChildObjects.Length - 1);
    }

}
