using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour 
{
	public float bounceIntensity = 50;
	//public float bounceDamping = 0.9f;
	public string obstacleTag = "Obstacle";
	public string groundTag = "Ground";
    public int damage = 1;

	private GlideController glide;
    private Health h;

	// Use this for initialization
	void Start () 
	{		
		glide = GetComponent<GlideController>();
        h = GetComponent<Health>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {}

	void OnCollisionEnter(Collision collision)
	{		
		if (collision.gameObject.tag == groundTag )
		{
			if (collision.contacts[0].normal.y >= 0.4f)
			{
				//Debug.Log("You died.");
                h.TakeDamage(h.maxLives);
			}
			else if (collision.contacts[0].normal.y < 0.4f)
			{
				//Debug.Log("You had a collision with a wall surface.");
				glide.BounceVelocity = bounceIntensity * Vector3.Reflect(glide.transform.forward, collision.contacts[0].normal);
				glide.acceleration *= 0.5f;
                h.TakeDamage(damage);
            }					 
		}
		else if (collision.gameObject.tag == obstacleTag)
		{
			glide.BounceVelocity = bounceIntensity * Vector3.Reflect(glide.transform.forward, collision.contacts[0].normal);
			glide.acceleration *= 0.5f;
            h.TakeDamage(damage);
        }	
	}
}
