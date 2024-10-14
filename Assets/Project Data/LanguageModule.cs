using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Project_Data
{
    public class LanguageModule : MonoBehaviour
    {
        [SerializeField] private bool _isPicture;

        [SerializeField] private TMP_Text _currentText;
        [SerializeField] private Image _currentImage;

        [SerializeField] private string _ruText;
        [SerializeField] private string _enText;
        [SerializeField] private string _trText;

        [SerializeField] private Sprite _ruSprite;
        [SerializeField] private Sprite _enSprite;
        [SerializeField] private Sprite _trSprite;

        private const string Russian = "ru";
        private const string English = "en";
        private const string Turkish = "tr";

        private void Start()
        {
#if UNITY_EDITOR
            return;
#endif

            switch (YG2.lang)
            {
                case Russian:
                    if (_isPicture)
                        _currentImage.sprite = _ruSprite;
                    else
                        _currentText.text = _ruText;
                    break;
                case English:
                    if (_isPicture)
                        _currentImage.sprite = _enSprite;
                    else
                        _currentText.text = _enText;
                    break;
                case Turkish:
                    if (_isPicture)
                        _currentImage.sprite = _trSprite;
                    else
                        _currentText.text = _trText;
                    break;
                default:
                    if (_isPicture)
                        _currentImage.sprite = _ruSprite;
                    else
                        _currentText.text = _ruText;
                    break;
            }
        }
    }
}