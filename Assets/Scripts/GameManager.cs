using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public List<Component> sceneObjects = new List<Component>();

    public GameObject player;

    private bool scaleDone = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        
    }

    // Update is called once per frame
    void Update () {
        if (!scaleDone)
        {
            new LevelGenerator().Generate();
            foreach (var one in sceneObjects)
            {
                one.transform.localScale = new Vector3(Constants.instance.Scale, Constants.instance.Scale, 1);
            }
            scaleDone = true;
        }
    }
}
