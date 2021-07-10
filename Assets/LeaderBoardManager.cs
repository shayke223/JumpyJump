using UnityEngine;

public class LeaderBoardManager : MonoBehaviour
{

    public static LeaderBoardManager Instance { get; private set; }
    public static int Counter { get; private set; }

    // Use this for initialization
    void Start()
    {
        Instance = this;
    }

    public void IncrementCounter()
    {
        RestartGame();
        Counter = PlayerScript.BestScore;

    //    CounterChallenges = GameManager.StaticChallengeScore;
        PlayGamesScript.AddScoreToLeaderboard(GPGSIds.leaderboard_top_score, PlayerScript.BestScore);
      //  PlayGamesScript.AddScoreToLeaderboard(GPGSIds.leaderboard_challenges_top_score, GameManager.StaticChallengeScore);
        UIScript.Instance.UpdatePointsText();
    }

    public void RestartGame()
    {
        PlayGamesScript.AddScoreToLeaderboard(GPGSIds.leaderboard_top_score, Counter);
       // PlayGamesScript.AddScoreToLeaderboard(GPGSIds.leaderboard_challenges_top_score, CounterChallenges);
        Counter = 0;
       // CounterChallenges = 0;
        UIScript.Instance.UpdatePointsText();
    }

}