using UnityEngine;
using UnityEngine.UI;

public class Button_ShopBoy:MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            int price = GameManager.instance.PriceResource.GetPriceResource(GameController.instance.ShopController.SelectedResuorce.ResourseType);
            if (price < GameController.instance.Gold&&GameController.instance.ShopController.IsPlayerHasPlace())
            {
                GameController.instance.Gold -= price;
                GameController.instance.ShopController.UpdateGold();
                GameController.instance.ShopController.SelectedResuorce.Reposition();
                GameController.instance.ShopController.Boy();
            }
        });
    }
}

