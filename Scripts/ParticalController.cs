using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalController : MonoBehaviour
{
    GameObject partical;
    //create a changable variable to store the full line number value for showing partical
    public static int row;

    // Start is called before the first frame update
    void Start()
    {
        //reference and deactivate it when not needed
        partical = GameObject.FindWithTag("Partical");
        partical.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void DisplayPartical()
    {
        //set the partical object's y position with the row value, then activate it, delay for two sec, deactivate
        partical.transform.position = new Vector3(0, row, -1);
        partical.SetActive(true);
        StartCoroutine(ParticlesCoroutine());
    }

    IEnumerator ParticlesCoroutine()
    {
        yield return new WaitForSeconds(2f);
        partical.SetActive(false);
    }
}
