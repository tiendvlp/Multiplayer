using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class CarDriveLeftSideScript : NetworkBehaviour {

	private GameObject startingPoint;
	private GameObject endingPoint;
	
	public float speed = 23.0f;

	void Start () {
		startingPoint = GameObject.Find ("starting-point");
		endingPoint = GameObject.Find ("ending-point");
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - speed * Time.deltaTime);
		if (this.transform.position.z < startingPoint.transform.position.z) {
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, endingPoint.transform.position.z);
		}
		
	}
}
