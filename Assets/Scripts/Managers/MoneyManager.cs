using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI M_MoneyText;


    public static MoneyManager Instance;
    public static int currentMoney = 0;
    internal List<Transform> toRotateList = new();
    private void Awake()
    {
        Instance = this;
        StartCoroutine(rotateMoney());
        AddMoney(0);
    }

    internal void AddMoney(int t)
    {
        currentMoney += t;
        OnMoneyChange();
    }

    IEnumerator rotateMoney()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            foreach (var item in toRotateList)
            {
                item.Rotate(new Vector3(0, 1, 0), Space.World);
            }
        }
    }

    void OnMoneyChange()
    {
        M_MoneyText.text = currentMoney.ToString("0");
    }
}
