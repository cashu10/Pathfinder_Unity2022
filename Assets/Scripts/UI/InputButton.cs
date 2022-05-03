using UnityEngine;
using UnityEngine.UI;

public class InputButton : MonoBehaviour, IControlDirection
{
    [SerializeField] private GameEvent_object _onButtonEvent;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _defaultSprite;
    private Directions _direction = Directions.ERROR;
    private Image _activeInputImage;
    private RectTransform _activeInputTransform;

    private void OnEnable() 
    {
        _activeInputImage = gameObject.GetComponentInChildren<Image>();
        _activeInputTransform = gameObject.GetComponent<RectTransform>();
    }

    public void RaiseButtonClick()
    {
        _onButtonEvent.Raise(this.gameObject);
    }

    public void ResetButton()
    {
        _direction = Directions.ERROR;
        _activeInputImage.sprite = _defaultSprite;
    }

    public void SetDirection(Directions direction)
    {
        _direction = direction;
        SetDirection();
    }

    public Directions GetDirection()
    {
        return _direction;
    }

    public void SetDirection()
    {
        _activeInputImage.sprite = _activeSprite;
        Vector3 change;
        float currentZ = _activeInputTransform.eulerAngles.z;
        float zeroZ = currentZ > 0 ? -1 * currentZ : currentZ;
        switch (_direction)
        {
            case Directions.UP:
                change = new Vector3(0, 0, zeroZ);
                _activeInputTransform.Rotate(change);
                break;

            case Directions.RIGHT:
                change = new Vector3(0, 0, -90 + zeroZ);
                _activeInputTransform.Rotate(change);
                break;

            case Directions.DOWN:
                change = new Vector3(0, 0, 180 + zeroZ);
                _activeInputTransform.Rotate(change);
                break;

            case Directions.LEFT:
                change = new Vector3(0, 0, 90 + zeroZ);
                _activeInputTransform.Rotate(change);
                break;
        }
    }
}
