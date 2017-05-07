using UnityEngine;
using System.Collections;

public class Updraft : MonoBehaviour
{
    [Tooltip("The tag name of the player.")]
    public string PlayerTag = "Player";
    [Tooltip("The amount of force that pushes the player.")]
    public float windIntensity = 50;   
	public enum FORCE_APPLICATION
	{
		INSTANT,
		SLOWLY,
		QUICKLY
	};
	[Tooltip("Whether the force of the wind is applied instantly, or slowly or quickly over time.")]
	public FORCE_APPLICATION forceApplication;
	//[Tooltip("Divides the total wind power into a percentage. 0.1 means 10% the power of the original. 0.4 means 40% the power of the original. 1 is 100%.")]
	//public float boostPercentage = 0.4f;

	private enum IN_WIND
	{
		IN,
		OUT
	};
	private IN_WIND inWind;

    private GlideController gc;   
	private float wind = 0;

    // Use this for initialization
    void Start() 	{	}
    // Update is called once per frame
    void FixedUpdate() { }

    void OnTriggerStay(Collider col)
    {    
		inWind = IN_WIND.IN;

        // Replace get component ???
        gc = col.GetComponent<GlideController>();


        if (gc != null)
        {
			if(forceApplication == FORCE_APPLICATION.INSTANT)
			{
				gc.WindVelocity = windIntensity * transform.right;

				// high +ve values = high tailwind, high -ve = headwind, zero = wind is side-on
				float tailwind = Vector3.Dot(gc.transform.forward, transform.right) * windIntensity;
				//gc.acceleration += tailwind * Time.deltaTime * boostPercentage;
				gc.acceleration += tailwind * Time.deltaTime;
				//Debug.Log(gc.acceleration);
			}
			else if(forceApplication == FORCE_APPLICATION.SLOWLY)
			{
				if(wind < windIntensity)
				{
					wind++;
				}

				gc.WindVelocity = wind * transform.right;

				// high +ve values = high tailwind, high -ve = headwind, zero = wind is side-on
				float tailwind = Vector3.Dot(gc.transform.forward, transform.right) * wind;
				//gc.acceleration += tailwind * Time.deltaTime * boostPercentage;
				gc.acceleration += tailwind * Time.deltaTime;
				//Debug.Log(wind);
			}
			else if(forceApplication == FORCE_APPLICATION.QUICKLY)
			{
				if(wind < windIntensity)
				{
					wind += 3;
				}

				gc.WindVelocity = wind * transform.right;

				// high +ve values = high tailwind, high -ve = headwind, zero = wind is side-on
				float tailwind = Vector3.Dot(gc.transform.forward, transform.right) * wind;
				//gc.acceleration += tailwind * Time.deltaTime * boostPercentage;
				gc.acceleration += tailwind * Time.deltaTime;
				//Debug.Log(wind);
			}
        }
    }
	void OnTriggerExit(Collider col)
	{
		inWind = IN_WIND.OUT;
		wind = 0;
	}
}