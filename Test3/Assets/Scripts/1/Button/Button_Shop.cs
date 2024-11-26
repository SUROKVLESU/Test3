using UnityEngine;
using UnityEngine.UI;

public class Button_Shop : MonoBehaviour
{
    private void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            GameController.instance.Shop.SetActive(true);
            GameController.instance.ShopController.CreateResources();
            GameController.instance.StartMenu.SetActive(false);
        });
    }
}
