using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerID : NetworkBehaviour {

	[SyncVar]public string playerUniqueName;
	// dùng để tạo ra network id
	private NetworkInstanceId playerNetID;

	public override void OnStartLocalPlayer() {
		GetNetIdentity ();
		SetIdentity ();
	}


	void Start () {

	}
	
	void Update () {
		if (name == "" || this.transform.name == "ironmanPrefab(Clone)") {
			SetIdentity ();
		}
	}

	void GetNetIdentity () {
		playerNetID = GetComponent<NetworkIdentity> ().netId;
		CmdTellServerMyIdentity (makeUniqueIdentity());
	}

	void SetIdentity () {
		if (!isLocalPlayer) {
			this.transform.name = playerUniqueName;	
		} else {
			this.transform.name = makeUniqueIdentity ();		
		}

	}

	[Command] 
	void CmdTellServerMyIdentity (string playerNetID) {
		this.playerUniqueName = playerNetID;
	}

	string makeUniqueIdentity () {
		return "player" + playerNetID.ToString ();
	}



}
