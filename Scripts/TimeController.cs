using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
//this class is used to control the block drop time interval, drop speed and set the drop speed text
public class TimeController : MonoBehaviour
{
    float timer;
    int speed=1;

    private TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GameObject.FindWithTag("Speed").GetComponent<TextMeshProUGUI>();
        SetText();
    }

    // Update is called once per frame
    void Update()
    {
        //update timer and fallTime
        timer += Time.deltaTime;
        if (timer > 60f)
        {
            UpdateSpeed();
          

            timer = 0;
        }
        //reset the speed when r key is pressed and in innovation mode
        if (Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().name=="SampleScene")
        {
            ResetSpeed();
        }
            
    }

    void UpdateSpeed()
    {
        if (TetrisBlock.FallTime > 0)
        {
            //decrease the time interval less to make the drop faster
            TetrisBlock.FallTime = TetrisBlock.FallTime * 0.8f;
            speed = speed + 1;
        }

        SetText();
    }

    void ResetSpeed()
    {
        TetrisBlock.FallTime = 1.0f;
        speed = 1;
        timer = 0;

        SetText();
    }

    void SetText()
    {
        textMesh.SetText("Current speed: "+speed);
    }
}
