using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtr : MonoBehaviour {
    public float _speed;
    Transform Target;
    Vector3 offset;
	// Use this for initialization
	void Start () {
        Target = GameObject.FindGameObjectWithTag("Player").transform;

        offset = transform.position - Target.transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position,  Target.transform.position+offset,Time.deltaTime*_speed);
	}
}
