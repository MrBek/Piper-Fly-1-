LeaderBoard/Prefabs/ClientManager birinchi sahnaga qoyiladi.

ClientManager.clientManager orqali chaqiriladi

---> Serverdagi app ma'lumotlarini olish.

public void GetAppData(string token, Action<GetAppResp> callingMethod)

---> Leader board uchun score kiritish.

public void RankPostData(string token, RankPostRequest rankPost)

---> Serverdan leaderboardni olish.

public void GetTopRankData(string token, Action<GetTopRankResp[]> callingMethod)

---> Serverdan userning ballar tarixini olish olish.

public void GetMyRankData(string token, string userName, Action<GetTopRankResp[]> callingMethod)