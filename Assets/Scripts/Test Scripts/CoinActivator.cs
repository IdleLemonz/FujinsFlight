using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CoinActivator : MonoBehaviour 
{
    [Tooltip("The distance you want to activate coins in.")]
    public float activationRadius = 500;

	private List<GameObject> m_coins;  
    private List<RotateObject> coinRotates;

    // Use this for initialization
    void Start () 
	{        
        OrbInfo[] orbs = FindObjectsOfType<OrbInfo>();
		m_coins = new List<GameObject>();
        coinRotates = new List<RotateObject>();        

        for (int i = 0; i < orbs.Length; i++)
        {
            m_coins.Add(orbs[i].gameObject);
            coinRotates.Add(m_coins[i].GetComponent<RotateObject>());
            coinRotates[i].enabled = false;
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
            for (int i = 0; i < m_coins.Count; i++)
            {
                if (m_coins[i] == null) continue;

                Vector3 diff = transform.position - m_coins[i].transform.position;
                float distSquared = diff.x * diff.x + diff.y * diff.y + diff.z * diff.z;

                if (distSquared < radius2)
                {                    
                    if (coinRotates[i].enabled == false)
                    {
                        coinRotates[i].enabled = true;
                    }
                }
                else
                {                    
                    if (coinRotates[i].enabled == true)
                    {
                        coinRotates[i].enabled = false;
                    }
                }     
            }

            //yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(1);
        }
	}
}
