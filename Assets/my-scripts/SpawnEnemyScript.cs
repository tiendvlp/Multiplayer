using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class SpawnEnemyScript : NetworkBehaviour {

	public GameObject objectToSpawn;
	public float timeToWaitBetweenSpawns = 2.0f;
	private float timer = 0;
	GameObject[] players;
	public bool startGame = false;

	void Start () {
		
	}
	
	void Update () {
		if (!startGame) {
			return;
		}
		timer += Time.deltaTime;
		if (timer > timeToWaitBetweenSpawns) {
			CmdSpawnEnemy();
			timer = 0;
		}
	}

	[Command]
	void CmdSpawnEnemy () {
		GameObject enemy = (GameObject) Instantiate(objectToSpawn, transform.position, transform.rotation);
		players = GameObject.FindGameObjectsWithTag ("Player");
		int Index = Random.Range (0, 1000) % players.Length;
		GameObject playerToAttack = players [Index];
		Debug.Log (playerToAttack.transform.name);
		enemy.GetComponent<EnemyMovement>().player =  playerToAttack;
		enemy.GetComponent<EnemyAttack>().player = playerToAttack;
		NetworkServer.Spawn(enemy);
	}

	public override void OnStartServer () {
		timer = 0;
		startGame = true;
	}
}
