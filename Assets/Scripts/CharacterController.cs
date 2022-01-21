using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Vector3 _targetPosition = Vector3.zero;
    private Vector3 _sourcePosition = Vector3.zero;
    private float _animationProgress = 1f;
    private float _animationDuration = 1f;
    public float Speed = 3f;
    [SerializeField] private Animator _animator;
    private static readonly int Walking = Animator.StringToHash("Walking");

    public void Move2D(Vector3 position)
    {
        _sourcePosition = transform.position;
        _targetPosition = new Vector3(position.x, _sourcePosition.y, position.z);
        transform.LookAt(_targetPosition);
        _animationProgress = 0f;
        _animationDuration = Vector3.Distance(_targetPosition, _sourcePosition) / Speed;
        _animator.SetBool(Walking, true);
    }

    private void Update()
    {
        if (_animationProgress >= 1f) return;
        _animationProgress += Time.deltaTime / _animationDuration;
        transform.position = Vector3.Lerp(_sourcePosition, _targetPosition, Mathf.Clamp01(_animationProgress));
        
        if (_animationProgress >= 1f) _animator.SetBool(Walking, false);
    }
}
