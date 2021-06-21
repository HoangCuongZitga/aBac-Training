private void OnValidate()
    {
        // auto fill list while Editor has changed
        if (listItem == null || listItem.Length == 0) listItem = transform.GetComponentsInChildren<SubData>();
    }
