using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {

	public int startingHealth = 100;
	[SyncVar]public int currentHealth = 100;
	float shakingTimer = 0;
	public float timeToShake = 1.0f;
	public float shakeIntensity = 3.0f;
	bool isShaking = false;
	[SyncVar]public bool isDead = false; //a
	Animator anim;

	private GameObject textHealth;
	public GameObject healthyBarPref;
	private GameObject healthyBar;
	private GameObject healthyBarPosition;
	// Use this for initialization
	void Start () {
		textHealth = GameObject.Find ("healthyText");
		currentHealth = startingHealth;
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		updateHealthText ();
		updateHealthyBar ();
		if (!isDead) {
			if (currentHealth <= 0) {
				currentHealth = 0;
				Death ();
			}
		}
	}

	void updateHealthText() {
		if (isLocalPlayer) {
			textHealth.GetComponent<Text> ().text = currentHealth + "";
		}
	}

	void updateHealthyBar () {
		if (isLocalPlayer) {
			GetComponentInChildren<Slider> ().value = currentHealth;
		}
	}

	public void TakeDamage(int amount){
		if (isDead)
			return;

		currentHealth -= amount;
		if (currentHealth <= 0) {
			Death ();
		}
	}

	public void Death(){
		//Debug.Log (isDead);
		if (isDead)
			return;
//		isDead = true;
		if (isServer) {
			RpcDeath ();
		} else {
			anim.SetTrigger ("Death");
			isDead = true;
		}
	}

	[ClientRpc]
	void RpcDeath () {
			anim.SetTrigger ("Death");
			isDead = true;
	}
}
