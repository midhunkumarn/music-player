using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject musicPlayerPanel;

   
    public void OpenMusicPlayer()
    {
        mainMenuPanel.SetActive(false);
        musicPlayerPanel.SetActive(true);
    }

   
    public void GoToMainMenu()
    {
        musicPlayerPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}