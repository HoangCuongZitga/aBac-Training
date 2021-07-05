// auto fill list while Editor has changed
private void OnValidate()
    {
        if (listItem == null || listItem.Length == 0) listItem = transform.GetComponentsInChildren<SubData>();
    }

// load specific something by using Resource
 public void SetSprite(string id)
    {
        imageOfItem.sprite = Resources.Load("ImageOfItems/" + id, typeof(Sprite)) as Sprite;
    }

//load all images from a path into list
void LoadImages()
    {
        listSprites = Resources.LoadAll("ImageOfItems", typeof(Sprite)).Cast<Sprite>().ToList();
    }

//using when gameObject is a button or UI elements
using UnityEngine.EventSystems;
public class ButtonController : MonoBehaviour, IPointerEnterHandler
{
public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
}

// using when gameObject is a sprite or not a UI element
void OnMouseOver()
    {
    }
