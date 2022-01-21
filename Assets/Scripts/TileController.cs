using System;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField] private Renderer tile;

    private Color _defaultColor;
    private Color _altColor;

    private TileMovementSystem _movementSystem;

    private void Start()
    {
        _defaultColor = tile.material.color;
        _altColor = new Color(_defaultColor.r + 0.3f, _defaultColor.g + 0.3f, _defaultColor.b + 0.3f);
    }

    private void OnMouseEnter()
    {
        tile.material.color = _altColor;
    }
    
    private void OnMouseExit()
    {
        tile.material.color = _defaultColor;
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
