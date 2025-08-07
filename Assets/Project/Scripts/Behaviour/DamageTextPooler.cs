using System.Collections.Generic;
using UnityEngine;

public class DamageTextPooler : MonoBehaviour
{
    public static DamageTextPooler Instance;

    [SerializeField] private GameObject _damageTextPrefab;
    [SerializeField] private int _poolSize = 10;

    private readonly Queue<GameObject> _damageTextPool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject textObject = Instantiate(_damageTextPrefab, transform);
            textObject.SetActive(false);
            _damageTextPool.Enqueue(textObject);
        }
    }

    public GameObject GetDamageText()
    {
        if (_damageTextPool.Count > 0)
        {
            GameObject textObject = _damageTextPool.Dequeue();
            textObject.SetActive(true);
            return textObject;
        }
        else
        {
            GameObject newTextObject = Instantiate(_damageTextPrefab, transform);
            return newTextObject;
        }
    }

    public void ReturnToPool(GameObject textObject)
    {
        textObject.SetActive(false);
        textObject.transform.SetParent(transform);
        _damageTextPool.Enqueue(textObject);
    }
}