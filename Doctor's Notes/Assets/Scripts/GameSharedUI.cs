using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSharedUI : MonoBehaviour
{
    #region Singleton class: GameSharedUI

    public static GameSharedUI Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }    
    }

    #endregion

    //For updating all text UIs needed.
    [SerializeField] TMP_Text[] moneyUIText;

    private void Start()
    {
        UpdateMoneyUIText();
    }

    public void UpdateMoneyUIText()
    {
        for (int i = 0; i < moneyUIText.Length; i++)
        {
            SetMoneyText(moneyUIText[i], GameDataManager.GetMoney());
        }
    }

    void SetMoneyText(TMP_Text moneyTextMesh, int moneyValue)
    {
        moneyTextMesh.text = moneyValue.ToString();
    }
}
