using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject pausePanel;

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

	public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
	}

	public void ExitLevel()
    {
        Application.LoadLevel(0);
    }

    public void Play()
    {
        Application.LoadLevel(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
