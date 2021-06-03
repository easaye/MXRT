using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDrawing : MonoBehaviour
{
    public GameObject[] shapes;//to get the array of drawings of arrays

    public static RandomDrawing instance = null;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayDrawing()//function to display drawing
    {
        shapes[Random.Range(0, shapes.Length)].SetActive(true);//this will randomly set active the drawings to display in the UI
    }
}
