using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //I used this website https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/ to reference
    //the timer codes, as it is a pretty useful website to understand and a straightforward method to create a efficient timer

    public TMP_Text timerText;//text to track the timer during the game
    public TMP_Text countdownText;//text to track the countdown before the game
    public TMP_Text pausedText;//text to show that the game is paused
    public GameObject countdownPanel;//to enable/disable the countdown timer
    public GameObject timesUp;//to enable/disable once timer is 0
    public GameObject finalScore;//to enable once timer is 0, and show final score
    public bool isPaused = false;//to set true/false when player click on pause
    public bool endGame = false;//to set true/false when timer is 0

    private float timer = 61.0f;//duration of the game
    private float countdownTimer = 4.0f;//countdown before the game
    private float endTimer = 1.5f;//this is to transition the times'up animation to the final score text
    private bool timerRunning  = false;//to stop the timer
    private bool countDown = false;//set to true/false when countdown timer is running
    private bool gameStart = false;//set to true/false when game has started or end
    private float minutes = 0.0f;
    private float seconds = 0.0f;

    public static GameManager instance = null;

    //using functions from these two scripts
    private CheckShapes checkShapes;
    private RandomDrawing randomDrawing;
    // Start is called before the first frame update
    void Start()
    {
        countDown = true;//countdown timer will start
        timerText.enabled = false;//will be disabled before countdown 
        checkShapes = GetComponent<CheckShapes>();
        randomDrawing = GetComponent<RandomDrawing>();
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown)//if countdown is true
        {
            timerRunning = true;//timerRunning is set to true
            
            if (timerRunning)//if timerRunning is true
            {   
                DisplayTime(countdownTimer, countdownText);//this will display the timer in the UI in minutes and seconds
                if (countdownTimer > 0)//if timer is more than 0
                {
                    Debug.Log(timer);
                    countdownTimer -= Time.deltaTime;//timer will be minus by the time in seconds by last the frame
                }
                else
                {
                    countdownTimer = 0;//once it reached 0, timer set to 0
                    timerRunning = false;//set the timerRunning to false
                    countDown = false;//countdown has finished hence, it is set to false
                    gameStart = true;//the game has started hence, it is set to true
                    randomDrawing.displayDrawing();//this will randomly spawn drawings in the notepad
                    countdownPanel.SetActive(false);//will set the countdown panel to false to it is
                }
            }
        }

        if (gameStart)//if game has started
        {
            timerRunning = true;//timerRunning is set to true
            timerText.enabled = true;//the game timer is set to true, as the game has started and players can see the timer
            
            if (timerRunning)//if timerRunning is true
            {
                if (!isPaused)//if game is not paused
                {
                    checkShapes.checkShapes();//game will check the shape that the player is showing through AR
                    DisplayTime(timer, timerText);//this will display the timer in the UI in minutes and seconds
                    if (timer > 0)//if timer is more than 0
                    {
                        Debug.Log(timer);
                        timer -= Time.deltaTime;//timer will be minus by the time in seconds by last the frame
                    }
                    else
                    {
                        timer = 0;//once it reached 0, timer set to 0
                        timerRunning = false;//set the timerRunning to false
                        gameStart = false;//game has ended hence, it is set to false;
                        endGame = true;//game has eneded hence, endGame is set to true;
                        timerText.text = "00:00";//the ingame text is set to 00:00 as it would display negative numbers
                                                 //since there are no negative numbers in time they are set to 00:00
                        timesUp.SetActive(true);//to display the time's up animation
                    }
                }
                else
                {
                    PausedDisplayTime(timer, pausedText);//this will display the timer in the UI in minutes and seconds
                }
            }
        }

        if (timerText.text == "00:03")//if the timer reaches 00:03, the text color will change to red
        {
            timerText.color = Color.red;
        }


        if (endGame)//if game ends
        {
            timerRunning = true;//timerRunning is set to true

            if (timerRunning)//if timerRunning is true
            {
                if (endTimer > 0)//if timer is more than 0
                {
                    Debug.Log(timer);
                    endTimer -= Time.deltaTime;//timer will be minus by the time in seconds by last the frame

                }
                else
                {
                    endTimer = 0;//once it reached 0, timer set to 0
                    timerRunning = false;//set the timerRunning to false
                    endGame = false;//end game is transitioning to the final score
                    timesUp.SetActive(false);//the times up animation is set to false
                    finalScore.SetActive(true);//final score is set to true since it is being transitioned
                }
            }
        }
    }

    void DisplayTime(float timeToDisplay, TMP_Text text)//function which needs a time that is going to be displayed, the text to display the time
    {

        minutes = Mathf.FloorToInt(timeToDisplay / 60);//we take the minutes by dividing it by 60
        seconds = Mathf.FloorToInt(timeToDisplay % 60);//then we take the seconds by using the modulo operation which takes the remainder
                                                       //FloorToInt which returns the largest integer smaller or equal to the number

        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);//using string.Format will put the numbers minutes and second with the zeros
                                                                     //as placeholders if there are no numbers 
    }

    void PausedDisplayTime(float pausedTimeToDisplay, TMP_Text text)//same as above but the difference is to display the time when its Paused
    {
        minutes = Mathf.FloorToInt(pausedTimeToDisplay / 60);
        seconds = Mathf.FloorToInt(pausedTimeToDisplay % 60);

        text.text = string.Format("Timer: {0:00}:{1:00}", minutes, seconds);
    }

    public void Paused()//function for onClick in the UI to pause the game
    {
        isPaused = true;//set the bool to true
    }

    public void Play()//function for onClick in the UI to play the game
    {
        isPaused = false;//set the bool to false
    }

    public void Restart()//to restart the scene
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//the scene manager will load the scene that the player is playing in again
    }

    public void MainMenu()//returns the player to the Main Menu
    {
        SceneManager.LoadScene("MainMenu");//the scene manager will load the scene called "MainMenu"
    }
    public void Quit()//to quit the game
    {
        Debug.Log("Quit");//if the player has successfully exited from the game
        Application.Quit();//quitting from the game application
    }
}
