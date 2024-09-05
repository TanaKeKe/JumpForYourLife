using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayPanel : MonoBehaviour
{
    [SerializeField] private UnityEngine.GameObject wallTinyLeft;
    [SerializeField] private UnityEngine.GameObject wallTinyRight;
    [SerializeField] private UnityEngine.GameObject wallLeft;
    [SerializeField] private UnityEngine.GameObject wallRight;

    [Space(10)]

    [SerializeField] private GameObject block;
    [SerializeField] private UnityEngine.GameObject blocks;
    private List<GameObject> _blockList;
    private float _positionYOfWall;
    private float _positionYOfBlock;
    void Start()
    {
        _positionYOfWall = wallTinyRight.transform.position.y;
        _positionYOfBlock = block.transform.position.y;
        GenerateWall(wallTinyLeft,wallTinyRight);
        GenerateBlock(block);
        
    }

    

    private void GenerateBlock(GameObject block)
    {
        for(int i =1;i<=8;++i)
        {
            var blockClone = Instantiate(block, blocks.transform);
            _blockList.Add(blockClone);
        }

        
    }

    private void GenerateWall(UnityEngine.GameObject wallTinyLeft, UnityEngine.GameObject wallTinyRight)
    {
        
        for ( int i=1;i<=15;++i)
        {
            
            var wallTinyLeftClone = Instantiate(wallTinyLeft, wallLeft.transform);
            wallTinyLeftClone.transform.position = new Vector3(wallTinyLeftClone.transform.position.x, _positionYOfWall, wallTinyLeftClone.transform.position.z);
            var wallTinyRightClone = Instantiate(wallTinyRight, wallRight.transform);
            wallTinyRightClone.transform.position = new Vector3(wallTinyRightClone.transform.position.x, _positionYOfWall, wallTinyRightClone.transform.position.z);
            _positionYOfWall -= 1.5f;
        }
    }
}
