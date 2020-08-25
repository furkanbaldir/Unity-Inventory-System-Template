using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    private int _itemId;
    private Sprite _itemSprite;

    public int GetItemId()
    {
        return _itemId;
    }

    public Sprite GetItemSprite()
    {
        return _itemSprite;
    }

    public void SetItemId(int id)
    {
        _itemId = id;
    }

    public void SetItemSprite(Sprite sprite)
    {
        _itemSprite = sprite;
    }
}
