using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Ammo : MonoBehaviour

{
	public int ammo;

	private void OnTriggerEnter(Collider other)
	{


		if (tag == "Player")
		{
			Destroy(gameObject);
		}
	}
}