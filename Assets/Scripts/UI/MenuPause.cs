using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    private static bool GameIsPaused = false;

    [SerializeField] private GameObject _pauseMenuUI;

    [SerializeField] private GameObject _darkPanelUI;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Resume()
    {
        _pauseMenuUI.SetActive(false);
        _darkPanelUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        _pauseMenuUI.SetActive(true);
        _darkPanelUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
