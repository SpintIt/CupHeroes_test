using UnityEngine;

public class Scroller : MonoBehaviour
{
    private float _spriteWidth;
    private Vector3 _spawnPosition;
    private int _screenWidth;
    private float _screenLeftEdge;
    
    [SerializeField] private float scrollSpeed = 1f;
    [SerializeField] private SpriteRenderer sprite;

    private void Awake()
    {
        _screenWidth = Screen.width;
        _spriteWidth = sprite.bounds.size.x;
        RecalculateScreenEdges();
    }

    public void Handler()
    {
        if (Screen.width != _screenWidth)
        {
            _screenWidth = Screen.width;
            RecalculateScreenEdges();
        }

        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        if (transform.position.x + _spriteWidth / 2 < _screenLeftEdge)
        {
            transform.position = _spawnPosition;
        }
    }

    private void RecalculateScreenEdges()
    {
        _screenLeftEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenRightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        _spawnPosition = new Vector3(screenRightEdge + _spriteWidth / 2, transform.position.y, transform.position.z);
    }
}
