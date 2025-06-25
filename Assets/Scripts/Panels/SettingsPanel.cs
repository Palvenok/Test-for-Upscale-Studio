using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingsPanel : BasePanel
{
    [SerializeField] private List<GameObject> _preferencesList;
    
    private InputHandler _input;
    private int _currentPanel;
    
    public void Initialize(InputHandler input)
    {
        _input = input;
        _input.Actions.UI.SideScroll.performed += SelectPanel;
    }

    private void SelectPanel(InputAction.CallbackContext context)
    {
        _currentPanel += (int)context.ReadValue<float>();
        if(_currentPanel < 0 ) _currentPanel = 0;
        if(_currentPanel >= _preferencesList.Count) _currentPanel = _preferencesList.Count - 1;
        
        foreach(GameObject panel in _preferencesList) panel.SetActive(false);
        
        _preferencesList[_currentPanel].SetActive(true);
    }
    
    private void SelectPanel()
    {
        foreach(GameObject panel in _preferencesList) panel.SetActive(false);
        _preferencesList[_currentPanel].SetActive(true);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        _currentPanel = 0;
        SelectPanel();
    }
}
