using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckShapes : MonoBehaviour
{
    public GameObject correct;//the UI to display that the shape is correct
    public GameObject wrong;//the UI to display that the shape is wrong

    //the drawings to disable once is enabled by displayDrawing()
    public GameObject circle;
    public GameObject triangle;
    public GameObject square;

    public static CheckShapes instance = null;
    private RandomDrawing randomDrawing;
    private Score score;

    private bool circleIsShown = false;//to check if player showed that the circle drawing is shown
    private bool triangleIsShown = false;//to check if player showed that the triangle drawing is shown
    private bool squareIsShown = false;//to check if player showed that the square drawing is shown
    private bool circleInDrawing = false;//set to true if there is a circle drawing randomly displayed in the UI
    private bool triangleInDrawing = false;//set to true if there is a triangle drawing randomly displayed in the UI
    private bool squareInDrawing = false;//set to true if there is a square drawing randomly displayed in the UI

    void Start()
    {
        randomDrawing = GetComponent<RandomDrawing>();
        score = GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void checkShapes()//this will check the drawing displayed on the bottom left hand corner with the player's card that 
                            //they will be showing through AR
    {

        if (circle.activeInHierarchy == true)//is the circle game object set to true
        {
            circleInDrawing = true;//this is set to true, as there is a circle displayed in the UI
            if (circleIsShown == true)//if player displayed the same shape
            {
                circle.SetActive(false);//the object is set to false, so that the next drawing can be displayed
                circleIsShown = false;//the boolean is set to false
                correctShape();//the correctShape function is executed
            }
            else if (triangleIsShown == true)//if player displays a different shape
            {
                circle.SetActive(false);//the object is set to false, so that the next drawing can be displayed
                triangleIsShown = false;//the boolean is set to false
                wrongShape();//the wrongShape function is executed
            }
            else if (squareIsShown == true)//same as above
            {
                circle.SetActive(false);
                squareIsShown = false;
                wrongShape();
            }
        }
        if (triangle.activeInHierarchy == true)//explanation for triangle and square is the same as above
        {
            triangleInDrawing = true;
            if (triangleIsShown == true)
            {
                triangle.SetActive(false);
                triangleIsShown = false;
                correctShape();
            }
            else if (circleIsShown == true)
            {
                triangle.SetActive(false);
                circleIsShown = false;
                wrongShape();
            }
            else if (squareIsShown == true)
            {
                triangle.SetActive(false);
                squareIsShown = false;
                wrongShape();
            }
        }
        if (square.activeInHierarchy == true)
        {
            squareInDrawing = true;
            if (squareIsShown == true)
            {
                square.SetActive(false);
                squareIsShown = false;
                correctShape();
            }
            else if (circleIsShown == true)
            {
                
                square.SetActive(false);
                circleIsShown = false;
                wrongShape();
            }
            else if (triangleIsShown == true)
            {

                square.SetActive(false);
                triangleIsShown = false;
                wrongShape();
            }
        }
    }

    IEnumerator Wait()//this coroutine will wait for animation for the wrong/correct text to finish
    {
        yield return new WaitForSeconds(1);
        correct.SetActive(false);
        wrong.SetActive(false);
    }

    public void wrongShape()//function whenever player get the shape wrong
    {
        wrong.SetActive(true);//the wrong text is enabled
        randomDrawing.displayDrawing();//the next drawing will be displayed
        score.DeductScore();//the score will be deducted
        StartCoroutine(Wait());//the coroutine wait will be executed
    }

    public void correctShape()//function whenever player get the shape correct
    {
        correct.SetActive(true);//the correct text is enabled
        randomDrawing.displayDrawing();//the next drawing will be displayed
        score.AddScore();//the score will be added
        StartCoroutine(Wait());//the coroutine wait will be executed
    }

    public void circleShown()//function to show that the player shown the Circle Card
    {
        circleIsShown = true;
    }

    public void triangleShown()//function to show that the player shown the Triangle Card
    {
        triangleIsShown = true;
    }

    public void squareShown()//function to show that the player shown the Square Card
    {
        squareIsShown = true;
    }
}
