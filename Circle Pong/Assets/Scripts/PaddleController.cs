using UnityEngine;

public class PaddleController : MonoBehaviour {

	[SerializeField] private float rotSpeed;	//The rotation speed of the paddles
	
	void Update () {
		//Every frame, rotate the paddles around the centre on the z axis at a rate depending on whether there is any input
		transform.RotateAround(Vector2.zero, Vector3.back, rotSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal"));
	}
}
