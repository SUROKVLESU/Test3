using System;
using UnityEngine;
[CreateAssetMenu(fileName = "ArrayResourcePrice")]
[Serializable]
public class ArrayResourcePrice:ScriptableObject
{
    [SerializeField] private ResourcePrice[] Prices;
    public int GetPriceResource(EnumTypeResource typeResource)
    {
        foreach (var sprite in Prices)
        {
            if (sprite.TypeResource == typeResource) return sprite.Price;
        }
        return 0;
    }
    public ArrayResourcePrice()
    {
        int countTypeResources = EnumTypeResource.GetNames(typeof(EnumTypeResource)).Length;
        Prices = new ResourcePrice[countTypeResources];
        for (int i = 0; i < countTypeResources; i++)
        {
            Prices[i] = new ResourcePrice() { TypeResource = (EnumTypeResource)i };
        }
    }
}

