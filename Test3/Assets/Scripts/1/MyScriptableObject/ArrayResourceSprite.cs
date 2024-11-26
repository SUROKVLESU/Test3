using UnityEngine;
using System;
[CreateAssetMenu(fileName = "ArrayResourceSprite")]
[Serializable]
public class ArrayResourceSprite:ScriptableObject
{
    [SerializeField] private ResourceSprite[] Sprites;
    public Sprite GetSpriteResource(EnumTypeResource typeResource)
    {
        foreach (var sprite in Sprites)
        {
            if(sprite.TypeResource == typeResource) return sprite.Sprite;
        }
        return null;
    }
    public ArrayResourceSprite()
    {
        int countTypeResources = EnumTypeResource.GetNames(typeof(EnumTypeResource)).Length;
        Sprites = new ResourceSprite[countTypeResources];
        for (int i = 0; i < countTypeResources; i++)
        {
            Sprites[i] = new ResourceSprite() { TypeResource = (EnumTypeResource)i };
        }
    }
}

