using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerRespawn : NetworkBehaviour {
	bool respawning = false;
	void Start () {
		respawning = false;
		Debug.Log ("bucu");
	}
	
	void Update () {
			if (GetComponent<PlayerHealth> ().currentHealth <= 0 && !respawning) {
				Debug.Log ("Respawn");
				Invoke ("RpcRespawn", 3.0f);
			}
	}

	[ClientRpc]
	void RpcRespawn () {
		transform.position = NetworkManager.singleton.GetStartPosition ().position;
		GetComponent<Animator> ().Play ("IdleWalk");
		GetComponent<PlayerHealth> ().currentHealth = GetComponent<PlayerHealth> ().startingHealth;
		GetComponent<PlayerHealth> ().isDead = false;
		respawning = false;
	}
}
