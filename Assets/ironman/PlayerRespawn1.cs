using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerRespawn1 : NetworkBehaviour  {
	[SyncVar] bool respawning = false;
	public float timeToRespawn;
	private float cacheTimeToRespawn = 2;
	private bool startInvoke = false;
	// Use this for initialization
	void Start () {
		respawning = false;
		cacheTimeToRespawn = timeToRespawn;
	}
	
	void Update () {
		if (!isServer) {
			return;
		}
		float dt = Time.deltaTime;
		if (GetComponent<PlayerHealth> ().currentHealth <= 0 && !respawning) {
			respawning = true;
			startInvoke = true;
		}
		invoke (RpcRespawn, dt, NetworkManager.singleton.GetStartPosition().position);

	}

	void invoke  (abc xyz, float dt, Vector3 pos) {
		if (startInvoke) {
			cacheTimeToRespawn -= dt;
			if (cacheTimeToRespawn <= 0) {
				startInvoke = false;
				xyz(pos);
				cacheTimeToRespawn = timeToRespawn;
			}
		}
	}

	delegate void abc (Vector3 pos);

	[ClientRpc]
	void RpcRespawn (Vector3 position) {
		transform.position = position;
		GetComponent<PlayerHealth> ().currentHealth = GetComponent<PlayerHealth> ().startingHealth;
		GetComponent<PlayerHealth> ().isDead = false;
		GetComponent<Animator> ().Play ("IdleWalk");
		respawning = false
	}
}
