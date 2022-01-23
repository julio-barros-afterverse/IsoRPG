using System.Collections.Generic;
using Model;
using UnityEngine;

public class ActionSystem : MonoBehaviour
{
    [SerializeField] private List<IActionPerformer> _performers;

    void PerformAction(IAction action)
    {
        foreach (var performer in _performers)
        {
            if (performer.Supports(action)) { performer.Perform(action); }
        }
    }
}
