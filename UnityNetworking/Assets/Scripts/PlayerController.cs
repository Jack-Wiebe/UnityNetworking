using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

	[SerializeField]
	GameObject BulletPrefab;
	[SerializeField]
	Transform bulletSpawn;

	[SerializeField]
	float m_bulletSpeed;

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}

	void Update()
	{
		if (!isLocalPlayer) {
			return;
		}
		float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		float z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);

		if (Input.GetKeyDown (KeyCode.Space)) {
			CmdFire ();
		}

	}

	[Command]
	private void CmdFire()
	{
		GameObject bullet = GameObject.Instantiate (BulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;

		bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * m_bulletSpeed;

		NetworkServer.Spawn (bullet);

		Destroy (bullet, 2.0f);
	}

}
