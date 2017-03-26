using UnityEngine;
using System.Collections;
using InControl;

public class DashScript : MonoBehaviour 
{		
	[Tooltip("How quickly the glider dashes - faster dash = longer dash.")]
	public float dashIntensity = 60.0f;
	[Tooltip("How long you have to wait between dashes.")]
	public float dashCooldown = 70.0f;

	private float dashLeft = 0;
	private float currentSpeed = 0;
	private float dashRemains = 0;
	private float dashDirection = 0;
	private bool keyDown = false;

	//public int counter = 0;

    private Rigidbody rb;
	private GlideController glide;

	// Use this for initialization
	void Start ()
    {
		glide = GetComponent<GlideController>();
        rb = GetComponent<Rigidbody>();
    }

	void FixedUpdate ()
	{
		InputDevice device = InputManager.ActiveDevice;

		// use the bounce script idea to modify the gliders velocity
		// make a new vector that uses transform.right to get the direction, then times it by a force, then add it to the GlideController

		if (device != null) 
		{		
			if((Input.GetKeyUp("q") || !(device.LeftBumper.IsPressed)) && (Input.GetKeyUp("3") || !(device.RightBumper.IsPressed)))
			{
				keyDown = false;
			}

			if (dashLeft <= 0)
			{	
				DashCoolDown();
				if (keyDown == false)
				{
					dashDirection = 0;
					if((Input.GetKeyDown("q")|| device.LeftBumper.IsPressed))
					{						
						// Set it to left
						dashDirection = -1;
						dashLeft = dashCooldown;
						keyDown = true;
					}
					if((Input.GetKeyDown("e")|| device.RightBumper.IsPressed))
					{	
						// Set it to right
						dashDirection = 1;
						dashLeft = dashCooldown;
						keyDown = true;
					}
				}
			}
			else
			{
				glide.DashVelocity = dashIntensity * transform.right * dashDirection;            
			}
		}
	}

	void DashCoolDown()
	{
		if(dashLeft > 0)
		{
			//Debug.Log(dashLeft);
			dashLeft -= 1;
		}
	}

	float DashSpeed()
	{
		if (currentSpeed > 0)
		{
			currentSpeed -= 0.5f;
		}
		else if (currentSpeed <= 0)
		{			
			currentSpeed = 0;
		}
		return currentSpeed;
	}
}
