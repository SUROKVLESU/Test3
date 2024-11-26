using UnityEngine;

public class GameController:MonoBehaviour
{
    public static GameController instance;
    public int Gold = 340;
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
    }
    public FillingInResourcesController FillingInResourcesController { get; set; }
    public ShopController ShopController { get; set; }

    public GameObject Shop {  get; set; }   
    public GameObject StartMenu { get; set; }
}
 
