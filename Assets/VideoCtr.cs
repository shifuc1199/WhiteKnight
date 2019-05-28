using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class VideoCtr : MonoBehaviour {
    public VideoPlayer vp;
	// Use this for initialization
	void Start () {
        

    }
	public void CheckISComplete()
    {
       if(!vp.isPlaying)
        {
            vp.Play();
        }

       else if ((long)vp.frameCount==vp.frame)
        {
            SceneManager.LoadScene("start");
        }
    }
	// Update is called once per frame
	void  Update () {
        CheckISComplete();
    }
}
