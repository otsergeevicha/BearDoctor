using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Watermelon.Store
{
    [RequireComponent(typeof(Button))]
    public class UIStoreTab : MonoBehaviour
    {
        [SerializeField] Button button;
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] Image enabledMask;
        [SerializeField] Image disabledMask;
        [SerializeField] Image backImage;

        public TabData Data { get; private set; }
        public bool IsSelected { get; private set; }

        private TabData.SimpleTabDelegate onTabSelected;

        public void Init(TabData data, TabData.SimpleTabDelegate onTabSelected)
        {
            Data = data;
            this.onTabSelected = onTabSelected;

            backImage.color = data.BackgroundColor;

#if UNITY_EDITOR
            text.text = Data.Name;
            return;
#endif

            text.text = GetCurrentName();
        }

        private const string Russian = "ru";
        private const string English = "en";
        private const string Turkish = "tr";

        private string GetCurrentName()
        {
            string currentName = Data.Name;

            switch (YG2.lang)
            {
                case Russian:
                    currentName = "Персонажи";
                    break;
                case English:
                    break;
                case Turkish:
                    currentName = "Karakterler";
                    break;
            }

            return currentName;
        }

        public void SetSelectedStatus(bool isSelected)
        {
            IsSelected = isSelected;

            enabledMask.enabled = isSelected;
            disabledMask.enabled = !isSelected;

            button.enabled = !isSelected;
        }

        public void OnButtonClick()
        {
            onTabSelected?.Invoke(Data);
        }
    }
}