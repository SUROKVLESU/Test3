using System.Collections.Generic;
using UnityEngine;

public class FillingInResourcesController : MonoBehaviour
{
    void Start()
    {
        Initialization();
        GameController.instance.FillingInResourcesController = this;
    }

    public List<EnumTypeResource> ResourcesPlayer { get; private set; }
    public List<EnumTypeResource> ResourcesMerchant { get; private set; }
    private void Initialization()
    {
        int count = Random.Range(1, GameManager.MaxResourcesPlayerInventory + 1);
        ResourcesPlayer = new List<EnumTypeResource>();
        for (int i = 0; i < count; i++)
        {
            ResourcesPlayer.Add((EnumTypeResource)UnityEngine.Random.Range(0,GameManager.instance.CountTypeResources));
        }
        count = Random.Range(1, GameManager.MaxResourcesMerchantInventory + 1);
        ResourcesMerchant = new List<EnumTypeResource>();
        for (int i = 0; i < count; i++)
        {
            ResourcesMerchant.Add((EnumTypeResource)UnityEngine.Random.Range(0, GameManager.instance.CountTypeResources));
        }
    }
    public void Buy(EnumTypeResource typeResource)
    {
        ResourcesMerchant.Remove(typeResource);
        ResourcesPlayer.Add(typeResource);
    }
    public void Sell(EnumTypeResource typeResource)
    {
        ResourcesMerchant.Add(typeResource);
        ResourcesPlayer.Remove(typeResource);
    }
    public bool IsPlayerHasPlace()
    {
        if(ResourcesPlayer.Count < GameManager.MaxResourcesPlayerInventory) return true;
        else return false;
    }
    public bool IsMerchantHasPlace()
    {
        if(ResourcesMerchant.Count < GameManager.MaxResourcesMerchantInventory) return true; 
        else return false;
    }
}
