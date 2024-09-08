using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private Block blockPrefab;
    [SerializeField] private int blockCount = 6;

    [Space(10)]
    [SerializeField] private float spaceBetweenTwoBlocks;

    private List<Block> _blockList;

    private void Awake()
    {
        _blockList = new List<Block>(); // tạo 1 danh sách lưu các Block
    }

    void Start()
    {
        GenerateBlock(blockPrefab);
        Debug.Log("Số khối trong danh sách là " + _blockList.Count);
    }

    private void GenerateBlock(Block block)
    {
        for (int i = 0; i < blockCount; ++i)
        {
            var blockClone = Instantiate(block, this.gameObject.transform);
            
            // set position of block
            if (_blockList.Count != 0)
            {
                blockClone.transform.position -= Vector3.up * spaceBetweenTwoBlocks * i;
            }

            // set speed of block
            // if (i % 2 == 0)
            // {
            //     blockClone.SetSpeed(Random.Range(-blockClone.GetSpeed() + 0.5f, -blockClone.GetSpeed() - 0.5f));
            // }
            // else
            // {
            //     blockClone.SetSpeed(Random.Range(blockClone.GetSpeed() + 0.5f, blockClone.GetSpeed() - 0.5f));
            // }

            var direction = i % 2 == 0 ? -1 : 1; // chẵn sang trái, lẻ sang phải
            blockClone.RandomSpeed(direction);
            _blockList.Add(blockClone);
        }
    }
}