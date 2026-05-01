using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayGame()
{
    SceneManager.LoadScene("IntroScene");
}

    public void OpenLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Prototype_Level_01");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Prototype_level_02");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Prototype_Level_03");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("main menu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit clicked");
    }
}