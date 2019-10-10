using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //create an instance of the menubar UI and the buttons 
    GameObject menuBarImg;
    GameObject helpBox;
    GameObject gameOverBox;

    //create instances of buttons in the paused menu bar
    public Button resumeBtn;
    public Button helpBtn;
    public Button quitBtn;
    //create instances of the buttons in the game over box
    public Button restartBtn;
    public Button backBtn;

    AudioSource bgmSource; 
    //store the pressed status
    bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;

        //initialize the menubar and deactivate it when not needed
        menuBarImg = GameObject.FindWithTag("OptionMenu");
        menuBarImg.SetActive(false);

        //initialize the help dialog box and deactivate it when not needed
        helpBox = GameObject.FindWithTag("Dialog");
        helpBox.SetActive(false);

        //initialize the gameover box and deactivate it when not needed
        gameOverBox = GameObject.FindWithTag("Finish");
        gameOverBox.SetActive(false);

        //set an onclick listener to the three buttons
        resumeBtn.onClick.AddListener(ResumeGame);
        helpBtn.onClick.AddListener(Help);
        quitBtn.onClick.AddListener(BackToMain);
        //set an onclick listener to the two buttons
        restartBtn.onClick.AddListener(Restart);
        backBtn.onClick.AddListener(BackToMain);

        bgmSource = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //effective only in innovation mode (sample scene)
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "SampleScene")
        {
            if (isPaused == false)
            {
                PauseGame();
            }

            else if (isPaused == true)
            {
                ResumeGame();
            }

        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        bgmSource.Pause();
        menuBarImg.SetActive(true);
        isPaused = true;
    }

    public void EndGame()
    {
        bgmSource.Stop();
        gameOverBox.SetActive(true);
        //effective only in innovation mode (when the score is available)
        if(GameObject.FindWithTag("Score")!=null)
        { GameObject.FindWithTag("Score").GetComponent<EstimateScore>().SetScore();}
            
    }

    //following methods are when the three buttons are clicked in the menubar
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        bgmSource.Play();
        menuBarImg.SetActive(false);
        //if the user press esc to continue, don't need to deactivate the help box
        if (helpBox.activeSelf)
            helpBox.SetActive(false);
        isPaused = false;
    }

    void Help()
    {
        //activate the help box
        helpBox.SetActive(true);
    }

    public void BackToMain()
    {
        //reset the drop speed
        Time.timeScale = 1.0f;
        TetrisBlock.FallTime = 1.0f;
        SceneManager.LoadScene("Menu");
    }

    void Restart()
    {
        TetrisBlock.FallTime = 1.0f;
        //reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
