using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    public static UIScript Instance { get; private set; }

    // Use this for initialization
    void Start()
    {
        Instance = this;
    }

    [SerializeField]
    private Text pointsTxt;

    public void GetPoint()
    {
        if (PlayerScript.FirstTimeLoginToday)
        {
            Social.ReportScore(PlayerScript.BestScore, GPGSIds.leaderboard_top_score, success => { });
            //   Social.ReportScore(GameManager.StaticChallengeScore, GPGSIds.leaderboard_challenges_top_score, success => { });
            LeaderBoardManager.Instance.IncrementCounter();
        }
    }

    public void Restart()
    {
        LeaderBoardManager.Instance.RestartGame();
    }

    public void Increment()
    {
        PlayGamesScript.IncrementAchievement(GPGSIds.leaderboard_top_score, 5);
      //  PlayGamesScript.IncrementAchievement(GPGSIds.leaderboard_challenges_top_score, 5);
    }

    public void Unlock()
    {
        PlayGamesScript.UnlockAchievement(GPGSIds.leaderboard_top_score);
     //   PlayGamesScript.UnlockAchievement(GPGSIds.leaderboard_challenges_top_score);
    }

    public void ShowAchievements()
    {
        PlayGamesScript.ShowAchievementsUI();
    }

    public void ShowLeaderboards()
    {
        if (PlayerScript.FirstTimeLoginToday)
        {
            if (Social.localUser.authenticated)
            {
                PlayGamesScript.ShowLeaderboardsUI();
            }
        }
    }

    public void UpdatePointsText()
    {
      
        pointsTxt.text = LeaderBoardManager.Counter.ToString();
    }
}