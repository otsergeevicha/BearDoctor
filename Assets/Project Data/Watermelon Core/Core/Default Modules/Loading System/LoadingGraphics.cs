using UnityEngine;
using UnityEngine.UI;

namespace Watermelon
{
    public class LoadingGraphics : MonoBehaviour
    {
        [SerializeField] Image backgroundImage;

        private void Awake() => 
            DontDestroyOnLoad(gameObject);

        private void OnEnable() => 
            GameLoading.OnLoadingFinished += OnLoadingFinished;

        private void OnDisable() => 
            GameLoading.OnLoadingFinished -= OnLoadingFinished;


        private void OnLoadingFinished()
        {
            backgroundImage.DOFade(0.0f, 0.6f, unscaledTime: true).OnComplete(delegate
            {
                Destroy(gameObject);
            });
        }
    }
}
