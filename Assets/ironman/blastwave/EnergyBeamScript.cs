using UnityEngine;
using System.Collections;

public class EnergyBeamScript : MonoBehaviour {

	public float scaleIncrement = 0.001f;
	public float beamLength = 1.0f;

	private bool isShooting = false;
	private float scaleX, scaleY;
	// Use this for initialization
	void Start () {
		isShooting = false;
		scaleX = transform.localScale.x;
		scaleY = transform.localScale.y;
		transform.localScale = new Vector3(0, 0, 0);
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isShooting) {
			transform.localScale += new Vector3(0, 0, scaleIncrement * Time.deltaTime);
			if(transform.localScale.z > beamLength){
				isShooting = false;
				transform.localScale = new Vector3(0, 0, 0);
			}
		}
	
	}


	void Shoot(){

		if (isShooting) {
			return;
		} else {
			isShooting = true;
			transform.localScale = new Vector3(scaleX, scaleY, 0);

		}
	}
}
