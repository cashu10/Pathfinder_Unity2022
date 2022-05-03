using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    [SerializeField] private SO_IntBase_resetting _score;
    private TextMeshProUGUI _text;

    private void OnEnable() => _text = gameObject.GetComponent<TextMeshProUGUI>();

    private void Update() {
        _text.text = _score.Value.ToString();
    }
}
