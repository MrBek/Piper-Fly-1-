using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ClientManager : MonoBehaviour
{
    public static ClientManager clientManager;
    public string token = "QjL5Z7Tt8YKgMn9zHEPm2sOMnRt7RDrA";
    private void Awake()
    {
        if (clientManager)
            Destroy(gameObject);
        clientManager = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }




    #region Public Methods
    /// <summary>
    /// Serverdagi app ma'lumotlarini olish.
    /// </summary>
    /// <param name="callingMethod">Ma`lumot olingach chaqirilishi kerak bo`lgan funktsiya.</param>
    public void GetAppData(Action<GetAppResp> callingMethod)
    {
        StartCoroutine(GetApp(callingMethod));
    }

    /// <summary>
    /// Leader board uchun score kiritish.
    /// </summary>
    /// <param name="rankPost">Post ma`lumotlari.</param>
    public void RankPostData(RankPostRequest rankPost)
    {
        StartCoroutine(RankPost(rankPost));
    }

    /// <summary>
    /// Serverdan leaderboardni olish.
    /// </summary>
    /// <param name="callingMethod">Ma`lumot olingach chaqirilishi kerak bo`lgan funktsiya.</param>
    public void GetTopRankData(string Authorization,Action<LeaderBoardData> callingMethod)
    {
        StartCoroutine(GetTopRank(Authorization,callingMethod));
    }
    /// <summary>
    /// Serverdan userning ballar tarixini olish olish.
    /// </summary>
    /// <param name="userName">Bizga kerak bo`lgan user name.</param>
    /// <param name="callingMethod">Ma`lumot olingach chaqirilishi kerak bo`lgan funktsiya.</param>
    public void GetMyRankData(string userName, Action<LeaderBoardData> callingMethod)
    {
        StartCoroutine(GetMyRank(userName, callingMethod));
    }


    public void GetUserCacheData(string Authorization, Action<string> callingMethod)
    {
        StartCoroutine(GetUserCache(Authorization,callingMethod));
    }

    public void SetUserPostData(string Authorization, string data)
    {
        StartCoroutine(SetUserPost(Authorization, data));
    }
    public void GetLoacationData(Action<Locations> callingMethod)
    {
        StartCoroutine(getLocation(callingMethod));
    }

    public void RegistrationProtses(string username, string password, Action<string> callingMethod)
    {
        StartCoroutine(Registration(username,password,callingMethod));
    }

    public void LoginProtses(string username, string password, Action<string> callingMethod)
    {
        StartCoroutine(Login(username,password,callingMethod));
    }
    #endregion

    #region Private Methods
    private IEnumerator GetApp(Action<GetAppResp> callingMethod)
    {
        var request = UnityWebRequest.Get("https://controller.uz/api/v1/get_app");
        request.SetRequestHeader("token", this.token);

        yield return request.SendWebRequest();

        callingMethod(JsonUtility.FromJson<GetAppResp>(request.downloadHandler.text));
    }

    private IEnumerator RankPost(RankPostRequest rankPost)
    {
        using (UnityWebRequest request = UnityWebRequest.Post("https://controller.uz/api/v1/rank", ""))
        {
            request.SetRequestHeader("Authorization", rankPost.Authorization);
            request.SetRequestHeader("token", this.token);
            request.SetRequestHeader("device", rankPost.device);
            request.SetRequestHeader("ip", rankPost.ip);
            request.SetRequestHeader("operation-system", rankPost.operation_system.ToString());
            request.SetRequestHeader("score", rankPost.score.ToString());
            request.SetRequestHeader("datetime", rankPost.datetime.ToString("yyyy-MM-dd HH:mm:ss"));
            request.SetRequestHeader("session", rankPost.session);


            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                Debug.Log(request.downloadHandler.text);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
    private IEnumerator GetTopRank(string Authorization,Action<LeaderBoardData> callingMethod)
    {
        var request = UnityWebRequest.Get("https://controller.uz/api/v1/rank/top");
        request.SetRequestHeader("token", this.token);
        request.SetRequestHeader("Authorization", "Token " + Authorization);

        yield return request.SendWebRequest();

        callingMethod(JsonUtility.FromJson<LeaderBoardData>(request.downloadHandler.text));
    }
    private IEnumerator GetMyRank(string Authorization, Action<LeaderBoardData> callingMethod)
    {
        var request = UnityWebRequest.Get("https://controller.uz/api/v1/rank/my");
        request.SetRequestHeader("token", this.token);
        request.SetRequestHeader("Authorization","Token "+ Authorization);
        yield return request.SendWebRequest();

        callingMethod(JsonUtility.FromJson<LeaderBoardData>(request.downloadHandler.text));
    }

    private IEnumerator GetUserCache(string Authorization, Action<string> callingMethod)
    {
        var request = UnityWebRequest.Get("https://controller.uz/api/v1/user/data");
        request.SetRequestHeader("Authorization", "Token " + Authorization);

        yield return request.SendWebRequest();

        callingMethod(request.downloadHandler.text);
    }

    private IEnumerator SetUserPost(string Authorization,string data)
    {
        using (UnityWebRequest request = UnityWebRequest.Post("https://controller.uz/api/v1/user/data", ""))
        {
            request.SetRequestHeader("Authorization", Authorization);
            request.SetRequestHeader("data", data);

            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    private IEnumerator getLocation(Action<Locations> callingMethod)
    {
        var request = UnityWebRequest.Get("https://controller.uz/api/v1/locations");

        yield return request.SendWebRequest();

        callingMethod(JsonUtility.FromJson<Locations>(request.downloadHandler.text));
    }

    private IEnumerator Registration(string username, string password, Action<string> callingMethod)
    {
        WWWForm data = new WWWForm();
        data.AddField("username", username);
        data.AddField("password", password);

        using (UnityWebRequest request = UnityWebRequest.Post("https://controller.uz/rest-auth/registration/", data))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                if (request.downloadHandler.text.Contains("username"))
                {
                    callingMethod(string.Empty);
                }
                else
                    callingMethod(JsonUtility.FromJson<AuthResp>(request.downloadHandler.text).key);
            }
        }
    }
    private IEnumerator Login(string username, string password, Action<string> callingMethod)
    {
        WWWForm data = new WWWForm();
        data.AddField("username", username);
        data.AddField("password", password);

        using (UnityWebRequest request = UnityWebRequest.Post("https://controller.uz/rest-auth/login/", data))
        {

            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                AuthResp resp = JsonUtility.FromJson<AuthResp>(request.downloadHandler.text);
                if (resp != null)
                    callingMethod(JsonUtility.FromJson<AuthResp>(request.downloadHandler.text).key);
                else
                    callingMethod(string.Empty);
            }
        }
    }

    #endregion
}