using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    private Transform target;

    void Start()
    {
        target = GameManager.instance.player.transform;
        Camera.main.transform.localScale = new Vector3(1, 1, 1);
        Camera.main.orthographicSize *= Constants.instance.Scale;
        Camera.main.GetComponentInChildren<Component>().transform.localScale = new Vector3(Constants.instance.Scale, Constants.instance.Scale, 1);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -10);
    }
}
