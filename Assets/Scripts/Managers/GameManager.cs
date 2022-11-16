using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using System.Collections.Generic;

public class GameManager : SingleTon<GameManager>
{
    [SerializeField] TextMeshProUGUI CID_ID_TEXT;
    [SerializeField] TextMeshProUGUI _CoinConnect_Cash;
    [SerializeField] TextMeshProUGUI _GoldenBit;

    public override void main()
    {
        //Init
        StaticGamemanager.gameDataStructure.CID_Number.onValueChange += onChangeCID_ID;
        StaticGamemanager.gameDataStructure.CoinConnect_Cash.onValueChange += OnCoinConnectCashChange;
        StaticGamemanager.gameDataStructure.GoldenBit.onValueChange += onGoldenBitChange;


    }

    void onChangeCID_ID(int id)
    {
        CID_ID_TEXT.text = id.ToString("00000000");
    }

    void OnCoinConnectCashChange(int i)
    {
        _CoinConnect_Cash.text = "Coinconnect Cash " + i.ToString("0");
    }

    void onGoldenBitChange(int i)
    {
        _GoldenBit.text = "Goldenbit " + i.ToString("0");
    }
}
