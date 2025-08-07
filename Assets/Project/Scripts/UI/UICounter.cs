using TMPro;
using UnityEngine;

public class UICounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _count;

    public void SetCount(string value)
    { 
        _count.text = value;
    }
}