using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDestroyGround : MonoBehaviour {
    public bool isOpen;
    public GameObject BreakeGround;
    // Use this for initialization
    void Start () {
		
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            if(collision.gameObject.GetComponent<PlayerState>().isSlim)
            {
                if(isOpen)
                {
                    Destroy(BreakeGround);
                }
                Destroy(gameObject);
            }
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
