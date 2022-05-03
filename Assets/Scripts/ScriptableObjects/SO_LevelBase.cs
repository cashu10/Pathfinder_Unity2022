using UnityEngine;

[CreateAssetMenu(fileName = "LevelBase", menuName = "Pathfinding/Level")]
public class SO_LevelBase : ScriptableObject
{
    [SerializeField] private int _height;
    [SerializeField] private int _width;
    [SerializeField] private int _numBarriers;
    [SerializeField] private int _numPOI;
    [SerializeField] private float _cellSize;
    public int Height { get; private set; }
    public int Width { get; private set; }
    public int NumBarriers { get; private set; }
    public int NumPOI { get; private set; }
    public float CellSize { get; private set; }

    private void OnEnable() {
        Height = _height;
        Width = _width;
        NumBarriers = _numBarriers;
        NumPOI = _numPOI;
        CellSize = _cellSize;
    }
}
