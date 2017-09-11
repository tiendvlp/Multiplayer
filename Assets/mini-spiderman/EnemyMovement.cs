using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour {

	public GameObject player;
	UnityEngine.AI.NavMeshAgent nav;

	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();	
	}
	
	// Update is called once per frame
	void Update () {
		EnemyMove ();
	}
	void EnemyMove () {
		if (!GetComponent<EnemyHealth> ().isDead) {
			if (player != null) {
				if (player.GetComponent<PlayerHealth> ().isDead) {
					GetComponent<EnemyHealth> ().Death ();
					GetComponent<EnemyHealth> ().isDead = true;
				} else {
					if (player != null) {
						nav.SetDestination (player.transform.position);
					}
				}
			}
		}
	}
}
