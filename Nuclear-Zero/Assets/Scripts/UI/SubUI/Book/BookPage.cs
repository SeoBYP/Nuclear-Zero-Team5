using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Book/BookPage")]
public class BookPage //: ScriptableObject
{
    [SerializeField] private Sprite _sprite;

    public Sprite Page => _sprite;
}
