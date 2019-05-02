using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button play;
    public Button quit;
    public Button back;

    public GameObject mainMenu;
    public GameObject optionsMenu;

	// Use this for initialization
	void Start ()
    {
 //       PlayerPrefs.DeleteAll();
        play.onClick.AddListener(PlayButtonClicked);
        quit.onClick.AddListener(QuitButtonClicked);
        back.onClick.AddListener(BackButtonClicked);
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
	}
	
	void PlayButtonClicked()
    {
        //        SceneManager.LoadScene(0);
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
	}

    void QuitButtonClicked()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    void LoadButtonClicked()
    {
        Debug.Log("LOAD BUTTON CLICKED");
    }

    void BackButtonClicked()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
