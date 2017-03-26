using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float positionOffset = 0.1f;
    public float rotationOffset = 0.1f;
    public float minDistance = 1;
    public float maxDistance = 10;

    private GlideController glide;
    public float distance;

    public Vector3 offset;
    // Use this for initialization
    void Start ()
    {

        glide = target.transform.parent.gameObject.GetComponent<GlideController>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(target)
        {
            //GameObject target = followTarget.transform.parent.gameObject;

            distance = Vector3.Distance(target.transform.position, transform.position);

            /*if(distance > maxDistance)
            {
                transform.position = Vector3.Lerp(transform.position, target.transform.position, 0.3f);
            }*/

            transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, rotationOffset);
            transform.position = Vector3.Lerp(transform.position, target.transform.position, positionOffset);

            offset = target.transform.position - transform.position;
        }
    }
}
