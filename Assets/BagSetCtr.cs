using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BagSetCtr : MonoBehaviour {
    public Image 图片;
    public Text 介绍;
    public Sprite[] 病毒图片;
    public string[] 病毒介绍;
    int index;
	// Use this for initialization
	void Start () {
        图片.sprite = 病毒图片[0];
        介绍.text = 病毒介绍[0];
    }
	public void Next()
    {
        index++;
        index %= 病毒图片.Length;
        图片.sprite = 病毒图片[index];
        介绍.text = 病毒介绍[index];
    }
	// Update is called once per frame
	void Update () {
        介绍.text = 介绍.text.Replace("\\n", "\n");

    }
}
