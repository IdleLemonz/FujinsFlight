using UnityEngine;
using System.Collections;

public class WinTrigger : MonoBehaviour
{
    public GameObject gameController;

    private UIManager uIM;

	// Use this for initialization
	void Start ()
    {
        uIM = gameController.GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            uIM.WinGame();
        }
    }
}
