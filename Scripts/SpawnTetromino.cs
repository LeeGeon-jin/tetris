using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetromino : MonoBehaviour
{
    public GameObject[] Tetrominoes;
    public GameObject singleBlock;

    // Start is called before the first frame update
    void Start()
    {
        //instantiate a tetromino when start the game
        NewTetromino();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewTetromino()
    {
        //randomly instantiate tetrominoes at the top of the screen
        Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);
    }

    public void NewSingleBlock()
    {
        Instantiate(singleBlock, transform.position, Quaternion.identity);
    }
}
