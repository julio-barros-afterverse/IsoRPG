using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private TileMovementSystem movementSystem;
    private TileController[][] _tileArray;
    void Start()
    {
        _tileArray = new TileController[8][];
        for (int x = 0; x < 8; x++)
        {
            _tileArray[x] = new TileController[8];
            for (int z = 0; z < 8; z++)
            {
                var posX = x * 1.8f + (z % 2 == 0 ? 0f : 0.9f); 
                var currentTile = 
                    Instantiate(tile, new Vector3(posX, 0, z*1.57f), Quaternion.identity, transform);
                currentTile.name = $"tile ({x}, {z})";
                var tileController = currentTile.GetComponentInChildren<TileController>();
                _tileArray[x][z] = tileController;
                tileController.RegisterMovementSystem(movementSystem);
            }
        }
    }
}
