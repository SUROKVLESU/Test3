using UnityEngine;

public class Find_StartMenu:MonoBehaviour
{
    private void Start()
    {
        GameController.instance.StartMenu = gameObject;
    }
}
