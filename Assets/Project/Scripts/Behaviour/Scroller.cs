using UnityEngine;

public class Scroller : MonoBehaviour
{
        // Скорость, с которой будет двигаться спрайт.
    public float scrollSpeed = 0.5f;

    // Ширина спрайта.
    private float spriteWidth;

    // Позиция, где спрайт будет начинать свой путь (за правой границей экрана).
    private Vector3 spawnPosition;

    // Переменная для хранения предыдущей ширины экрана.
    private int screenWidth;
    
    // Переменная для хранения левого края экрана.
    private float screenLeftEdge;

    void Awake()
    {
        // Инициализируем ширину экрана.
        screenWidth = Screen.width;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null || sr.sprite == null)
        {
            Debug.LogError("SpriteRenderer или спрайт не найден на объекте.");
            enabled = false;
            return;
        }

        spriteWidth = sr.bounds.size.x;
        RecalculateScreenEdges();
    }

    void Update()
    {
        // Проверяем, изменилась ли ширина экрана.
        if (Screen.width != screenWidth)
        {
            screenWidth = Screen.width;
            RecalculateScreenEdges();
        }

        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        if (transform.position.x + spriteWidth / 2 < screenLeftEdge)
        {
            transform.position = spawnPosition;
        }
    }

    /// <summary>
    /// Метод для пересчета границ экрана и начальной позиции.
    /// </summary>
    private void RecalculateScreenEdges()
    {
        screenLeftEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenRightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        
        spawnPosition = new Vector3(screenRightEdge + spriteWidth / 2, transform.position.y, transform.position.z);
        
        // Если спрайт уже двигается, его позиция не должна быть сброшена в RecalculateScreenEdges().
        // Это нужно только при старте.
    }
}
