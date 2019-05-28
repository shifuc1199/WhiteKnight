using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {
    public TriggerType thisType;
    public int TeachIndex;
    public GameObject[] CopyVirus;
    bool isEnter;
	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            switch(thisType)
            {
                case (TriggerType.Copy):
                    for (int i = 0; i < CopyVirus.Length; i++)
                    {
                        CopyVirus[i].GetComponent<copyCtr>().enabled = true;
                    }
                    break;
                case (TriggerType.Teach):
                    if (!isEnter)
                    {
                        UIManager._instance.TeachIndex = this.TeachIndex;
                        isEnter = true;
                    }
                    break;
                case (TriggerType.End):
                    GameManager._instance.End();
                    break;
            }
           
           
        }
    }
    
    // Update is called once per frame
    void Update () {
		
	}
}
public enum TriggerType
{
    Teach,
    Copy,
        End
}
