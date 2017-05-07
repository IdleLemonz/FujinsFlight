using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    private GameObject hero;
    private Vector3 offset;
    private float distance;  

	// Use this for initialization
	void Start () {
        hero = FindObjectOfType<GlideController>().gameObject;
        distance = Vector3.Distance(transform.position, hero.transform.position);
        offset = hero.transform.position - transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = hero.transform.position - offset;
        offset = hero.transform.position - transform.position;
    }
}
