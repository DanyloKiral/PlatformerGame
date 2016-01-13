using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {

    public static Constants instance = null;

    public float Scale = 1;

    public int goldCoinPoints = 10;
    public int silverCoinPoints = 5;
    public int bronseCoinPoints = 1;
    public AudioClip pickCoinSound;

    void Start()
    {
        Scale = Screen.height / 1.5f;
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
