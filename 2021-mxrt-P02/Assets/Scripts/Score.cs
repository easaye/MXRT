using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text score;//to display the score during the game
    public TMP_Text pausedScore;//to display the score during the paused scene
    public TMP_Text endScore;//to display the score during the end of the game

    public static Score instance = null;//to allow the script to share the functions and variables to other scripts

    private int correctMultiplier = 100;//this adds when the players get the correct shapes
    private int wrongDeduct = 50;//this minus with how much players get the wrong shapes 
    private int scoreWCorrect;//the score which will be added in the final score
    private int scoreWWrong;//the score which will be added in the final score
    private int finalScore;//final that will be displayed in the UI

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        score.text = "0000";//score is set to 0000
        pausedScore.text = "Score: ";//score in the paused level is set to Score: 
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "" + finalScore;//is updated everytime the player get the shape correct/wrong 
        endScore.text = "" + finalScore;//is updated as well for thescore to show at the end

        if(gameManager.isPaused == true)//if the game isPaused in game manager is true;
        {
            pausedScore.text = "Score: " + finalScore;//is updated when the player has paused the game
        }
    }

    public void AddScore()//function to add score
    {
        scoreWCorrect = 1 * correctMultiplier;//will multiply once and added to the scoreWCorrect variable
        finalScore += scoreWCorrect;//which will be added to the finalScore
    }

    public void DeductScore()//function to minus score
    {
        if(finalScore > 0)//if score is more than 0
        {
            scoreWWrong = 1 * wrongDeduct;//will multiply once and added to the scoreWWrong variable
            finalScore -= scoreWWrong;//which will be deducted to the finalScore
        }
        else//if finalScore is 0
        {
            finalScore = 0;//finalScore will be set to 0
        }
        
    }
}
