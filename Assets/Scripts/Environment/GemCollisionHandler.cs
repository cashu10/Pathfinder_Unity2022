using UnityEngine;

public class GemCollisionHandler : MonoBehaviour
{
    [SerializeField] SO_IntBase_resetting _score;
    private SpriteRenderer _sprite;
    private Collider2D _collider;

    private void Start()
    {
        _sprite = gameObject.GetComponent<SpriteRenderer>();
        _collider = gameObject.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            _sprite.enabled = false;
            _collider.enabled = false;
            _score.Value++;
        }
    }

    public void ResetGem()
    {
        if(!_sprite.enabled){
            _sprite.enabled = true;
            _collider.enabled = true;
            _score.Value--;
        }
    }
}
