using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CoinActivator : MonoBehaviour 
{
	public float activationRadius = 500;

	private List<GameObject> m_coins;

	// Use this for initialization
	void Start () 
	{
		OrbInfo[] orbs = FindObjectsOfType<OrbInfo>();
		m_coins = new List<GameObject>();

		// List of all coins gathered when game loads
		foreach (OrbInfo orb in orbs)
		{
			m_coins.Add(orb.gameObject);
			orb.GetComponent<RotateObject>().enabled = false;
		}

		StartCoroutine(CheckCoins());
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	IEnumerator CheckCoins()
	{
		float radius2 = activationRadius * activationRadius;

		while(true)
		{
			int i = 0;
			foreach (GameObject coin in m_coins)
			{
				if (coin == null) continue;

				Vector3 diff = transform.position - coin.transform.position;
				float distSquared = diff.x * diff.x + diff.y * diff.y + diff.z * diff.z;

				if (distSquared < radius2)
				{
					if (coin.GetComponent<RotateObject>().enabled == false)
					{
						coin.GetComponent<RotateObject>().enabled = true;
					}
				}
				else
				{
					if (coin.GetComponent<RotateObject>().enabled == true)
					{
						coin.GetComponent<RotateObject>().enabled = false;
					}
				}

				i++;
			}

			yield return new WaitForEndOfFrame();
		}
	}
}
