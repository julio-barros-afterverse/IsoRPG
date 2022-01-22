using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.WSA;

public class TileMovementSystem : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    public void OnMoveToTile(TileController tileController)
    {
        characterController.Move2D(tileController.transform.position);
    }
}
