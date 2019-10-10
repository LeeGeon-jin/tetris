using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//this class is used for the score text in the gameover box
public class EstimateScore : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScore()
    {
        text.text = "Your score: " + PointController.Point.ToString();
    }
}
