using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockController : MonoBehaviour
{
    [SerializeField] private GameObject block;
    

    [Space(10)]
    [SerializeField] private float spaceBetweenTwoBlocks;
    private List<GameObject> _blockList;

    private void Awake()
    {
        _blockList = new List<GameObject>(); // tạo 1 danh sách lưu các Block
        
    }
    void Start()
    {
        GenerateBlock(block);
        Debug.Log("Số khối trong danh sách là " + _blockList.Count);
    }


    private void GenerateBlock(GameObject block)
    {
        for (int i = 0; i < 6; ++i)
        {
            var blockClone = Instantiate(block, this.gameObject.transform);
            // set position of block
            if(_blockList.Count != 0 )
            {
                blockClone.transform.position -= Vector3.up * spaceBetweenTwoBlocks * i;
            }
            // set speed of block
            if(i%2 == 0)
            {
                blockClone.SetSpeed(Random.Range(-blockClone.GetSpeed()+0.5f, -blockClone.GetSpeed() - 0.5f));
            }
            else
            {
                blockClone.SetSpeed(Random.Range(blockClone.GetSpeed() + 0.5f, blockClone.GetSpeed() - 0.5f));
            }
            _blockList.Add(blockClone);
        }


    }

    
}
