using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    public static GameManager _instance;
    public GameObject[] SecondTwoFountain;
     
    public SkillManager sm;
    public GameObject FinalDoor;
    private void Awake()
    {
        _instance = this;
        sm = Resources.Load<SkillManager>("PlayerSkillManager");
    }
    // Use this for initialization
    void Start () {
        

    }
    public void End()
    {
        SceneManager.LoadScene("end");
    }
	public bool Check()
    {
        for (int i = 0; i < sm.PlayerSkills.Length; i++)
        {
            if (!sm.PlayerSkills[i].isOwn)
                return false;
        }
        return true;
    }
	// Update is called once per frame
	void Update () {
        FinalDoor.SetActive(!Check());

    }
}
