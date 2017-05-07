using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCulling : MonoBehaviour {

    [Tooltip("The distance you want to activate objects in. Anything outside this will be culled.")]
    public float activationRadius = 100;

    GameObject[] sceneObjects;

	// Use this for initialization
	void Start ()
    {
        sceneObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject item in sceneObjects)
        {
            if (item.gameObject.tag != "MainCamera" && item.gameObject.tag != "Player" && item.gameObject.tag != "CullMask")
            {
                item.SetActive(false);
            }
        }

        StartCoroutine(CheckCulling());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator CheckCulling()
    {
        float radius2 = activationRadius * activationRadius;

        while (true)
        {
            for (int i = 0; i < sceneObjects.Length; i++)
            {
                if (sceneObjects[i] == null) continue;

                Vector3 diff = transform.position - sceneObjects[i].transform.position;
                float distSquared = diff.x * diff.x + diff.y * diff.y + diff.z * diff.z;

                if (distSquared < radius2 && sceneObjects[i].gameObject.tag != "Player" && sceneObjects[i].gameObject.tag != "MainCamera" && sceneObjects[i].gameObject.tag != "UI" && sceneObjects[i].gameObject.tag != "CullMask")
                {
                    if (sceneObjects[i].activeSelf == false)
                    {
                        sceneObjects[i].SetActive(true);
                    }
                }
                else if (distSquared >= radius2 && sceneObjects[i].gameObject.tag != "Player" && sceneObjects[i].gameObject.tag != "MainCamera" && sceneObjects[i].gameObject.tag != "UI" && sceneObjects[i].gameObject.tag != "CullMask")
                {
                    //print("culling");
                    if (sceneObjects[i].activeSelf == true)
                    {
                        sceneObjects[i].SetActive(false);
                    }
                }
            }

            //yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(0.8f);
        }
    }
}
