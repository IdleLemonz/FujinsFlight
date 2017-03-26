using UnityEngine;
using System.Collections;

public class NewCameraTest : MonoBehaviour
{
    public Transform target;
    public float positionOffset = 0.1f;
    public float rotationOffset = 0.1f;
    public float minDistance = 1;
    public float maxDistance = 10;

    public float distance;

    public Vector3 offset;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            /*distance = Vector3.Distance(target.transform.position, transform.position);
            Debug.Log(distance);
            if (distance != maxDistance)
            {*/ 
                  transform.position = Vector3.Lerp(transform.position, target.transform.position, 0.3f);
            //}

            //transform.position = Vector3.Lerp(transform.position, target.transform.position, positionOffset);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, rotationOffset);
          

            offset = target.transform.position - transform.position;
        }
    }
}
