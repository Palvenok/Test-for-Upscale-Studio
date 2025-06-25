using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public void StartNewGame(int saveNum)
    {
        Debug.Log("Starting new game on save: " + saveNum);
    }

    public void LoadGame(int saveNum)
    {
        Debug.Log("Loading game on save: " + saveNum);
    }
}
