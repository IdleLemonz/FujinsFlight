using UnityEngine;
using System.Collections;

public class OrbInfo : MonoBehaviour
{
    public int orbValue = 1;
    //[Tooltip("Set to zero to not rotate on this angle.")]
    //public float rotateSpeedX = 0;
    //[Tooltip("Set to zero to not rotate on this angle.")]
    //public float rotateSpeedY = 50;
    //[Tooltip("Set to zero to not rotate on this angle.")]
    //public float rotateSpeedZ = 0;

    private float time;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //time = Time.deltaTime;
        //transform.Rotate(rotateSpeedX * time, rotateSpeedY * time, rotateSpeedZ * time);
    }
}
