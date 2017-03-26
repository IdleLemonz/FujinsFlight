using UnityEngine;
using InControl;
using System.Collections;

public class Brake : MonoBehaviour
{
    public float isBraking = 0;
    public float brakePower = 50;
    public float brakeCapacity = 100;
    public float coolDownTime = 5;

    private float currentBrakeCapacity;
    private GlideController gc;
    private bool coolingDown = false;

    // Use this for initialization
    void Start()
    {
        gc = GetComponent<GlideController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InputDevice device = InputManager.ActiveDevice;

        isBraking = (-device.RightStickY) + Input.GetAxis("Fire1");
        
        if(!coolingDown)
        {
            //gc.BrakeVelocity = brakePower * -transform.forward * isBraking;
            coolingDown = true;
        }
        else
        {
            coolDownTime -= Time.deltaTime;
            if(coolDownTime <= 0)
            {
                coolingDown = false;
            }
        }
    }
}
