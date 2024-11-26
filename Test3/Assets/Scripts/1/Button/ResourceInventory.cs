using System.Collections.Generic;
using UnityEngine;

public class ResourceInventory:MonoBehaviour
{
    public EnumTypeResource ResourseType;
    public EnumWhoseResource WhoseResource;
    public List<GameObject> ThisResourses;
    private void OnMouseDown()
    {
        GameController.instance.ShopController.ClicResourceInventory(this);
    }
    public void Reposition()
    {
        ThisResourses.Remove(gameObject);
        Destroy(gameObject);
    }
}

