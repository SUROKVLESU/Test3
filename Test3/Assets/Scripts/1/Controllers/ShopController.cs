using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class ShopController : MonoBehaviour
{
    [SerializeField] private GameObject PrefabOfferWindow;
    [SerializeField] private GameObject PrefabResourceIcon;

    private List<GameObject>ResourcesPlayer;
    private List<GameObject> ResourcesMerchant;

    private RectTransform PlayerInventory;
    private RectTransform MerchantInventory;


    private const int CountRow = 5;
    private const int CountLines = 6;
    private float Height;

    public ResourceInventory SelectedResuorce {  get; private set; }
    private GameObject PricePanel;
    private Text TextGold;
    private Color StartColor;
    public void ClicResourceInventory(ResourceInventory inventoryResuorce)
    {
        if (inventoryResuorce == SelectedResuorce)
        {
            PricePanel.SetActive(false);
            SelectedResuorce = null;
        }
        else
        {
            PricePanel.SetActive(true);
            SelectedResuorce = inventoryResuorce;
            if (SelectedResuorce.WhoseResource == EnumWhoseResource.Player)
            {
                PricePanel.transform.GetChild(1).gameObject.SetActive(true);
                PricePanel.transform.GetChild(0).gameObject.SetActive(false);
                PricePanel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text =
                    "Sell\n" + (int)((GameManager.instance.PriceResource.GetPriceResource
                    (SelectedResuorce.ResourseType)*(100-GameManager.PercentageOfMoneyLostOnSale)/100f));
            }
            else
            {
                int price = GameManager.instance.PriceResource.GetPriceResource(SelectedResuorce.ResourseType);
                PricePanel.transform.GetChild(1).gameObject.SetActive(false);
                PricePanel.transform.GetChild(0).gameObject.SetActive(true);
                PricePanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text =
                    "Boy\n" + price;
                if (price > GameController.instance.Gold) PricePanel.transform.GetChild(0).GetComponent<Image>().color = Color.red;
                else PricePanel.transform.GetChild(0).GetComponent<Image>().color = StartColor;
            }
        }
    }


    private void Awake()
    {
        PlayerInventory = transform.GetChild(1).GetComponent<RectTransform>();
        MerchantInventory = transform.GetChild(2).GetComponent<RectTransform>();
        PricePanel = transform.GetChild(3).gameObject;
        TextGold = transform.GetChild(4).GetComponent<Text>();
        StartColor = PricePanel.transform.GetChild(0).GetComponent<Image>().color;
    }
    private void Start()
    {
        TextGold.text = GameController.instance.Gold.ToString();
    }

    public void CreateResources()
    {
        ResourceInventory resourceInventory;
        ResourcesPlayer = 
            ReceiveRandomCards(PlayerInventory, GameController.instance.FillingInResourcesController.ResourcesPlayer,EnumWhoseResource.Player);
        ResourcesMerchant = 
            ReceiveRandomCards(MerchantInventory, GameController.instance.FillingInResourcesController.ResourcesMerchant, EnumWhoseResource.Merchant);
        List<GameObject> ReceiveRandomCards(RectTransform inventory,List<EnumTypeResource> resources,EnumWhoseResource whoseResource)
        {
            Height = inventory.rect.width / CountRow;
            float newScale = Height/PrefabResourceIcon.GetComponent<RectTransform>().rect.width;
            List<GameObject> res = new List<GameObject>(resources.Count);

            Vector2 startPosition = 
                new Vector2(((-inventory.rect.width)/(2*CountRow))* (CountRow-1), (inventory.rect.height / (2 * CountRow)) * (CountRow - 1));
            for (int i = 0, k = 0; i < CountLines&& k < resources.Count; i++)
            {
                for (int j = 0; j < CountRow&&k<resources.Count; j++, k++)
                {
                    res.Add(Instantiate(PrefabResourceIcon, inventory));
                    res[k].GetComponent<Image>().sprite = GameManager.instance.SpriteResource.GetSpriteResource(resources[k]);
                    res[k].GetComponent<RectTransform>().localScale = new Vector2(newScale, newScale);
                    resourceInventory = res[k].GetComponent<ResourceInventory>();
                    resourceInventory.WhoseResource = whoseResource;
                    resourceInventory.ResourseType = resources[k];
                    resourceInventory.ThisResourses = res;
                    res[k].transform.localPosition = startPosition+ new Vector2
                        (Height*j, -Height*i);
                }
            }
            return res;
        }
    }
    public void Boy()
    {
        PricePanel.SetActive(false);

        float newScale = Height / PrefabResourceIcon.GetComponent<RectTransform>().rect.width;
        ResourceInventory resourceInventory;
        GameObject newResource = Instantiate(PrefabResourceIcon, PlayerInventory);
        ResourcesPlayer.Add(newResource);
        newResource.GetComponent<Image>().sprite = GameManager.instance.SpriteResource.GetSpriteResource(SelectedResuorce.ResourseType);
        newResource.GetComponent<RectTransform>().localScale = new Vector2(newScale, newScale);
        resourceInventory = newResource.GetComponent<ResourceInventory>();
        resourceInventory.WhoseResource = EnumWhoseResource.Player;
        resourceInventory.ResourseType = SelectedResuorce.ResourseType;
        resourceInventory.ThisResourses = ResourcesPlayer;

        UpdateResources(PlayerInventory, ResourcesPlayer, EnumWhoseResource.Player);
        UpdateResources(MerchantInventory, ResourcesMerchant, EnumWhoseResource.Merchant);
    }
    public void Sell()
    {
        PricePanel.SetActive(false);

        float newScale = Height / PrefabResourceIcon.GetComponent<RectTransform>().rect.width;
        ResourceInventory resourceInventory;
        GameObject newResource = Instantiate(PrefabResourceIcon, MerchantInventory);
        ResourcesMerchant.Add(newResource);
        newResource.GetComponent<Image>().sprite = GameManager.instance.SpriteResource.GetSpriteResource(SelectedResuorce.ResourseType);
        newResource.GetComponent<RectTransform>().localScale = new Vector2(newScale, newScale);
        resourceInventory = newResource.GetComponent<ResourceInventory>();
        resourceInventory.WhoseResource = EnumWhoseResource.Merchant;
        resourceInventory.ResourseType = SelectedResuorce.ResourseType;
        resourceInventory.ThisResourses = ResourcesMerchant;

        UpdateResources(PlayerInventory, ResourcesPlayer, EnumWhoseResource.Player);
        UpdateResources(MerchantInventory, ResourcesMerchant, EnumWhoseResource.Merchant);
    }
    private void UpdateResources(RectTransform inventory, List<GameObject> resources, EnumWhoseResource whoseResource)
    {
        Height = inventory.rect.width / CountRow;
        float newScale = Height / PrefabResourceIcon.GetComponent<RectTransform>().rect.width;

        Vector2 startPosition =
            new Vector2(((-inventory.rect.width) / (2 * CountRow)) * (CountRow - 1), (inventory.rect.height / (2 * CountRow)) * (CountRow - 1));
        for (int i = 0, k = 0; i < CountLines && k < resources.Count; i++)
        {
            for (int j = 0; j < CountRow && k < resources.Count; j++, k++)
            {
                resources[k].transform.localPosition = startPosition + new Vector2
                    (Height * j, -Height * i);
            }
        }
    }
    public void UpdateGold()
    {
        TextGold.text = GameController.instance.Gold.ToString();
    }
    public bool IsPlayerHasPlace()
    {
        if(ResourcesPlayer.Count<GameManager.MaxResourcesPlayerInventory) return true;
        return false;
    }
    public bool IsMerchantHasPlace()
    {
        if (ResourcesMerchant.Count < GameManager.MaxResourcesMerchantInventory) return true;
        return false;
    }
}
