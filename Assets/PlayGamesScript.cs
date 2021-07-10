using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayGamesScript : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
        /*
          if (PlayerScript.FirstTimeLoginToday == false)
          {
              PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
              PlayGamesPlatform.InitializeInstance(config);
              PlayGamesPlatform.Activate();
              if (!Social.localUser.authenticated)
              {
                  SignIn();

              }
          }
         */

    }
    public void FirstLogin()
    {
        if (PlayerScript.FirstTimeLoginToday == false)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.Activate();
            if (!Social.localUser.authenticated)
            {
                PlayerScript.FirstTimeLoginToday = true;
                SignIn();

            }
        }

    }
    void SignIn()
    {
        Social.localUser.Authenticate(success => { });
    }

    #region Achievements
    public static void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100, success => { });
    }

    public static void IncrementAchievement(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    }

    public static void ShowAchievementsUI()
    {
        Social.ShowAchievementsUI();
    }
    #endregion /Achievements

    #region Leaderboards
    public static void AddScoreToLeaderboard(string leaderboardId, long score)
    {
        Social.ReportScore(score, leaderboardId, success => { });
    }

    public static void ShowLeaderboardsUI()
    {
         Social.ShowLeaderboardUI();
    }
    #endregion /Leaderboards

}