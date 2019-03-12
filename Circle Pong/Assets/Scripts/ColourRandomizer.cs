using System.Collections;
using UnityEngine;

public class ColourRandomizer : MonoBehaviour {

	private Camera cam;			//The camera in the scene
	public Color[] colours;	//The colours that the background should switch between

	[SerializeField] private float fadeSpeed;	//The speed at which the colours should fade

	void Start(){
		cam = Camera.main;	//Get a reference to the camera
		StartCoroutine(SwitchColour());	//Start the coroutine to switch colours
	}

	public IEnumerator SwitchColour(){
		int i = 0;	//Initialise int i (this will be used to loop through the colours)
		while(i <= colours.Length){		//While i is not greater than the size of the colours array
			if(cam.backgroundColor != colours[i]){	//Check if the camera's background colour is not the current target colour
				//Create three floats to represent the RBG values of the current camera colour and initialise them
				float r, g, b;
				r = cam.backgroundColor.r;
				g = cam.backgroundColor.g;
				b = cam.backgroundColor.b;
				//Use maths to move the camera's RGB values towards those of the target colour
				r = Mathf.MoveTowards(r, colours[i].r, fadeSpeed * Time.deltaTime);
				g = Mathf.MoveTowards(g, colours[i].g, fadeSpeed * Time.deltaTime);
				b = Mathf.MoveTowards(b, colours[i].b, fadeSpeed * Time.deltaTime);
				//Set the camera background colour and the sprite colours equal to the new RGB values
				cam.backgroundColor = new Color(r, g, b, 1f);
			} else {
				//If the camera's background colour is equal to the target colour
				yield return new WaitForSeconds(0.5f);		//Wait for 0.5 seconds
				i = (i == colours.Length - 1) ? 0 : i + 1;	//If the value of i is equal to the length of the colours array reset i to zero otherwise increment it by one
			}
			yield return null;
		}
	}
}
