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
    private int _count = 0;
    private void Start()
    {
        _blockPool = GetObjectPool();
        Messenger.Broadcast(EventKey.SetSkinBlock, blockPrefab);
        GenerateBlock(blockPrefab);
        Debug.Log("Sinh khối đứng thành công: " + AmountObjectInPool());
    }

    private void OnEnable()
    {
        Messenger.AddListener(EventKey.SetStatusBlockWhenPlay, SetStatusBlock);
    }
    private void OnDisable()
    {
        Messenger.RemoveListener(EventKey.SetStatusBlockWhenPlay, SetStatusBlock);
    }

    private void SetStatusBlock()
    {
        foreach (var block in _blockPool)
        {
            var obj = block.GetComponent<BlockEvent>().isBreak;
            if (obj == true)
            {
                block.GetComponent<Block>()._collider2D.isTrigger = true;
            }
        }
    }

    private void Update()
    {
        if (GamePlayController.Instance.isStart == false)
        {
            TutorialPlay();
        }

        if(GamePlayController.Instance.isPlaying == true)
        {
            if(_count == 0)
            {
                _blockPool[0].GetComponent<Block>().SetSpeed(1.2f);
                ++_count;
            }
        }
        GetObjectFromPool();
        CheckOutCameraToResetPositionObject(spaceBetweenTwoBlocks);
    }

    private void TutorialPlay()
    {
        _blockPool[0].GetComponent<BlockEvent>().isBreak = true;
        _blockPool[0].GetComponent<Block>().SetNoneSpeed();
    }

    private void GenerateBlock(GameObject block)
    {
        for (int i = 0; i < blockCount; ++i)
        {
            var blockClone = Instantiate(block, this.gameObject.transform);
            blockClone.transform.position -= Vector3.up * spaceBetweenTwoBlocks * i; // set position of block
            //Debug.Log(blockClone.transform.position);
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