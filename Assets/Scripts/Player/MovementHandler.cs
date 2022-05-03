using System.Collections;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    [SerializeField] private bool _canMove = false;
    [SerializeField] private Directions _currentDirection = Directions.ERROR;
    [SerializeField] float _moveSpeed = 5.0f;
    [SerializeField] private GameEvent _onBarrierHit;
    [SerializeField] private GameEvent _onFinishHit;
    public Vector3 StartPos;
    public float CellSize;
    void Update()
    {
        Debug.DrawLine(transform.position + new Vector3(0, (CellSize / 2), -1), transform.position + new Vector3(0, (CellSize / 2), 1), Color.red, .1f);
        Debug.DrawLine(transform.position + new Vector3((CellSize / 2), 0, -1), transform.position + new Vector3((CellSize / 2), 0, 1), Color.red, .1f);
        Debug.DrawLine(transform.position + new Vector3(0, (CellSize / 2) * -1, -1), transform.position + new Vector3(0, (CellSize / 2) * -1, 1), Color.red, .1f);
        Debug.DrawLine(transform.position + new Vector3((CellSize / 2) * -1, 0, -1), transform.position + new Vector3((CellSize / 2) * -1, 0, 1), Color.red, .1f);
        
        if(!_canMove) return;
        switch (_currentDirection)
        {
            case Directions.UP:
                transform.Translate(Vector3.up * Time.deltaTime * _moveSpeed);
                break;

            case Directions.RIGHT:
                transform.Translate(Vector3.right * Time.deltaTime * _moveSpeed);
                break;

            case Directions.DOWN:
                transform.Translate(Vector3.down * Time.deltaTime * _moveSpeed);
                break;

            case Directions.LEFT:
                transform.Translate(Vector3.left * Time.deltaTime * _moveSpeed);
                break;

            case Directions.ERROR:
                break;

            default:
                break;
        }
    }
    
    private void FixedUpdate() 
    {
        if (!_canMove) return;
        Collider2D hit;
        switch (_currentDirection)
        {
            case Directions.UP:
                hit = Physics2D.OverlapPoint(transform.position + new Vector3(0, (CellSize * .55f)));
                CheckRaycast(hit);
                break;

            case Directions.RIGHT:
                hit = Physics2D.OverlapPoint(transform.position + new Vector3((CellSize * .55f), 0));
                CheckRaycast(hit);
                break;

            case Directions.DOWN:
                hit = Physics2D.OverlapPoint(transform.position + new Vector3(0, (CellSize * .55f) * -1));
                CheckRaycast(hit);
                break;

            case Directions.LEFT:
                hit = Physics2D.OverlapPoint(transform.position + new Vector3((CellSize * .55f) * -1, 0));
                CheckRaycast(hit);
                break;
        }        
    }

    private void CheckRaycast(Collider2D hit)
    {
        if (hit != null)
        {
            // Debug.Log("HIT WAS..." + hit.gameObject.tag);
            
            if (hit.gameObject.tag == "Barrier")
            {
                StopRecieved();
            }
            if (hit.gameObject.tag == "Finish")
            {
                FinishRecieved();
            }
        }
    }

    public void StopRecieved()
    {
        _onBarrierHit.Raise();
        _canMove = false;
    }

    public void SetCurrentDirection(Directions direction)
    {
        _currentDirection = direction;
        StartCoroutine("WaitAndSetMove");
    }

    public void FinishRecieved()
    {
        _onFinishHit.Raise();
        _canMove = false;
    }

    public void ResetPlayer()
    {
        transform.position = StartPos;
        _canMove = false;
    }

    private IEnumerator WaitAndSetMove()
    {
        yield return new WaitForSeconds(.1f);
        _canMove = true;
    }
}
