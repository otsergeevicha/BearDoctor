using UnityEngine;
using YG;

public class GameReadyAPI : MonoBehaviour
{
    private bool _firstTouch;
    
    private void OnTriggerEnter(Collider _)
    {
        if (!_firstTouch)
        {
            _firstTouch = true;
#if UNITY_EDITOR
            Debug.Log($"Game ready API");
#endif
            YG2.GameReadyAPI();
            
        }
    }
}
