using UnityEngine;
using Watermelon;
using YG;

namespace Project_Data.Watermelon_Core.Core.Default_Modules.Advertisement.Scripts.Providers.Yandex
{
    public class YandexHandler : AdvertisingHandler
    {
        public YandexHandler(AdvertisingModules moduleType) : base(moduleType) { }
        private string _idAdv;

        public override void Init(AdsData adsSettings)
        {
            this.adsSettings = adsSettings;
            
            isInitialized = true;

            AdsManager.OnAdsModuleInitializedEvent?.Invoke(ModuleType);

            if (adsSettings.SystemLogs)
                Debug.Log("[AdsManager]: SDK is initialized!");
        }

        #region Banner
        public override void ShowBanner() => 
            YG2.StickyAdActivity(true);

        public override void HideBanner() => 
            YG2.StickyAdActivity(false);

        public override void DestroyBanner() => 
            YG2.StickyAdActivity(false);

        #endregion

        #region Interstitial
        public override void ShowInterstitial(InterstitialCallback callback)
        {
#if UNITY_EDITOR
            Debug.Log("Показана реклама");
            return;
#endif
            
            YG2.InterstitialAdvShow();
        }

        public override void RequestInterstitial() => 
            throw new System.NotImplementedException();

        public override bool IsInterstitialLoaded()
        {
            return true;
        }

        #endregion

        #region Rewarded Video
        public override void ShowRewardedVideo(RewardedVideoCallback callback)
        {
#if UNITY_EDITOR
            Debug.Log("Показан ревард");
            callback?.Invoke(true);
            return;
#endif
            
            YG2.RewardedAdvShow(_idAdv, () =>
            {
                callback.Invoke(true);
            });
        }

        public override void RequestRewardedVideo() => 
            Description("RequestRewardedVideo");

        public override bool IsRewardedVideoLoaded()
        {
            return true;
        }

        #endregion

        #region GDPR

        public override void SetGDPR(bool state) => 
            Description("SetGDPR");

        #endregion

        private void Description(string current) => 
            Debug.Log($"Implementation {current} in Yandex is not required");
    }
}