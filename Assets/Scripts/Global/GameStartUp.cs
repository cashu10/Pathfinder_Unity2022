using System.Collections.Generic;
using UnityEngine;

public class GameStartUp : MonoBehaviour
{
    [SerializeField] private GameObject _prefabPlayer;
    [SerializeField] private GameObject _prefabStart;
    [SerializeField] private GameObject _prefabFinish;
    [SerializeField] private GameObject _prefabPath;
    [SerializeField] private GameObject _prefabBG;
    [SerializeField] private GameObject _prefabGem;

    [SerializeField] private SO_IntBase _difficulty;
    [SerializeField] private SO_LevelBase _levelEasy;
    [SerializeField] private SO_LevelBase _levelMedium;
    [SerializeField] private SO_LevelBase _levelHard;
    [SerializeField] private int _height = 4;
    [SerializeField] private int _width = 4;
    [SerializeField] private int _numBarriers = 2;
    [SerializeField] private int _numPOI = 3;
    [SerializeField] private float _cellSize = 10f;

    private GridHandler _gridHandler;
    private GameObject _environmentHolder;
    private GameObject _POI_Holder;
    void Start()
    {
        _environmentHolder = new GameObject("Environment");
        _POI_Holder = new GameObject("PointsOfInterest");

        SetGameLevel();

        _gridHandler = new GridHandler(_width, _height, _numBarriers, _numPOI);
        PopulateAssets(_gridHandler.CoordinateStates);
        PlaceSPOI();
        PlacePlayer();

    }

    private void SetGameLevel()
    {
        int state = _difficulty.Value;
        state = state < 0 ? 0 : state;
        state = state > 2 ? 2 : state;
        switch (state)
        {
            case 0:
                _height = _levelEasy.Height;
                _width = _levelEasy.Width;
                _numBarriers = _levelEasy.NumBarriers;
                _numPOI = _levelEasy.NumPOI;
                _cellSize = _levelEasy.CellSize;
            break;

            case 1:
                _height = _levelMedium.Height;
                _width = _levelMedium.Width;
                _numBarriers = _levelMedium.NumBarriers;
                _numPOI = _levelMedium.NumPOI;
                _cellSize = _levelMedium.CellSize;
            break;

            case 2:
                _height = _levelHard.Height;
                _width = _levelHard.Width;
                _numBarriers = _levelHard.NumBarriers;
                _numPOI = _levelHard.NumPOI;
                _cellSize = _levelHard.CellSize;
            break;
        }
    }

    private void PopulateAssets(GridStates[,] _coordinateStates)
    {
        for (int x = 0; x < _coordinateStates.GetLength(0); x++)
        {
            for (int y = 0; y < _coordinateStates.GetLength(1); y++)
            {
                if(_coordinateStates[x, y] == GridStates.START)
                {
                    var clone = Instantiate(_prefabStart, ConvertToWorldPosition(x, y) + new Vector3(_cellSize, _cellSize) * .5f, Quaternion.identity);
                    clone.transform.localScale *= _cellSize;
                }
                else if (_coordinateStates[x, y] == GridStates.FINISH)
                {
                    var clone = Instantiate(_prefabFinish, ConvertToWorldPosition(x, y) + new Vector3(_cellSize, _cellSize) * .5f, Quaternion.identity);
                    clone.transform.SetParent(_environmentHolder.transform, false);
                    clone.transform.localScale *= _cellSize;
                }
                else if (_coordinateStates[x, y] == GridStates.PATH)
                {
                    var clone = Instantiate(_prefabPath, ConvertToWorldPosition(x, y) + new Vector3(_cellSize, _cellSize) * .5f, Quaternion.identity);
                    clone.transform.SetParent(_environmentHolder.transform, false);
                    clone.transform.localScale *= _cellSize;
                }
                else
                {
                    var clone = Instantiate(_prefabBG, ConvertToWorldPosition(x, y) + new Vector3(_cellSize, _cellSize) * .5f, Quaternion.identity);
                    clone.transform.SetParent(_environmentHolder.transform, false);
                    clone.transform.localScale *= _cellSize;
                }
            }
        }
    }

    private void PlacePlayer()
    {
        var playerStart = ConvertToWorldPosition(0, _gridHandler.GetStart()) + new Vector3(_cellSize, _cellSize, -2) * .5f;
        var clone = Instantiate(_prefabPlayer, playerStart, Quaternion.identity);
        clone.transform.localScale *= _cellSize;
        var mH = clone.GetComponent<MovementHandler>();
        mH.StartPos = playerStart;
        mH.CellSize = _cellSize;
    }

    private void PlaceSPOI()
    {
        List<Vector2Int> POI = new List<Vector2Int>();
        _gridHandler.GetPOI(POI);
        foreach(Vector2Int point in POI)
        {
            var clone = Instantiate(_prefabGem, ConvertToWorldPosition(point[0], point[1]) + new Vector3(_cellSize, _cellSize, -2) * .5f, Quaternion.identity);
            clone.transform.SetParent(_POI_Holder.transform, false);
            clone.transform.localScale *= _cellSize;
        }
    }

    private Vector3 ConvertToWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * _cellSize;
    }
}
