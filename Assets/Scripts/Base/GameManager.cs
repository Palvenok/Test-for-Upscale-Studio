using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private MenuManager _menuManager;

    private void Awake()
    {
        _inputHandler.Initialize();
        _menuManager.Initialize(_inputHandler);
    }
}
