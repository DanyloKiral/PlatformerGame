using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    public GameObject block;
    public GameObject centerBlock;

    public GameObject leftHillBlock;
    public GameObject rightHillBlock;
    public GameObject leftHillCornerBlock;
    public GameObject rightHillCornerBlock;

    public GameObject bronseCoin;
    public GameObject silverCoin;
    public GameObject goldCoin;

    public int BlocksPerLevel = 200;

    private int RandomFrom = 3;
    private int RandomTo = 5;

    public int StreighnessOfLevel = 8;

    private float x;
    private float y;

    private List<GameObject> groundPieces = new List<GameObject>();

    public Vector3 step;

    public void Start()
    {
        step = new Vector3(block.GetComponent<BoxCollider2D>().size.x, block.GetComponent<BoxCollider2D>().size.y, 0);
        GroundGeneration();
        FillingWithCoins();
    }

    private void FillingWithCoins()
    {
        bool InGroup = false;
        int GroupNumb = 0;
        foreach (var obj in groundPieces)
        {
            if (InGroup)
            {
                if (GroupNumb <= 0)
                    InGroup = false;
                else
                {
                    GroupNumb--;
                    Instantiate(bronseCoin, new Vector3(obj.transform.position.x, obj.transform.position.y + step.y, 1), Quaternion.identity);
                }
            }
            if (Random.Range(0, 10) > 8 && !InGroup)
            {
                GroupNumb = Random.Range(3, 6);
                Instantiate(bronseCoin, new Vector3(obj.transform.position.x, obj.transform.position.y + step.y, 1), Quaternion.identity);
                InGroup = true;
            }            
        }
    }

    private void GroundGeneration()
    {
        y = CameraScript.leftBottom.y;
        x = CameraScript.leftBottom.x;
        int? previous = null;
        for (int i = 0; i < BlocksPerLevel; i++)
        {
            switch (Random.Range(0, StreighnessOfLevel))
            {
                case 0:     //up                    
                    if (previous == null || previous == 0)
                    {                        
                        previous = UpFloor();
                    }
                    else
                    {
                        previous = RightFloor();
                    }
                    break;
                case 1:     //down                   
                    if (previous == null || previous == 1)
                    {
                        previous = DownFloor();   
                    }
                    else
                    {
                        previous = RightFloor();
                    }
                    break;
                default:
                    previous = RightFloor();
                    break;
            }
        }
    }

    private int? UpFloor()
    {
        for (int i = 0; i < Random.Range(RandomFrom, RandomTo); i++, x += step.x)
        {
            y += step.y;
            groundPieces.Add(Instantiate(rightHillBlock, new Vector3(x, y, 1), Quaternion.identity) as GameObject);
            Instantiate(rightHillCornerBlock, new Vector3(x, y - step.y, 1), Quaternion.identity);
            FillingWithCenterBlocks(x, y - step.y);
        }
        return 0;
    }

    private int? DownFloor()
    {
        for (int i = 0; i < Random.Range(RandomFrom, RandomTo); i++, x += step.x)
        {
            groundPieces.Add(Instantiate(leftHillBlock, new Vector3(x, y, 1), Quaternion.identity) as GameObject);
            Instantiate(leftHillCornerBlock, new Vector3(x, y - step.y, 1), Quaternion.identity);
            FillingWithCenterBlocks(x, y - step.y);
            y -= step.y;
        }
        return 1;
    }

    private int? RightFloor()
    {
        for (int i = 0; i < Random.Range(RandomFrom, RandomTo); i++, x += step.x)
        {
            groundPieces.Add(Instantiate(block, new Vector3(x, y, 1), Quaternion.identity) as GameObject);
            FillingWithCenterBlocks(x, y);
        }
        return null;
    }

    private void FillingWithCenterBlocks(float x, float y)
    {
        for (float yTemp = y - step.y; yTemp > y - (CameraScript.leftTop.y - CameraScript.leftBottom.y + step.y*2); yTemp -= step.y)
        {
            Instantiate(centerBlock, new Vector3(x, yTemp, 1), Quaternion.identity);
        }
    }
}
