using UnityEngine;
using UnityEngine.UI;

public class UIFpsText : MonoBehaviour
{
    private Text _fpsText;
    private float _deltaTime;
        
    private void Awake()
    {
        _fpsText = GetComponentInChildren<Text>();
#if UNITY_EDITOR
        if(_fpsText == null) Debug.LogWarning($"{gameObject.name} - No {GetType()}");
#endif

    }

    private void Update()
    {
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
            
        UpdateText($"{1.0f / _deltaTime:0.} fps");
    }

    private void UpdateText(string newText)
    {
        if(_fpsText)
            _fpsText.text = newText;
#if UNITY_EDITOR
        else
            Debug.LogWarning($"{gameObject.name} - No {GetType()}");
#endif
    }
}