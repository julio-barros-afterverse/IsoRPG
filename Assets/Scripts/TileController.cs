using System;
using Model;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField] private Renderer tile;

    private Color _defaultColor;
    private Color _altColor;

    private TileMovementSystem _movementSystem;

    public Hex Position;

    private void Start()
    {
        _defaultColor = tile.material.color;
        _altColor = new Color(1f - _defaultColor.r, 1f - _defaultColor.g, 1f -  _defaultColor.b);
    }

    public void SetOnPath(bool onPath)
    {
        tile.material.color = onPath ? _altColor : _defaultColor;
    }

    private void OnMouseEnter()
    {
        _movementSystem.OnHoverTile(this);
    }
    
    private void OnMouseExit()
    {
        _movementSystem.OnUnhoverTile(this);
    }

    private void OnMouseUp()
    {
        if (_movementSystem == null) return;
        _movementSystem.OnMoveToTile(this);
    }

    public void RegisterMovementSystem(TileMovementSystem s)
    {
        _movementSystem = s;
    }
}
