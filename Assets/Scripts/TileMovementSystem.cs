using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class TileMovementSystem : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private BoardController boardController;
    private TileController _currentTile;

    private void Awake()
    {
        boardController.OnFinished(() =>
        {
            foreach (var coord in boardController.Coordinates.Values)
            {
                coord.RegisterMovementSystem(this);
            }

            var zeroTile = boardController.Coordinates[new Hex(0, 0)];
            characterController.Move2D(zeroTile.transform.position);
            _currentTile = zeroTile;
        });
    }

    public void OnMoveToTile(TileController tile)
    {
        var distance = Hex.Distance(_currentTile.Position, tile.Position);
        if (distance <= 0)
        {
            return;
        }

        Queue<Vector3> path = new Queue<Vector3>();
        for (int i = 0; i <= distance; i++)
        {
            var coord = Hex.Lerp(_currentTile.Position, tile.Position, (1.0f / distance) * i);
            var point = boardController.Coordinates[coord];
            point.SetOnPath(true);
            path.Enqueue(point.transform.position);
        }
        characterController.Move2D(path);
        _currentTile = tile;
    }

    public void OnHoverTile(TileController tile)
    {
        // draw line to tile
        var distance = Hex.Distance(_currentTile.Position, tile.Position);
        if (distance <= 0)
        {
            return;
        }
        for (int i = 0; i <= distance; i++)
        {
            var coord = Hex.Lerp(_currentTile.Position, tile.Position, (1.0f / distance) * i);
            var point = boardController.Coordinates[coord];
            point.SetOnPath(true);
        }
    }
    
    public void OnUnhoverTile(TileController tile)
    {
        foreach (var coord in boardController.Coordinates.Values)
        {
            coord.SetOnPath(false);
        }
    }
}
