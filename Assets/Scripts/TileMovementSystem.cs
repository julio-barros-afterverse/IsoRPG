using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.WSA;

public class TileMovementSystem : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject board;
    private TileController[][] _tileArray;

    private void Start()
    {
        _tileArray = new TileController[100][];
        var tiles = board.GetComponentsInChildren<TileController>();
        foreach (var tile in tiles)
        {
            var position = tile.transform.position;
            var x = Mathf.RoundToInt(position.x / 0.9f);
            var z = Mathf.RoundToInt(position.z / 1.57f);
            _tileArray[x] = new TileController[100];
            _tileArray[x][z] = tile;
            tile.RegisterMovementSystem(this);
        }
    }

    public void OnMoveToTile(TileController tileController)
    {
        characterController.Move2D(tileController.transform.position);
    }
}
