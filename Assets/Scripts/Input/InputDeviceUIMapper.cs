using UnityEngine;

public class InputDeviceUIMapper : MonoBehaviour
{
    [SerializeField] private GameObject _pcView;
    [SerializeField] private GameObject _xboxView;
    [SerializeField] private GameObject _psView;
    
    
    public void OnPCInputTypeDetected()
    {
        _pcView.SetActive(true);
        _xboxView.SetActive(false);
        _psView.SetActive(false);
    }

    public void OnXboxInputTypeDetected()
    {
        _pcView.SetActive(false);
        _xboxView.SetActive(true);
        _psView.SetActive(false);
    }

    public void OnPSInputTypeDetected()
    {
        _pcView.SetActive(false);
        _xboxView.SetActive(false);
        _psView.SetActive(true);
    }
}
