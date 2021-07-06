using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SubData : MonoBehaviour
{
    [SerializeField] private Text nameItem;
    public Button buttonClaim;
    public GameObject darkPanel;

    public GameObject listItemAward;
    public GameObject modelListItemAward;
    [SerializeField] private Text[] listNumberOfItemAward;

    // Start is called before the first frame update

    private void OnValidate()
    {
        if (listNumberOfItemAward == null || listNumberOfItemAward.Length == 0)
            listNumberOfItemAward = listItemAward.transform.GetComponentsInChildren<Text>();
    }

    void Start()
    {
        darkPanel.SetActive(false);
        buttonClaim.onClick.AddListener(ClaimOnClickEffect);

        // generate random number foreach item award
        for (int i = 0; i < listNumberOfItemAward.Length; i++)
        {
            listNumberOfItemAward[i].text = GenerateRandomNumber(1, 9).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void SetNameItem(string newName)
    {
        nameItem.text = newName;
    }


    void ClaimOnClickEffect()
    {
        darkPanel.SetActive(true);
    }

    int GenerateRandomNumber(int startRange, int endRange)
    {
        int randomNumber = Random.Range(startRange, endRange);
        return randomNumber;
    }
}