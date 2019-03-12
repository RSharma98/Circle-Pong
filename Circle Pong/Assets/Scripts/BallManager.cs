using System.Collections;
using UnityEngine;

public class BallManager : MonoBehaviour {

	GameObject gm;					//The gamemaster object in the scene
	ScoreManager scoreManager;		//The ScoreManager component
	TrailRenderer trailRenderer;	//The trail renderer that is attached to the ball
	
	Vector2 vel;	//The velocity of the ball
	[SerializeField] private Vector2 launchVel;	//The velocity (speed) at which the ball should launch
	[SerializeField] private Vector2 maxVel;	//The maximum velocity the ball should achieve

	// Use this for initialization
	void Start () {
		gm = GameObject.Find("GM");	//Find the GameMaster object
		scoreManager = gm.GetComponent<ScoreManager>();	//Get the Score Manager component from the GM
		trailRenderer = GetComponent<TrailRenderer>();	//Get the trail renderer from the ball
	}

	void Update(){
		vel = GetComponent<Rigidbody2D>().velocity;	//Get the current velocity of the ball
		if(vel != Vector2.zero){	//If the ball's velocity is not equal to zero
			//Do NOT let the x velocity exceed the max velocity or fall below the launch velocity
			if(vel.x > maxVel.x || vel.x < -maxVel.x){
				int multiplier = vel.x > 0 ? 1 : -1;
				vel.x = maxVel.x * multiplier;
			} else if(vel.x < launchVel.x && vel.x > -launchVel.x){
				int multiplier = vel.x > 0 ? 1 : -1;
				vel.x = launchVel.x * multiplier;
			}

			//Do NOT let the y velocity exceed the max velocity or fall below the launch velocity
			if(vel.y > maxVel.y || vel.y < -maxVel.y){
				int multiplier = vel.y > 0 ? 1 : -1;
				vel.y = maxVel.y * multiplier;
			} else if(vel.y < launchVel.y && vel.y > -launchVel.y){
				int multiplier = vel.y > 0 ? 1 : -1;
				vel.y = launchVel.y * multiplier;
			}
			GetComponent<Rigidbody2D>().velocity = vel;	//Assign the new velocity
		}
	}
	
	//Everytime the ball collides with a paddle, increment the score in the score manager component
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Paddle"){
			StartCoroutine(scoreManager.IncrementScore());
		}
	}

	public IEnumerator LaunchBall(float waitTime){
		yield return new WaitForSeconds(waitTime);	//Wait for the wait time
		Vector2 launchDir = new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));	//Create a random Vector2 to randomise the launch direction
		vel = new Vector2(launchVel.x * launchDir.x, launchVel.y * launchDir.y);	//Set the velocity to be equal to the launch speed multiplied by the launch direction
		GetComponent<Rigidbody2D>().velocity = vel;		//Assign the velocity
		trailRenderer.enabled = true;	//Enable the trail renderer on the ball
	}

	public void ResetBall(){
		trailRenderer.enabled = false;		//Disable the trail renderer on the ball
		transform.position = Vector2.zero;	//Set the ball's position to 0 (the centre)
		vel = Vector2.zero;					//Set the velocity to 0
		GetComponent<Rigidbody2D>().velocity = vel;		//Assign the velocity
	}
}
