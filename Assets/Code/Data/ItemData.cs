using System;
using Code.Data;
using Code.Infrastructure.Assets;
using Code.UI.Windows.ShopWindow;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class ItemData
{
    public ItemType Type;
    public Sprite Sprite;
    public int Cost;
    public int Damage;
}
