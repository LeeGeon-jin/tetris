using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;


public class ButtonController : MonoBehaviour
{
    //create instances of the buttons in the main menu
    public Button startBtn;
    public Button startPlainBtn;
    public Button helpBtn;
    public Button exitBtn;
    public Button okBtn;
    
    //instance of help box
    GameObject dialogBox;

    // Start is called before the first frame update
    void Start()
    {
        //reference the dialog box in the scene and disable at beginning
        dialogBox = GameObject.FindWithTag("Dialog");
        dialogBox.SetActive(false);
        //add a listener to the buttons
        startBtn.onClick.AddListener(GoToGame);
        startPlainBtn.onClick.AddListener(GoToPlainGame);
        helpBtn.onClick.AddListener(Help);
        exitBtn.onClick.AddListener(Quit);
        okBtn.onClick.AddListener(CloseDialog);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GoToGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void GoToPlainGame()
    {
        SceneManager.LoadScene("RecreatedGame");
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void Help()
    {
        //activate the help box
        dialogBox.SetActive(true);
    
    }

    void Quit()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }

    void CloseDialog()
    {
        //deactivate the help box when closing
        dialogBox.SetActive(false);
    }
}
