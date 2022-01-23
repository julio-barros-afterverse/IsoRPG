using System;
using MainGameState;
using Model;
using UnityEngine;

public class TileSystem : MonoBehaviour
{
    [SerializeField] private Renderer tile;

    private Color _defaultColor;
    private Color _altColor;

    private MainGameStateMachine _stateMachine;

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
        _stateMachine.CurrentState.OnHoverTile(this);
    }
    
    private void OnMouseExit()
    {
        _stateMachine.CurrentState.OnUnhoverTile(this);
    }

    private void OnMouseUp()
    {
        if (_stateMachine.CurrentState == null) return;
        _stateMachine.CurrentState.OnSelectTile(this);
    }

    public void RegisterMainGameStateMachine(MainGameStateMachine s)
    {
        _stateMachine = s;
    }
}
