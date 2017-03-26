using UnityEngine;
using InControl;
using System.Collections;

public class Dive : MonoBehaviour {

    public float isDiving = 0;
    public float divePower = 50;
    public float diveCapacity = 100;

    private float currentDiveCapacity;
    private GlideController gc;

    // Use this for initialization
    void Start()
    {
        gc = GetComponent<GlideController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InputDevice device = InputManager.ActiveDevice;

        isDiving = device.RightTrigger + Input.GetAxis("Fire2");

        //gc.DiveVelocity = divePower * -transform.forward * isDiving;
    }
}
