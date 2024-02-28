using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance;
    
    [SerializeField] private Image _mask;
    
    private float originalSize;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalSize = _mask.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {				      
        _mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
