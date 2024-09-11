using System.Collections.Generic;
using UnityEngine;

public class BlockPool : ObjectPool
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private int blockCount;

    [Space(10)]
    [SerializeField] private float spaceBetweenTwoBlocks;
    [SerializeField] private float lengthBlock;
    
    private void Start()
    {
        GenerateBlock(blockPrefab);
        Debug.Log("Sinh khối đứng thành công: " + AmountObjectInPool());
        
    }

    private void Update()
    {
        GetObjectFromPool(lengthBlock);
        CheckOutCameraToResetPositionObject(lengthBlock,spaceBetweenTwoBlocks);
    }
    private void GenerateBlock(GameObject block)
    {
        for (int i = 0; i < blockCount; ++i)
        {
            var blockClone = Instantiate(block, this.gameObject.transform);
            blockClone.transform.position -= Vector3.up * spaceBetweenTwoBlocks * i;// set position of block
            var direction = i % 2 == 0 ? -1 : 1; // chẵn sang trái, lẻ sang phải
            blockClone.GetComponent<Block>().RandomSpeed(direction);

            AddObjectToPool(blockClone);
        }
    }

}