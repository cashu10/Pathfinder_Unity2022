using UnityEngine;

public class ChoiceButtons : MonoBehaviour
{
    private Canvas _canvas;
    private IControlDirection _activeInput;

    private void OnEnable() => _canvas = gameObject.GetComponent<Canvas>();

    public void GetUpPressed()
    {
        SendDirectionPressed(Directions.UP);
    }

    public void GetRightPressed()
    {
        SendDirectionPressed(Directions.RIGHT);
    }

    public void GetDownPressed()
    {
        SendDirectionPressed(Directions.DOWN);
    }

    public void GetLeftPressed()
    {
        SendDirectionPressed(Directions.LEFT);
    }

    public void SendDirectionPressed(Directions direction)
    {
        _activeInput.SetDirection(direction);
        _canvas.enabled = false;
    }

    public void SetActiveInputButton(GameObject active)
    {
        _activeInput = active.GetComponentInChildren<IControlDirection>();
    }
}
