using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InControl;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GlideController : MonoBehaviour
{
    [Tooltip("Whether to use this objects artificial gravity or not.")]
    public bool artificialGravity = true;
    [Tooltip("The speed at which the glider rotates.")]
    public float turningSensitivity = 45.0f;
    [Tooltip("The current speed of the glider. Modifying this modifies the starting speed of the hero.")]
    public float acceleration = 30.0f;
    [Tooltip("The glider's max speed.")]
    public float maxVelocity = 100;
    [Tooltip("How quickly the glider slows when aimed upward. Smaller numbers means faster deceleration.")]
    public float upDeccelerate = 65;
    [Tooltip("How fast the glider accelerates when aimed downward. Smaller numbers means faster acceleration.")]
    public float downAccelerate = 50;
	[Tooltip("Use a number between 0.9 and 0.01. The smaller the number, the quicker the dash slows down.")]
	public float dashDamping = 0.9f;
	[Tooltip("Use a number between 0.9 and 0.01. The smaller the number, the quicker you slow down from bouncing off a collision.")]
	public float bounceDamping = 0.9f;
	[Tooltip("Use a number between 0.09 and 0.01. The smaller the number, the quicker you return to normal speed after touching wind currents.")]
	public float windDamping = 0.9f;
	[Tooltip("Attach the Game Controller ")]
    public GameObject gameController;

	private float dipAnglesPerSecond = 150.0f; // how fast the character dips down
	private ScoreKeeping scoreKeeper;
	private float smooth = 1.0f;
    private float minVelocity = 0; // The lowest possible flight speed.
    private Vector3 angles = Vector3.zero;
    private Rigidbody rb;    
	private float forwardSpeed = 0;

   // public LayerMask terrainLayer;
	private Vector3 bounceVelocity;
	public Vector3 BounceVelocity
	{
		get { return bounceVelocity; }
		set { bounceVelocity = value; }
	}

	private Vector3 windVelocity;
	public Vector3 WindVelocity
	{
		get { return windVelocity; }
		set { windVelocity = value; }
	}

	private Vector3 dashVelocity;
	public Vector3 DashVelocity
	{
		get { return dashVelocity; }
		set { dashVelocity = value; }
	}

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scoreKeeper = gameController.GetComponent<ScoreKeeping>();
		acceleration = 50;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		//speed = rb.velocity.magnitude;
        InputDevice device = InputManager.ActiveDevice;

		float horizontal = Input.GetAxis("Horizontal") + device.LeftStick.X; // move horizontal - get the control stick or keyboards horizontal movement input
        float vertical = Input.GetAxis("Vertical") + device.LeftStick.Y; // move vertical - get the control stick or keyboards vertical movement input

		angles.z = Mathf.LerpAngle(angles.z, 0, Time.deltaTime * smooth); // banking reset        

		forwardSpeed = Vector3.Dot(transform.forward, rb.velocity); // gets the forward velocity
		forwardSpeed = 1.0f - Mathf.Clamp(forwardSpeed, 0, 75) / 85.0f; // clamps the speed value, subtracting 1 at the start reverses the angle adjustment curve by making it negative 1, dividing by 100 normalises it,
		forwardSpeed *= forwardSpeed; // squaring it to create a curved adjustment in speed
		float dipRate = (forwardSpeed) * dipAnglesPerSecond * Time.deltaTime; // 200 = angles per second -- how much you dip.
        angles.x += dipRate; // make it dip

		angles.x = Mathf.Clamp(angles.x + vertical * turningSensitivity * Time.deltaTime, -60, 90); // up and down rotation with control stick
		angles.y = angles.y + horizontal * turningSensitivity * Time.deltaTime; // left and right rotation
		angles.z = Mathf.Clamp(angles.z + horizontal * -turningSensitivity * Time.deltaTime, -90, 90); // banking rotation 
        transform.eulerAngles = angles;

        Mathf.Clamp(acceleration, 0, maxVelocity);

        rb.velocity = transform.forward * Accelerate() + BounceVelocity + WindVelocity + DashVelocity;

        WindVelocity *= windDamping;
        BounceVelocity *= bounceDamping;
		DashVelocity *= dashDamping;        
    }

    float Accelerate()
    {
        // If glider pointed downward and acceleration greater than 0, start to speed up
        if (angles.x > 0 && acceleration > 0)
        {
            // if you are facing down, accelerate quickly
            acceleration += angles.x / (downAccelerate);
        }
        // else if glider pointed down but acceleration is in the negatives due to gravity, after upward flight, 
        // convert the negative acceleration (used for gravity pull) into positive downwards acceleration
        else if (angles.x > 0 && acceleration < 0)
        {
            // if you are facing up, deccelerate slowly - slower than you speed up going down
            acceleration *= -1;
        }
        else if (angles.x < 0)
        {
            // if you are facing up, deccelerate slowly - slower than you speed up going down
            acceleration += angles.x / (upDeccelerate);
        }

        if (acceleration < minVelocity)
        {
            // if in built gravity is not in use, reset acceleration to 0
            if (artificialGravity == false)
            {
                acceleration = minVelocity;
            }
        }
        else if (acceleration > maxVelocity)
        {
            acceleration = maxVelocity;
        }
        Mathf.Clamp(acceleration, 0, maxVelocity);
        return acceleration;
    }

    void OnTriggerEnter(Collider col)
    {    
        if (col.gameObject.tag == "ScoreCollectable")
        {            
            scoreKeeper.AddScore(col);
            col.gameObject.SetActive(false);
		}
    }
}
