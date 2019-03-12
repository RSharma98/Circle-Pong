using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	private bool isGameRunning;	//Is the game running or should the main menu be shown
	Coroutine textCoroutine;	//A variable to store the coroutine

	public Text titleText;		//The text box to show the title
	public Text startText;		//The text box to show the player how to start a game
	public Text scoreText;		//The text box to show the current score
	public Text highScoreText;	//The textbox to show the highscore
	private int scoreTextSize;	//The starting font size of the score text

	private int score, highScore;	//Variables to store the score and highscore
	GameObject ball;	//The ball

	void Start () {
		isGameRunning = false;	//On start the game is not running (i.e. the main menu should be shown)
		highScore = GetHighScore();	//Assign the highscore
		scoreTextSize = scoreText.fontSize;	//Store the font size of the score text
		ball = GameObject.FindGameObjectWithTag("Ball");	//Find the ball
	}
	
	void Update () {
		if(isGameRunning){	//If the game is running
			//Stop the text coroutine if it is not null
			if(textCoroutine != null) {
				StopCoroutine(textCoroutine);
				textCoroutine = null;
			}
			//Disable the title and start text
			titleText.enabled = false;
			startText.enabled = false;
			//Enable the score and high score text
			scoreText.enabled = true;
			highScoreText.enabled = true;
			scoreText.text = score.ToString();					//Set the score text to show the score
			highScoreText.text = "HIGH SCORE: " + highScore;	//Set the high score text to show the high score

			//Get the ball's position to see if it is still on screen
			Vector2 ballScreenPoint = Camera.main.WorldToViewportPoint(ball.transform.position);
			bool ballOffScreen = ballScreenPoint.x < 0.1f || ballScreenPoint.x > 0.9f || ballScreenPoint.y < 0.1f || ballScreenPoint.y > 0.9f;
			if(ballOffScreen) {
				//If the ball is not on screen, end the game
				EndGame();
			}
		} else{		//If the game is not running (i.e. the main menu should be shown)
			if(textCoroutine == null) textCoroutine = StartCoroutine(TextEffect(startText, 0.5f));	//If the text effect coroutine has not started, start it
			titleText.enabled = true;		//Enable the title text
			scoreText.enabled = false;		//Disable the score text
			highScoreText.enabled = false;	//Disable the highscore text

			if(Input.GetKeyDown(KeyCode.Space)){	//If the spacebar is pressed
				score = 0;	//Reset the score
				ball.GetComponent<BallManager>().ResetBall();	//Reset the ball
				StartCoroutine(ball.GetComponent<BallManager>().LaunchBall(1f));	//Launch the ball
				isGameRunning = true;	//Set the bool isGameRunning to true to disable the main menu text and enable the in-game text
			}
		}
	}

	public IEnumerator IncrementScore(){
		scoreText.fontSize = (int)(scoreTextSize * 1.2f);	//Increase the score font size by 20%
		score++;	//Increment the score
		yield return new WaitForSeconds(0.1f);	//Wait for 0.1 seconds
		scoreText.fontSize = scoreTextSize;		//Set the score font size back to normal
	}

	void EndGame(){
		SetHighScore(score);		//Call the highscore function to potentially set a new high score
		isGameRunning = false;		//Set isGameRunning to false so the main menu is shown
		Debug.Log("Game Over!");	//Output "Game Over!" to the console log
	}

	public IEnumerator TextEffect(Text textBox, float waitTime){
		while(true){	//While the coroutine is running
			textBox.enabled = !textBox.enabled;	//Enable the text box if it is disabled, and disable it if it's enabled
			yield return new WaitForSeconds(waitTime);	//Wait for the wait time before looping again
		}
	}

	//Public int to get the high score
	public int GetHighScore(){
		return PlayerPrefs.GetInt("HighScore");
	}

	public void SetHighScore(int newHighScore){
		//If the new high score is greater than the currently stored high score
		if(newHighScore > PlayerPrefs.GetInt("HighScore")) {
			PlayerPrefs.SetInt("HighScore", newHighScore);	//Update the PlayerPrefs for the highscore (this stores the value so the high score isn't deleted after closing the game)
			highScore = newHighScore;	//Update the high score variable
		}
	}
}
