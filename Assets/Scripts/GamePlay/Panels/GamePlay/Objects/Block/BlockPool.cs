using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockPool : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private int blockCount;
    [SerializeField] private float spaceBetweenTwoBlocks;
    [SerializeField] private int numberOpenMediumBlock;
    [SerializeField] private int numberOpenHardBlock;

    private int _endIndex;
    private List<GameObject> _blockPool;
    private float _positionLowest;
    private int _count = 0;

    private void Start()
    {
        _blockPool = new List<GameObject>();
        _positionLowest = -14.5f;
        GenerateBlock(blockPrefab);
        //Debug.Log("Sinh khối đứng thành công: " + AmountObjectInPool());
    }

    private void OnEnable()
    {
        Messenger.AddListener(EventKey.GetBlockFormPool, GetBlockFromPool);
        Messenger.AddListener(EventKey.SetStatusBlockWhenPlay, SetStatusBlock);
    }
    private void OnDisable()
    {
        Messenger.RemoveListener(EventKey.GetBlockFormPool, GetBlockFromPool);
        Messenger.RemoveListener(EventKey.SetStatusBlockWhenPlay, SetStatusBlock);
    }

    public void GetBlockFromPool()
    {
        float rangeTopCamera = GamePlayController.Instance.GetRangeTopCamera();
        float rangeBottomCamera = GamePlayController.Instance.GetRangeBottomCamera();
        foreach (GameObject obj in _blockPool)
        {
            if (obj != null)
            {
                if (obj.transform.position.y <= rangeTopCamera && obj.transform.position.y >= rangeBottomCamera)
                {
                    obj.SetActive(true);
                }
                else
                {
                    if (GamePlayController.Instance.isPlaying == true)
                    {
                        SetTypeBlock(obj);
                    }
                }
            }
        }
    }

    private void SetTypeBlock(GameObject obj)
    {
        SetAmountTypeBlockUsing();
        int index = (int)Random.Range(0, _endIndex);
        TypeBlock type = (TypeBlock)index;
        Block block = obj.GetComponent<Block>();
        // BlockEvent blockEvent = obj.GetComponent<BlockEvent>();
        BlockEvent blockEvent = block.BlockEvent;
        blockEvent.typeBlock = type;

        if (type == TypeBlock.Normal)
        {
            blockEvent.LoadSkinNormalBlock();
            blockEvent.SetColliderNormalBlock();
        }

        if (type == TypeBlock.Medium)
        {
            blockEvent.LoadSkinMediumBlock();
            blockEvent.SetColliderMediumBlock();
        }

        if (type == TypeBlock.Speed)
        {
            block.BoostSpeed();
        }

        if (type == TypeBlock.Hidden)
        {
            blockEvent.SetHiddenBlock();
        }

        if (type == TypeBlock.Oblique)
        {
            block.SetAngle();
        }

        if (type == TypeBlock.Hard)
        {
            blockEvent.LoadSkinHardBlock();
            blockEvent.SetColliderHardBlock();
        }

        if (type == TypeBlock.Normal || type == TypeBlock.Medium || type == TypeBlock.Hard)
        {
            int randomNumber = (int)Random.Range(0, 2);
            if (randomNumber == 0) blockEvent.SetDoKill();
            else block.SetNoneAngle();
        }

        blockEvent.SetOriginBlock();

    }

    private void SetAmountTypeBlockUsing()
    {
        if (GamePlayController.Instance.score < numberOpenMediumBlock) _endIndex = 1;
        else
        {
            if (GamePlayController.Instance.score < numberOpenHardBlock) _endIndex = 5;
            else _endIndex = 6;
        }
    }

    private void AddBlockToPool(GameObject obj)
    {
        _blockPool.Add(obj);
    }

    private int AmountBlockInPool()
    {
        return _blockPool.Count;
    }

    private void Update()
    {
        TutorialPlay();
        SetSpeedFirstBlockWhenPlay();
        CheckOutCameraToResetPositionBlock();
    }

    private void SetSpeedFirstBlockWhenPlay()
    {
        if (GamePlayController.Instance.isPlaying == true)
        {
            if (_count == 0)
            {
                _blockPool[0].GetComponent<Block>().SetSpeed(1.3f);
                ++_count;
            }
        }
    }

    private void CheckOutCameraToResetPositionBlock()
    {
        float rangeTopCamera = GamePlayController.Instance.GetRangeTopCamera(); // lấy khoảng trên của camera
        foreach (GameObject obj in _blockPool) // tìm những object ở ngoài camera về phía trên
        {
            if (obj != null)
            {
                if (obj.transform.position.y > rangeTopCamera)
                {
                    ResetPositionObject(obj);
                }
            }
        }
    }

    private void ResetPositionObject(GameObject obj)
    {
        _positionLowest -= spaceBetweenTwoBlocks;
        //Debug.Log(_positionLowest);
        Vector3 position = obj.transform.position;
        position.y = _positionLowest;
        obj.transform.position = position; // đặt lại vị trí cho object
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

    private void TutorialPlay()
    {
        if (GamePlayController.Instance.isStart == false)
        {
            _blockPool[0].GetComponent<BlockEvent>().isBreak = true;
            _blockPool[0].GetComponent<Block>().SetNoneSpeed();
        }
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

            AddBlockToPool(blockClone);
        }
    }
}