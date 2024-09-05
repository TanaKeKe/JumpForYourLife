using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayPanel : MonoBehaviour
{
    [SerializeField] GameObject wallTinyLeft;
    [SerializeField] GameObject wallTinyRight;
    [SerializeField] GameObject wallLeft;
    [SerializeField] GameObject wallRight;
    private float positionYOfWall;
    void Start()
    {
        positionYOfWall = wallTinyRight.transform.position.y;
        GenerateWall(wallTinyLeft,wallTinyRight);

    }

    private void GenerateWall(GameObject wallTinyLeft, GameObject wallTinyRight)
    {
        
        for ( int i=1;i<=8;++i)
        {
            
            var wallTinyLeftClone = Instantiate(wallTinyLeft, wallLeft.transform);
            wallTinyLeftClone.transform.position = new Vector3(wallTinyLeftClone.transform.position.x, positionYOfWall, wallTinyLeftClone.transform.position.z);
            var wallTinyRightClone = Instantiate(wallTinyRight, wallRight.transform);
            wallTinyRightClone.transform.position = new Vector3(wallTinyRightClone.transform.position.x, positionYOfWall, wallTinyRightClone.transform.position.z);
            positionYOfWall -= 1.5f;
        }
    }
}
