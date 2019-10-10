using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//this class is used to update and show the points
public class PointController : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private static int point;
    public static int Point { get { return point; } }

    // Start is called before the first frame update
    void Start()
    {
        point = 0;
        textMesh = GetComponent<TextMeshProUGUI>();
        DisplayPoint();
    }

    // Update is called once per frame
    void Update()
    {
             
    }

    public void UpdatePoint()
    {
        point = point + 100;
        DisplayPoint();
        
    }

    private void DisplayPoint()
    {
        textMesh.SetText("Your Score: "+point);
    }
}
