using UnityEngine;

public class ResetButton : MonoBehaviour
{
    [SerializeField] private GameEvent _onReset;

    public void OnResetClicked()
    {
        _onReset.Raise();
    }
}
