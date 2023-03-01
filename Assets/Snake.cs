using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments;
    public Transform segmentPrefab;

    private void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }

    private void Update()
    {
        if (_direction != Vector2.down && Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        }
        else if (_direction != Vector2.up && Input.GetKeyDown(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
        }
        else if (_direction != Vector2.right && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = Vector2.left;
        }
        else if (_direction != Vector2.left && Input.GetKeyDown(KeyCode.RightArrow))
        {
            _direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }

    private void Grow()
    {
        Transform newSegment = Instantiate(this.segmentPrefab);
        newSegment.position = _segments[_segments.Count - 1].position;
        _segments.Add(newSegment);
    }

    private void ResetGame()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }
        else if (other.tag == "Obstacle")
        {
            ResetGame();
        }
    }
}
