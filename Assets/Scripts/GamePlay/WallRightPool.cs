using UnityEngine;

public class WallRightPool : ObjectPool
{
    [SerializeField] private GameObject wallTinyRightPrefab;
    [SerializeField] private Transform wallRight;
    [SerializeField] private int wallCount;
    [SerializeField] private float distanceWallSpawn;
    [SerializeField] private float lengthWall;
    private float _wallPosition;

    private void Start()
    {
        _wallPosition = wallTinyRightPrefab.transform.position.y;
        GenerateWall(wallTinyRightPrefab);
        Debug.Log("Sinh tường thành công: " + AmountObjectInPool());
    }

    private void Update()
    {
        GetObjectFromPool(lengthWall);
        CheckOutCameraToResetPositionObject(lengthWall, distanceWallSpawn);
    }

    private void GenerateWall(GameObject wallTinyRight)
    {
        for (int i = 1; i <= wallCount; ++i)
        {
            var wallTinyRightClone = Instantiate(wallTinyRight, wallRight);

            var wallPos = wallTinyRightClone.transform.position;
            wallPos.y = _wallPosition;
            wallTinyRightClone.transform.position = wallPos;

            AddObjectToPool(wallTinyRightClone);
            _wallPosition -= distanceWallSpawn;
        }
    }
}