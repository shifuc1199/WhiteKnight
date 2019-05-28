using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class PlayerAnimationEvent : MonoBehaviour {
    public GameObject Effect;
    GameObject temp;
    public GameObject Player;
    // Use this for initialization
    void Start () {
        
    }
     

     public void CreateEffect()
    {
        temp=Instantiate(Effect, transform.position+new Vector3(0,0.6f,0), Quaternion.identity);
    }
   public void FireEffect()
    {
        gameObject.SetActive(false);
    }
    public void DestroyEffect()
    {
        Player.GetComponentInChildren<SpriteRenderer>().enabled = true;
        Player.GetComponent<PlayerState>().isSiphon = false;
        Destroy(temp);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
