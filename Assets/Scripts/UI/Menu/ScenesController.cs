using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    public void NextScens(int _sceneNumber)
    {
        SceneManager.LoadScene(_sceneNumber);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
