using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Watermelon.Upgrades;
using YG;

namespace Watermelon
{
    public class UpgradeUIPanel : MonoBehaviour
    {
        [SerializeField] Image iconImage;
        [SerializeField] Image buttonImage;
        [SerializeField] Sprite buttonActiveSprite;
        [SerializeField] Sprite buttonDisableSprite;
        [SerializeField] Image currencyImage;

        [Space]
        [SerializeField] GameObject adsButtonObject;

        [Space]
        [SerializeField] GameObject maxObject;

        [Space]
        [SerializeField] TextMeshProUGUI priceText;
        [SerializeField] TextMeshProUGUI levelText;
        [SerializeField] TextMeshProUGUI nameText;

        private Currency currency;

        private CanvasGroup canvasGroup;
        public CanvasGroup CanvasGroup => canvasGroup;

        private CanvasGroup priceCanvasGroup;

        private BaseUpgrade upgrade;
        public BaseUpgrade Upgrade => upgrade;

        private UISecretaryWindow uiSecretaryWindow;

        public void Initialise(UISecretaryWindow uiSecretaryWindow, BaseUpgrade upgrade)
        {
            this.uiSecretaryWindow = uiSecretaryWindow;
            this.upgrade = upgrade;

            // Get component
            canvasGroup = GetComponent<CanvasGroup>();
            priceCanvasGroup = priceText.GetComponent<CanvasGroup>();

            // Redraw panel
            Redraw();

            // Set name
            iconImage.sprite = upgrade.Icon;

#if UNITY_EDITOR
            nameText.text = upgrade.Title;
            return;
#endif
            
            nameText.text = GetCurrentName(upgrade.Title);
        }

        public void Redraw()
        {
            // Doctor is opened
            if (upgrade.Upgrades.Length <= upgrade.UpgradeLevel + 1)
            {
                // Disable button
                buttonImage.gameObject.SetActive(false);
                adsButtonObject.gameObject.SetActive(false);

                // Enable hired panel
                maxObject.SetActive(true);

                // Reset panel transparent
                canvasGroup.alpha = 1.0f;
            }
            else
            {
                // Enable button
                buttonImage.gameObject.SetActive(true);
                adsButtonObject.gameObject.SetActive(true);

                // Enable hired panel
                maxObject.SetActive(false);

                Currency currency = CurrenciesController.GetCurrency(upgrade.Upgrades[upgrade.UpgradeLevel + 1].CurrencyType);
                currencyImage.sprite = currency.Icon;

                int price = upgrade.Upgrades[upgrade.UpgradeLevel + 1].Price;
                if (currency.Amount >= price)
                {
                    buttonImage.sprite = buttonActiveSprite;
                    priceCanvasGroup.alpha = 1.0f;
                }
                else
                {
                    buttonImage.sprite = buttonDisableSprite;
                    priceCanvasGroup.alpha = 0.6f;
                }

                // Reset panel transparent
                canvasGroup.alpha = 1.0f;

                // Set price
                priceText.text = CurrenciesHelper.Format(price);
            }

            // Set level

#if UNITY_EDITOR
            levelText.text = $"Lvl {upgrade.UpgradeLevel + 1}";
            return;
#endif
            
            levelText.text = $"{GetCurrentLevelName(YG2.lang)} {upgrade.UpgradeLevel + 1}";
        }

        public void PurchaseButton()
        {
            if (upgrade.Upgrades.Length >= upgrade.UpgradeLevel + 1)
            {
                Currency currency = CurrenciesController.GetCurrency(upgrade.Upgrades[upgrade.UpgradeLevel + 1].CurrencyType);
                int price = upgrade.Upgrades[upgrade.UpgradeLevel + 1].Price;
                if (currency.Amount >= price)
                {
                    AudioController.PlaySound(AudioController.Sounds.buttonSound);

                    CurrenciesController.Substract(currency.CurrencyType, price);

                    upgrade.UpgradeStage();

                    uiSecretaryWindow.OnUpgraded();
                }
            }
        }

        public void PurchaseAdButton()
        {
            if (upgrade.Upgrades.Length >= upgrade.UpgradeLevel + 1)
            {
                AudioController.PlaySound(AudioController.Sounds.buttonSound);

                AdsManager.ShowRewardBasedVideo((reward) =>
                {
                    if (reward)
                    {
                        upgrade.UpgradeStage();

                        uiSecretaryWindow.OnUpgraded();
                    }
                });
            }
        }

        private const string Russian = "ru";
        private const string English = "en";
        private const string Turkish = "tr";
        
        private const string DoctorStrength = "Doctor Strength";
        private const string UpgradeTitle = "Doctor Speed";
        private const string NursesSpeed = "Nurses Speed";
        private const string PickUpSpeed = "Pick Up Speed";

        private string GetCurrentLevelName(string lang)
        {
            string currentName = "LVL";

            if (lang == Russian)
                currentName = "УР";

            return currentName;
        }
        
        private string GetCurrentName(string upgradeTitle)
        {
            switch (upgradeTitle)
            {
                case DoctorStrength:
                    
                    switch (YG2.lang)
                    {
                        case Russian:
                            upgradeTitle = "Сила доктора";
                            break;
                        case English:
                        break;
                        case Turkish:
                            upgradeTitle =  "Doktor Gücü";
                            break;
                    }
                    break;
                case UpgradeTitle:
                    
                    switch (YG2.lang)
                    {
                        case Russian:
                            upgradeTitle =  "Скорость доктора";
                            break;
                        case English:
                            break;
                        case Turkish:
                            upgradeTitle =  "Doktor Hızı";
                            break;
                    }
                    break;
                case NursesSpeed:
                    
                    switch (YG2.lang)
                    {
                        case Russian:
                            upgradeTitle =  "Скорость медбрата";
                            break;
                        case English:
                            break;
                        case Turkish:
                            upgradeTitle =  "Hemşire Hızı";
                            break;
                    }
                    break;
                case PickUpSpeed:
                    
                    switch (YG2.lang)
                    {
                        case Russian:
                            upgradeTitle =  "Взять скорость";
                            break;
                        case English:
                            break;
                        case Turkish:
                            upgradeTitle =  "Hızlanın";
                            break;
                    }
                    break;
            }

            return upgradeTitle;
        }
    }
}