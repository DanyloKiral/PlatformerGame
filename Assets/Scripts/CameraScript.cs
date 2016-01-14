using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraScript : MonoBehaviour {

    private Transform target;

    public static Vector3 leftTop;
    public static Vector3 leftBottom;
    public static Vector3 rightTop;
    public static Vector3 rightBottom;

    void  Awake()
    {
        leftBottom = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0f, 0f, 1));
        leftTop = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0f, GetComponent<Camera>().pixelHeight, 1));
        rightTop = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(GetComponent<Camera>().pixelWidth, GetComponent<Camera>().pixelHeight, 1));
        rightBottom = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(GetComponent<Camera>().pixelWidth, 0f, 1));
      //  DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
        target = GameObject.FindGameObjectWithTag("Player").transform;
    //    Camera.main.orthographicSize *= Constants.instance.Scale;
       // Camera.main.GetComponentInChildren<Component>().transform.localScale = new Vector3(Constants.instance.Scale, Constants.instance.Scale, 1);    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -10);
    }

    private void OnDrawGizmos()
    {
    /*    Vector3 p1 = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0f, 0f, 1));
        Vector3 p2 = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0f, GetComponent<Camera>().pixelHeight, 1));
        Vector3 p3 = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(GetComponent<Camera>().pixelWidth, GetComponent<Camera>().pixelHeight, 1));
        Vector3 p4 = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(GetComponent<Camera>().pixelWidth, 0f, 1));
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);
        leftBottom = p1;
        leftTop = p2;
        rightTop = p3;
        rightBottom = p4;*/
    }
}
