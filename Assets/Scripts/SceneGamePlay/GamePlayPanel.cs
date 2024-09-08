using UnityEngine;

public class GamePlayPanel : MonoBehaviour
{
    [SerializeField] private GameObject wallTinyLeftPrefab;
    [SerializeField] private GameObject wallTinyRightPrefab;
    [SerializeField] private Transform wallLeft;
    [SerializeField] private Transform wallRight;
    [SerializeField] private int wallCount = 15;

    private float _wallPosition;

    private void Start()
    {
        _wallPosition = wallTinyRightPrefab.transform.position.y;
        GenerateWall(wallTinyLeftPrefab, wallTinyRightPrefab);
    }

    private void GenerateWall(GameObject wallTinyLeft, GameObject wallTinyRight)
    {
        for (int i = 1; i <= wallCount; ++i)
        {
            var wallTinyLeftClone = Instantiate(wallTinyLeft, wallLeft);
            var wallTinyRightClone = Instantiate(wallTinyRight, wallRight);

            var wallPos = wallTinyLeftClone.transform.position;
            wallPos.y = _wallPosition;
            wallTinyLeftClone.transform.position = wallPos;

            wallPos.x *= -1;
            wallTinyRightClone.transform.position = wallPos;

            _wallPosition -= 1.5f;
        }
    }
}