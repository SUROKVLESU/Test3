using UnityEngine;

public class Find_Shop:MonoBehaviour
{
    private void Start()
    {
        GameController.instance.ShopController = transform.GetComponent<ShopController>();
        GameController.instance.Shop = gameObject;
        gameObject.SetActive(false);
    }
}