using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadSceneByName(string name)
    {
        //Scene sceneToLoad = SceneManager.GetSceneByName(name);

        //if (sceneToLoad != null)
        //{
        //  SceneManager.LoadScene(sceneToLoad.buildIndex);
        //  }
        SceneManager.LoadScene(name);
    }

   public void QuitGame()
    {
        Application.Quit();
    }
}
