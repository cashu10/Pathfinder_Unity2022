using System.Collections.Generic;
using UnityEngine;

public class DirectionBarHandler : MonoBehaviour
{
    [SerializeField] GameEvent_directions OnNextDirection;
    private List<Directions> _directions;
    private int _totalDirections;
    private int _currentDirection = 0;
    public void SetDirectionsList()
    {
        _directions = new List<Directions>();
        IControlDirection[] directionControllers = gameObject.GetComponentsInChildren<IControlDirection>();
        _totalDirections =  directionControllers.Length;
        foreach(var dc in directionControllers)
        {
            _directions.Add(dc.GetDirection());
        }
        _currentDirection = 0;
        NextDirection();
    }

    public void NextDirection()
    {
        Directions returnDirection = Directions.ERROR;
        if (_currentDirection < _totalDirections)
        {
            returnDirection = _directions[_currentDirection];
            _currentDirection++;
        }
        RaiseNextDirection(returnDirection);
    }

    public void RaiseNextDirection(Directions direction)
    {
        OnNextDirection.Raise(direction);
    }
}
