using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager _instance;
    public AudioClip[] Audios;
    private AudioSource ao;
	// Use this for initialization
	void Start () {
        _instance = this;
        ao = GetComponent<AudioSource>();
    }
    public int FindIndexByName(string name)
    {
        for (int i = 0; i < Audios.Length; i++)
        {
            if (Audios[i].name == name)
                return i;
        }
        return -1;
    }
	public void AudioPlay(string name)
    {
     
        ao.PlayOneShot(Audios[FindIndexByName(name)]);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
