using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private GameObject block;
    [SerializeField] private UnityEngine.GameObject blocks;

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

    private void Update()
    {
        
    }

    private void GenerateBlock(GameObject block)
    {
        for (int i = 1; i <= 8; ++i)
        {
            var blockClone = Instantiate(block, blocks.transform);
            blockClone.setActive(false);
            _blockList.Add(blockClone);
        }


    }
}
