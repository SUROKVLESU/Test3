using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        float multiplier = Screen.currentResolution.width / 1920;
        if (multiplier < Screen.currentResolution.height / 1080) InterfaceMultiplier = multiplier;
        else InterfaceMultiplier = Screen.currentResolution.height / 1080;

        CountTypeResources = EnumTypeResource.GetNames(typeof(EnumTypeResource)).Length;
    }
    [HideInInspector] public float InterfaceMultiplier;
    public ArrayResourceSprite SpriteResource;
    public ArrayResourcePrice PriceResource;
    public const int MaxResourcesPlayerInventory = 14;
    public const int MaxResourcesMerchantInventory = 14;
    [HideInInspector] public int CountTypeResources;
    public const int PercentageOfMoneyLostOnSale = 10;
}
