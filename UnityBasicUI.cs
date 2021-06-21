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
