using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGameManager : MonoBehaviour {
    public GameObject 图鉴;
    public GameObject 药物;
    public GameObject 病毒;
    // Use this for initialization
    void Start () {
		
	}
    
    public void 病毒Ctr()
    {
        AudioManager._instance.AudioPlay("鼠标点击");
        药物.SetActive(false);
        病毒.SetActive(true);
    }
    public void 药物Ctr()
    {
        AudioManager._instance.AudioPlay("鼠标点击");
        病毒.SetActive(false);
        药物.SetActive(true);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("game");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void 图鉴显示()
    {
        AudioManager._instance.AudioPlay("鼠标点击");
        图鉴.SetActive(!图鉴.activeSelf);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
