using System;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class BoardSystem : MonoBehaviour
{
    [SerializeField] private GameObject tile;

    private TileSystem[][] _tileArray;
    private readonly Dictionary<Hex, TileSystem> _coordinates = new Dictionary<Hex, TileSystem>();
    public Dictionary<Hex, TileSystem> Coordinates => _coordinates;
    private event System.Action ONFinished;

    void Start()
    {
        for (int r = -3; r <= 3; r++)
        {
            for (int q = Math.Max((-3-r), -3); q <= Math.Min(-r+3, 3); q++)
            {
                var coord = new Hex(r, q);
                var posZ = coord.r * 1.57f;
                var coordX = (coord.q + (coord.r - (coord.r & 1)) / 2);
                var posX = coordX * 1.8f + Mathf.Abs(coord.r & 1) * 0.9f;
                var currentTile = 
                    Instantiate(tile, new Vector3(posX, 0, -posZ), Quaternion.identity, transform);
                currentTile.name = $"tile (q={q}, r={r})";
                var tileController = currentTile.GetComponentInChildren<TileSystem>();
                _coordinates[coord] = tileController;
                tileController.Position = coord;
            }
        }
        ONFinished?.Invoke();
        ONFinished = null;
    }

    public void OnFinished(System.Action action)
    {
        ONFinished += action;
    }
}
