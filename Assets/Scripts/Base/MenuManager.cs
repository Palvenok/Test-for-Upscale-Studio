using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private PromptPanel _promptPanel;
    [SerializeField] private MenuPanel _menuPanel;
    [SerializeField] private StartGamePanel _startGamePanel;
    [SerializeField] private SettingsPanel _settingsPanel;
    [SerializeField] private CreditsPanel _creditsPanel;
    
    private InputHandler _input;
    private Dictionary<PanelType, BasePanel> _panels;
    private PanelType _currentPanel;
    private PanelType _lastPanel;
    private List<InputDeviceUIMapper> _inputDeviceUIMappers;
    
    public void OnSetMenuPanel() => SwitchToPanel(PanelType.Menu);
    public void OnStartGamePanel() => SwitchToPanel(PanelType.StartGame);
    public void OnSettingsPanel() => SwitchToPanel(PanelType.Settings);
    public void OnCreditsPanel() => SwitchToPanel(PanelType.Credits);
    public void ReturnToLastPanel() => SwitchToPanel(_lastPanel);
    
    private void OnCancelActionPerformed(InputAction.CallbackContext context) => SwitchToPanel(_lastPanel);

    public void Initialize(InputHandler inputHandler)
    {
        _input = inputHandler;
        
        _settingsPanel.Initialize(_input);
        
        _inputDeviceUIMappers = new List<InputDeviceUIMapper>();
        
        foreach (var root in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            var components = root.GetComponentsInChildren<InputDeviceUIMapper>(true); // includeInactive = true
            foreach (var comp in components)
                _inputDeviceUIMappers.Add(comp);
        }
        
        _panels = new Dictionary<PanelType, BasePanel>
        {
            { PanelType.Prompt, _promptPanel},
            { PanelType.Menu, _menuPanel },
            { PanelType.StartGame, _startGamePanel },
            { PanelType.Settings, _settingsPanel },
            { PanelType.Credits, _creditsPanel }
        };
        
        SwitchToPanel(PanelType.Prompt);
        _input.Actions.UI.Cancel.performed += OnCancelActionPerformed;

        foreach (var inputDeviceUIMapper in _inputDeviceUIMappers)
        {
            _input.OnKeyboardMouseDetected += inputDeviceUIMapper.OnPCInputTypeDetected;
            _input.OnPlayStationGamepadDetected += inputDeviceUIMapper.OnPSInputTypeDetected;
            _input.OnXboxGamepadDetected += inputDeviceUIMapper.OnXboxInputTypeDetected;
        }
    }

    private void SwitchToPanel(PanelType panelType)
    {
        _lastPanel = _currentPanel;
        _currentPanel = panelType;
        
        foreach (var panel in _panels.Values)
            panel.gameObject.SetActive(false);

        if (_currentPanel == PanelType.Menu) _lastPanel = PanelType.Menu;
        
        _panels[panelType].gameObject.SetActive(true);
    }
    
    public enum PanelType
    {
        Prompt, 
        Menu,
        StartGame,
        Settings,
        Credits,
    }
}
