using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerSystem : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Queue<Vector3> _path = new Queue<Vector3>();
    private Vector3 _targetPosition = Vector3.zero;
    private Vector3 _sourcePosition = Vector3.zero;
    private float _animationProgress = 1f;
    private float _animationDuration = 1f;
    public float Speed = 3f;

    private static readonly int Walking = Animator.StringToHash("Walking");

    public TileSystem CurrentTile;
    private static readonly int Lightning1 = Animator.StringToHash("Lightning");

    public void Move2D(Vector3 position)
    {
        transform.position = new Vector3(position.x, _sourcePosition.y, position.z);
        _animationProgress = 1f;
    }
    public void Move2D(Queue<Vector3> path)
    {
        _path = path;
    }

    private void RestartInterpolation()
    {
        if (_path.Count == 0)
        {
            return;
        }

        _targetPosition = _path.Dequeue();
        _sourcePosition = transform.position;
        transform.LookAt(_targetPosition);
        _animationProgress = 0f;
        _animationDuration = Vector3.Distance(_targetPosition, _sourcePosition) / Speed;
        _animator.SetBool(Walking, true);
    }

    private void Update()
    {
        if (_animationProgress >= 1f)
        {
            if (_path.Count > 0) { RestartInterpolation(); }
            else { return; }
        }
        _animationProgress += Time.deltaTime / _animationDuration;
        transform.position = Vector3.Lerp(_sourcePosition, _targetPosition, Mathf.Clamp01(_animationProgress));
        
        if (_animationProgress >= 1f) _animator.SetBool(Walking, false);
    }

    public void Lightning()
    {
        _animator.SetTrigger(Lightning1);
    }
}
