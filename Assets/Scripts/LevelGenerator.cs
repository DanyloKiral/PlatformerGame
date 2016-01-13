using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

    public float height;

    public void Init()
    {
        height = Camera.main.orthographicSize * 2;
    }

    public void Generate()
    {
        Init();
        print(height);
    }
}
