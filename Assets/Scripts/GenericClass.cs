using UnityEngine;
using System.Collections;

public class GenericClass : MonoBehaviour {

	// Use this for initialization
	void Start() {
        GameManager.instance.sceneObjects.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
