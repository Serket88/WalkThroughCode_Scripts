using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Button quit;
    public Button resume;
    public Button restart;
    public Button main;

    public GameManager gMan;

    // Use this for initialization
    void Start()
    {
        resume.onClick.AddListener(ResumeButtonClicked);
        restart.onClick.AddListener(RestartButtonClicked);
        main.onClick.AddListener(MainButtonClicked);
        quit.onClick.AddListener(QuitButtonClicked);
    }

    void ResumeButtonClicked()
    {
        Debug.Log("Resume button clicked...");
        gMan.pauseGame();
        
    }

    void RestartButtonClicked()
    {
        Debug.Log("Restart button clicked...");
        gMan.sceneLoad(SceneManager.GetActiveScene().buildIndex);
    }

    void MainButtonClicked()
    {
        Debug.Log("Main button clicked...");
        SceneManager.LoadScene(0);
    }

    void QuitButtonClicked()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
