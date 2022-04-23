using UnityEngine;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using System;

namespace Project.GameSystems
{
    public class GooglePlayServicesManager : MonoBehaviour
    {
        private static GooglePlayServicesManager instance;
        public static GooglePlayServicesManager Instance => instance;

        public bool isConnectedToGooglePlayServices;

        private void Awake()
        {
            #region singleton
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(this);
            #endregion /singleton

            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

            PlayGamesPlatform.InitializeInstance(config);

            //Activate play games
            //PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();

        }
        private void Start()
        {
            SignInToGooglePlayServices();
        }

        private bool signin, achivement, leaderboard;
        private void SignInToGooglePlayServices()
        {
            PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
            {
                switch (result)
                {
                    case SignInStatus.Success:
                        isConnectedToGooglePlayServices = true;
                        signin = true;
                        break;
                    default:
                        isConnectedToGooglePlayServices = false;
                        break;
                }
            });
        }

        #region Achivements
        public static void UnlockAchivement(string id)
        {
            Social.ReportProgress(id, 100, sucecss => { });
        }

        public static void IncrementAchivement(string id, int stepsToIncrement)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, sucecss => { });
        }

        public void ShowAchivementsUI()
        {
            if (Instance.isConnectedToGooglePlayServices)
            {
                achivement = true;
                Debug.Log("Achivements");
                ((PlayGamesPlatform)Social.Active).ShowAchievementsUI();
                //Social.ShowAchievementsUI();
            }
        }
        #endregion /Achivements

        #region LeaderBoards
        public static void AddScoreToLeaderBoard(string leaderboardId, long score)
        {
            if (Social.localUser.authenticated)
            {
                Social.ReportScore(score, leaderboardId, success => { });
            }
        }
        public void ShowLeaderboardUI()
        {
            if (Instance.isConnectedToGooglePlayServices)
            {
                leaderboard = true;
                Debug.Log("Leaderboard");
                ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGSIds.leaderboard_leaderboard);
                //Social.ShowLeaderboardUI();
            }
        }
        #endregion /LeaderBoards
        private void OnGUI()
        {
            if (signin)
            {
                GUI.TextArea(new Rect(240, 700, 250, 60), "Connected");
                signin = false;
            }
            if (achivement)
            {
                GUI.TextArea(new Rect(240, 600, 250, 60), "Achivements Button Pressed");
                achivement = false;
            }
            if (leaderboard)
            {
                GUI.TextArea(new Rect(240, 500, 250, 60), "Leaderboards Button Pressed");
                leaderboard = false;
            }
        }
    }
}
