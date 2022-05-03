using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum GridStates
{
    START,
    NOT_PATH,
    PATH,
    BARRIER,
    FINISH,
}

public class GridHandler
{
    [SerializeField] private int _height = 4;
    [SerializeField] private int _width = 4;
    [SerializeField] private int _numTurns = 2;
    [SerializeField] private int _numPOI = 3;
    private GridStates[,] _coordinateStates;
    public GridStates[,] CoordinateStates {get{return _coordinateStates;}}
    private List<int> _barrierX = new List<int>();
    private List<Vector2Int> _path = new List<Vector2Int>();
    private List<Vector2Int> _POI = new List<Vector2Int>();
    private int _start;
    private int _finish;

    public GridHandler(int width, int height, int maxTurns, int numPOI)
    {
        _height = height;
        _width = width;
        _numTurns = maxTurns;
        _numPOI = numPOI;

        _coordinateStates = new GridStates[width, height];

        SetInitialStates();
        SetStartPosition();
        SetFinishPosition();
        GetBarriersX();
        SetBarriers();
        SetPath();
        SetPOI();
    }

    private void SetInitialStates()
    {
        for (int x = 0; x < _coordinateStates.GetLength(0); x++)
        {
            for (int y = 0; y < _coordinateStates.GetLength(1); y++)
            {
                _coordinateStates[x, y] = GridStates.NOT_PATH;
                // Debug.DrawLine(ConvertToWorldPosition(x, y), ConvertToWorldPosition(x, y + 1), Color.white, 100f);
                // Debug.DrawLine(ConvertToWorldPosition(x, y), ConvertToWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        // Debug.DrawLine(ConvertToWorldPosition(0, _height), ConvertToWorldPosition(_width, _height), Color.white, 100f);
        // Debug.DrawLine(ConvertToWorldPosition(_width, 0), ConvertToWorldPosition(_width, _height), Color.white, 100f);
    }

    private void SetStartPosition()
    {
        _start = Random.Range(1, _height - 1);
        _coordinateStates[0, _start] = GridStates.START;
        Debug.Log("Start: " + _start);
    }

    private void SetFinishPosition()
    {
        _finish = Random.Range(1, _height - 1);
        _coordinateStates[_width - 1, _finish] = GridStates.FINISH;
        Debug.Log("Finish: " + _finish);
    }

    private void GetBarriersX()
    {
        bool canBeBarrier = true;
        int barrierCount = 0;
        for (int i = 1; i < _width - 1; i++)
        {
            if (_barrierX.Contains(i - 1))
            {
                continue;
            }
            if (canBeBarrier)
            {
                int barrierChance = Random.Range(0, 10);
                if (barrierChance < 3)
                {
                    _barrierX.Add(i);
                    barrierCount ++;
                    if(barrierCount > _numTurns){
                        break;
                    }
                    canBeBarrier = false;
                }
            }
            else
            {
                canBeBarrier = true;
            }
        }
    }

    private void SetBarriers()
    {
        bool isTop = (Random.Range(0, 1) == 1) ? true : false;
        int yRef = _start;
        foreach (int barrier in _barrierX)
        {
            int yDepth;
            if (isTop)
            {
                int change = Random.Range(1, yRef);
                yDepth = (yRef - change > 0) ? yRef - change : 1;
                for (int y = _height - 1; y >= yDepth; y--)
                {
                    _coordinateStates[barrier, y] = GridStates.BARRIER;
                }
            }
            else
            {
                int change = Random.Range(yRef + 1, _height - 2);
                yDepth = yRef + change;
                yDepth = (yRef + change < _height - 2) ? yRef + change : _height - 2;
                for (int y = 0; y <= yDepth; y++)
                {
                    _coordinateStates[barrier, y] = GridStates.BARRIER;
                }
            }
            yRef = yDepth;
            isTop = !isTop;
        }
    }

    private void SetPath()
    {
        int currY = _start;
        for (int x = 1; x < _width; x++)
        {
            // Against the outer wall, find finish
            if (x == _width - 1)
            {
                if (_finish > currY)
                {
                    for (int y = currY; y < _finish; y++)
                    {
                        _path.Add(new Vector2Int(x, y));
                        _coordinateStates[x, y] = GridStates.PATH;
                    }
                }
                if (_finish < currY)
                {
                    for (int y = currY; y > _finish; y--)
                    {
                        _path.Add(new Vector2Int(x, y));
                        _coordinateStates[x, y] = GridStates.PATH;
                    }
                }
            }

            // Nothing in the way, add path
            else if (_coordinateStates[x, currY] != GridStates.BARRIER)
            {
                _path.Add(new Vector2Int(x, currY));
                _coordinateStates[x, currY] = GridStates.PATH;
            }

            // Hit the barrier, draw path on x-1 and find a new path
            else if (_coordinateStates[x, currY] == GridStates.BARRIER)
            {
                bool startAtBottom = _coordinateStates[x, 0] == GridStates.BARRIER;
                if(startAtBottom)
                {
                    for(int y=0;y<_height;y++)
                    {
                        if(y <= currY)
                        {
                            continue;
                        }
                        _coordinateStates[x - 1, y] = GridStates.PATH;
                        if(_coordinateStates[x, y] != GridStates.BARRIER)
                        {
                            _coordinateStates[x, y] = GridStates.PATH;
                            currY = y;
                            break;
                        }
                    }
                }
                else
                {
                    for (int y = _height - 1; y >= 0; y--)
                    {
                        if (y >= currY)
                        {
                            continue;
                        }
                        _coordinateStates[x - 1, y] = GridStates.PATH;
                        if (_coordinateStates[x, y] != GridStates.BARRIER)
                        {
                            _coordinateStates[x, y] = GridStates.PATH;
                            currY = y;
                            break;
                        }
                    }
                }
            }
        }
    }

    private void SetPOI()
    {
        int pathCount = _path.Count - 1;
        int poiCounter = 0;
        while(poiCounter < _numPOI)
        {
            int poiPlacement = Random.Range(0, pathCount);
            if(_POI.Contains(_path[poiPlacement]))
            {
                continue;
            }
            _POI.Add(_path[poiPlacement]);
            poiCounter++;
        }
        // foreach (var p in _POI)
        // {
        //     Debug.Log("POI at: " + p);
        // }
    }

    public int GetStart()
    {
        return _start;
    }

    public int GetFinish()
    {
        return _finish;
    }

    public void GetPOI(List<Vector2Int> poi)
    {
        foreach (Vector2Int point in _POI)
        {
            poi.Add(point);
        }
    }
}
