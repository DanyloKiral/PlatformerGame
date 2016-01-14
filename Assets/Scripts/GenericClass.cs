using UnityEngine;
using System.Collections;

public class GenericClass : MonoBehaviour {

	// Use this for initialization
	void Start() {
        transform.localScale = new Vector3(Constants.instance.Scale, Constants.instance.Scale, 1);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
