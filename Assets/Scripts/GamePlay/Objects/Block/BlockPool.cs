using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockPool : ObjectPool
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private int blockCount;
    [Space(10)]

    [SerializeField] private float spaceBetweenTwoBlocks;

    private List<GameObject> _blockPool;
    private bool _checkStart;

    private void Start()
    {
        _blockPool = GetObjectPool();
        Messenger.Broadcast(EventKey.SetSkinBlock, blockPrefab);
        GenerateBlock(blockPrefab);
        Debug.Log("Sinh khối đứng thành công: " + AmountObjectInPool());
    }

    private void Update()
    {
        GetObjectFromPool();
        TutorialPlay();
        CheckOutCameraToResetPositionObject(spaceBetweenTwoBlocks);
        
    }

    private void TutorialPlay()
    {
        if (GamePlayController.Instance.isPlaying && !_checkStart)
        {
            _checkStart = true;
            _blockPool[0].GetComponent<Block>().SetSpeed(1.2f);
        }
        else
        {
            if (!_checkStart) _blockPool[0].GetComponent<Block>().SetNoneSpeed();
        }
    }

    private void GenerateBlock(GameObject block)
    {
        for (int i = 0; i < blockCount; ++i)
        {
            var blockClone = Instantiate(block, this.gameObject.transform);
            blockClone.transform.position -= Vector3.up * spaceBetweenTwoBlocks * i; // set position of block
            Debug.Log(blockClone.transform.position);
            var direction = i % 2 == 0 ? -1 : 1; // chẵn sang trái, lẻ sang phải
            blockClone.GetComponent<Block>().RandomSpeed(direction);

            AddObjectToPool(blockClone);
        }
    }

    public float GetSpaceBetweenTwoBlocks()
    {
        return spaceBetweenTwoBlocks;
    }
}