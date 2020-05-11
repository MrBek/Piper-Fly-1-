using System;

[Serializable]
public class GetAppResp
{
    public int id;
    public string name;
    public string description;
    public string company;
    public string icon;
    public string token;
    public string appstore_link;
    public string google_play_link;
    public int ar_version;
    public string created_at;
    public string updated_at;
    public int app_type;
    public int user;
}

[Serializable]
public class RankPostRequest
{
    public string Authorization;
    public string device;
    public string ip;
    public int operation_system;
    public int score;
    public DateTime datetime;
    public string session;

    public RankPostRequest(string Authorization, string device, string ip, int operation_system, int score, DateTime datetime, string session)
    {
        this.Authorization = "Token " + Authorization;
        this.device = device;
        this.ip = ip;
        this.operation_system = operation_system;
        this.score = score;
        this.datetime = datetime;
        this.session = session;
    }
}
[Serializable]
public class LeaderBoardData
{
    public GetTopRankResp[] top;
    public GetTopRankResp[] user_top;
    public int user_position;
}

[Serializable]
public class GetTopRankResp
{
    public int id;
    public Request request;
    public string username;
    public int score;
    public string datetime;
    public string session;
    public string created_at;
    public string updated_at;
    public int app;
    public int operation_system;
}

[Serializable]
public class Request
{
    public int id;
    public string device;
    public string ip;
    public string app_token;
    public int operation_system;
}


[Serializable]
public class Region
{
    public int id;
    public string name;
}
[Serializable]
public class District
{
    public int id;
    public string name;
    public int region;
}

[Serializable]
public class Locations
{
    public Region[] regions;
    public District[] districts;
}

[Serializable]
public class AuthResp
{
    public string key;
}