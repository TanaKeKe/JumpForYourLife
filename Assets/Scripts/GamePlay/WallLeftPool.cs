using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLeftPool : ObjectPool
{
    [SerializeField] private GameObject wallTinyLeftPrefab;
    [SerializeField] private Transform wallLeft;
    [SerializeField] private int wallCount;
    [SerializeField] private float distanceWallSpawn;
    [SerializeField] private float lengthWall;
    private float _wallPosition;

    private void Start()
    {
        _wallPosition = wallTinyLeftPrefab.transform.position.y;
        GenerateWall(wallTinyLeftPrefab);
        Debug.Log("Sinh tường thành công: " + AmountObjectInPool());
    }

    private void Update()
    {
        GetObjectFromPool(lengthWall);
        CheckOutCameraToResetPositionObject(lengthWall, distanceWallSpawn);
    }

    private void GenerateWall(GameObject wallTinyLeft)
    {
        for (int i = 1; i <= wallCount; ++i)
        {
            var wallTinyLeftClone = Instantiate(wallTinyLeft, wallLeft);

            var wallPos = wallTinyLeftClone.transform.position;
            wallPos.y = _wallPosition;
            wallTinyLeftClone.transform.position = wallPos;

            AddObjectToPool(wallTinyLeftClone);
            _wallPosition -= distanceWallSpawn;
        }
    }
}