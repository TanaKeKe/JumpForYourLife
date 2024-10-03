using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawn : MonoBehaviour
{
    [SerializeField] private GameObject walls;
    [SerializeField] private GameObject wallTinyRightPrefab;
    [SerializeField] private Transform wallRight;
    [SerializeField] private GameObject wallTinyLeftPrefab;
    [SerializeField] private Transform wallLeft;
    [Space(10)]

    [SerializeField] private int wallCount;
    private void Start()
    {
        GenerateWall(wallTinyRightPrefab, wallTinyLeftPrefab);
        walls.transform.SetParent(GamePlayController.Instance.GetCamera().transform);
    }

    private void GenerateWall(GameObject wallTinyRight, GameObject wallTinyLeft)
    {
        for (int i = 1; i <= wallCount; ++i)
        {
            var wallTinyLeftClone = Instantiate(wallTinyLeft, wallLeft);
            var wallTinyRightClone = Instantiate(wallTinyRight, wallRight);
        }
    }
}
