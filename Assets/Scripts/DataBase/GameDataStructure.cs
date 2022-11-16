using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public record GameDataStructure
{
    public ChangeableData<int> CID_Number = new();
    public ChangeableData<int> CoinConnect_Cash = new();
    public ChangeableData<int> GoldenBit = new();
    public List<Item> itemsCollected = new();
    public List<Property> PropertiesCollected = new();

    //World
    public Vector3? lastPlayerPos = null;
    public Quaternion? lastPlayerRot = null;
}

[Serializable]
public record Item
{
    [field: SerializeField] public string name { get; set; } = "Unknown";
    [field: SerializeField] public ItemType itemType { get; set; } = ItemType.things;
    [field: SerializeField] public int intValue { get; set; } = 0;
    [field: SerializeField] public float floatValue { get; set; } = 0;
    [field: SerializeField] public int priceInCash { get; set; } = 0;
    [field: SerializeField] public int count { get; set; } = 0;

    public override string ToString()
    {
        return name;
    }
}

public enum ItemType
{
    food,
    things
}

[Serializable]
public record Property
{
    [field: SerializeField] public string name { get; set; }
    [field: SerializeField] public int propertyCode { get; set; }
    [field: SerializeField] public int priceInCash { get; set; }


    public override string ToString()
    {
        return name;
    }
}


[Serializable]
public struct ChangeableData<T>
{
    private T _Value;
    public T Value
    {
        get => _Value;
        set
        {
            _Value = value;
            onValueChange?.Invoke(value);
        }
    }
    public Action<T> onValueChange;
}