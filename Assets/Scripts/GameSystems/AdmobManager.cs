using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace Project.GameSystems
{
    public class AdmobManager : MonoBehaviour
    {
        private string rewardedAd_ID = "ca-app-pub-5176018929650163/3281523202";//"ca-app-pub-3940256099942544/5224354917";// <- test // REAL -> "ca-app-pub-5176018929650163/3281523202";

        private RewardedAd rewardedAd;

        public static AdmobManager instance;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(this);
        }
        void Start()
        {
            // Configure TagForChildDirectedTreatment and test device IDs.
            RequestConfiguration requestConfiguration =
                new RequestConfiguration.Builder()
                .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified).build();

            MobileAds.SetRequestConfiguration(requestConfiguration);

            MobileAds.Initialize(HandleInitCompleteAction);

        }

        private void HandleInitCompleteAction(InitializationStatus initstatus)
        {
            // Callbacks from GoogleMobileAds are not guaranteed to be called on
            // main thread.
            // In this example we use MobileAdsEventExecutor to schedule these calls on
            // the next Update() loop.
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                this.rewardedAd = CreateAndLoadRewardedAd(rewardedAd_ID);
            });
        }

        public RewardedAd CreateAndLoadRewardedAd(string adUnitId)
        {
            RewardedAd rewardedAd = new RewardedAd(adUnitId);

            rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            rewardedAd.OnAdClosed += HandleRewardedAdClosed;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the rewarded ad with the request.
            rewardedAd.LoadAd(request);
            return rewardedAd;
        }

        public void ShowRewardedAd()
        {
            if (rewardedAd != null)
            {
                rewardedAd.Show();
            }
            else
            {
                Debug.Log("Rewarded ad is not ready yet.");
            }
        }
        //Events
        public void HandleRewardedAdFailedToLoad(object sender, AdError args)
        {
            MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.GetMessage());

        }
        //EVENTS AD DELEGATES FOR REWARD BASED VIDEO
        public void HandleRewardedAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdLoaded event received");
        }
        public void HandleRewardedAdClosed(object sender, EventArgs args)
        {
            print("HandleRewardBasedVideoClosed event received");
            this.rewardedAd = CreateAndLoadRewardedAd(rewardedAd_ID);
        }

        public void HandleUserEarnedReward(object sender, Reward args)
        {
            print("HandleRewardBasedVideoRewarded event received");
            if (GameManager.instance.resurrected)
                CreditManager.instance.DoubleGainedCredit();
            else
                GameManager.instance.Resurrect();
        }
    }
}