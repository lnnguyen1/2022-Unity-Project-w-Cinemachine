using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    [SerializeField] private float _displayTime = 4.0f;
    [SerializeField] private GameObject _dialogBox;
    
    private float timerDisplay;
    
    void Start()
    {
        _dialogBox.SetActive(false);
        timerDisplay = -1.0f;
    }
    
    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                _dialogBox.SetActive(false);
            }
        }
    }
    
    public void DisplayDialog()
    {
        timerDisplay = _displayTime;
        _dialogBox.SetActive(true);
    }
}