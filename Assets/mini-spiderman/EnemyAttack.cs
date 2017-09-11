using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public int attackDamage = 60;
	public float timeBetweenAttacks = 0.5f;
	// todo: grab the player's health script
	bool playerInRange = false;
	float timer;


	Animator anim;
	public GameObject player;
	EnemyHealth enemyHealth;
	PlayerHealth playerHealth;
	Animator playerAnim;
	bool isEnabled = true;
	UnityEngine.AI.NavMeshAgent agent;
	PlayerShoot playerShoot;
	IronManBehaviorScript playerMovement;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		enemyHealth = GetComponent<EnemyHealth> ();
		playerHealth = player.GetComponent<PlayerHealth> ();
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		playerShoot = player.GetComponent<PlayerShoot> ();
		playerMovement = player.GetComponent<IronManBehaviorScript> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (isEnabled == false)
			return;
		
		timer += Time.deltaTime;
		if (timer >= timeBetweenAttacks && playerInRange == true && enemyHealth.currentHealth > 0) {
			Attack ();
		}

		if (playerHealth.currentHealth <= 0) {
			playerHealth.Death();

			isEnabled = false;
			anim.enabled = false;
			//anim.SetTrigger ("idle");
			agent.enabled = false;
			playerShoot.DisableShooting ();
		}

	
	}

	// whenever we get close to the player, we can attack
	void OnTriggerEnter(Collider other){
		if (other.gameObject == player) {
			playerInRange = true;
		}
	}


	void OnTriggerExit(Collider other){
		if (other.gameObject == player) {
			playerInRange = false;
		}
	}


	void Attack(){
		timer = 0f;
		anim.SetTrigger ("Attack");
		if (playerHealth.currentHealth > 0) {
			playerHealth.TakeDamage (attackDamage);
		}
	}
}
