using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.CrossPlatformInput;
using DigitalRuby.PyroParticles;
using UnityEngine.Networking;
public class PlayerShoot : NetworkBehaviour {
	RaycastHit shootHit;
	Ray shootRay;
	LineRenderer laserLine;
	int shootableMask;

	bool isShooting = false;
	public int damagePoints = 10;
	public bool isEnabled = true;
	public GameObject[] projectilePrefabs;
	private GameObject selectedProjectilePrefab;
	private GameObject currentPrefabObject;
	private FireBaseScript currentPrefabScript;
	public GameObject projectileSpawnPoint;
	public Material[] materials;

	// Use this for initialization
	void Start () {
		shootableMask = LayerMask.GetMask ("Enemies");
		//laserLine = GetComponentInChildren<LineRenderer> ();
		//laserLine.enabled = false;
		IntializeProjectile();
		IntializeCustume ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer || GetComponent<PlayerHealth> ().isDead) {
			return;
		}
		#if !MOBILE_INPUT
		if (Input.GetButtonDown ("Fire1") && isShooting == false) {
			CmdShoot ();
			
		} 
		#else
		if(CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0){
			CmdShoot();
		}
		#endif
	
	}

	[Command]
	public void CmdShoot(){
		isShooting = true;
		SpawnProjectiled ();
		//laserLine.enabled = true;
		/*
		shootRay.direction = transform.forward;

		if (Physics.Raycast (shootRay, out shootHit, 100.0f, shootableMask)) {
			//laserLine.SetPosition (1, shootHit.point);
			EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth> ();
			if (enemyHealth != null) {
				enemyHealth.TakeDamage (damagePoints, shootHit.point);
			}
		} else {
			//laserLine.SetPosition (1, LaserBeamEnd.transform.position);
		}
		*/
		Invoke ("StopShooting", 0.15f);
	}

	void StopShooting(){
		//laserLine.enabled = false;
		isShooting = false;
	}

	public void DisableShooting(){
		isEnabled = false;
	}

	private void IntializeProjectile () {
		int selected = UnityEngine.Random.Range (1, 1000) % this.projectilePrefabs.Length;
		selectedProjectilePrefab = this.projectilePrefabs [selected];
	}

	private void SpawnProjectiled () {
		currentPrefabObject = GameObject.Instantiate (selectedProjectilePrefab);
		currentPrefabObject.transform.position = this.projectileSpawnPoint.transform.position;
		currentPrefabObject.transform.rotation = this.projectileSpawnPoint.transform.rotation;
		NetworkServer.Spawn (currentPrefabObject);
		currentPrefabObject.GetComponent<FireProjectileScript> ().ownerName = currentPrefabObject.name;
	}

	private void IntializeCustume () {
		int rand = UnityEngine.Random.Range (1, 1000) % this.materials.Length;
		GetComponent<Renderer> ().material = this.materials[rand];
	}
}
