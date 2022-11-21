using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tradingmanager : SingleTon<Tradingmanager>
{
    [SerializeField] AttachedData _UI_ItemPrefab, _UI_PropertyPrefab;
    [SerializeField] Transform _ItemSpawnContainer, _PropertySpawnContainer;
    [SerializeField] internal GameObject TradingScreen, sellItemPanel, sellPropertyPanel;
    public override void main()
    {
        TradingScreen.SetActive(false);
        RefreshItems();
    }

    [ContextMenu("addItem")]
    private void addItem()
    {
        addItemUI(new Item());
    }

    [ContextMenu("addProperty")]
    private void addProperty()
    {
        addPropetryUI(new Property() { propertyCode = 2 });
    }

    public static void addItemUI(Item item)
    {
        if (item != null)
        {
            var spawned = Instantiate(Instance._UI_ItemPrefab, Instance._ItemSpawnContainer);
            if (spawned.getComponentByName("title", out TextMeshProUGUI title))
            {
                string titleStr = "";
                titleStr = titleStr.appendAlignment("Qt", TextAlignment.left);
                titleStr = titleStr.appendAlignment("Name", TextAlignment.center);
                titleStr = titleStr.appendAlignment("cost", TextAlignment.right, true);
                titleStr = titleStr.appendAlignment(item.count.ToString(), TextAlignment.left);
                titleStr = titleStr.appendAlignment(item.name.ToString(), TextAlignment.center);
                titleStr = titleStr.appendAlignment(item.priceInCash.ToString(), TextAlignment.right, true);
                title.text = titleStr;
            }
        }
    }

    public static void addPropetryUI(Property property)
    {
        if (property != null)
        {
            var spawned = Instantiate(Instance._UI_PropertyPrefab, Instance._PropertySpawnContainer);
            if (spawned.getComponentByName("title", out TextMeshProUGUI title))
            {
                string titleStr = "";
                titleStr = titleStr.appendAlignment("Spot", TextAlignment.left);
                titleStr = titleStr.appendAlignment("cost", TextAlignment.right, true);
                titleStr = titleStr.appendAlignment("Plot " + property.propertyCode.ToString("0000"), TextAlignment.left);
                titleStr = titleStr.appendAlignment(property.priceInCash.ToString(), TextAlignment.right, true);
                title.text = titleStr;
            }
        }
    }

    public void enableTradingScreen(bool t)
    {
        TradingScreen.SetActive(t);
        if (t)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }



    #region ItemBuy
    [SerializeField] Transform _itemBuyContainer;
    [SerializeField] AttachedData _itemBuyPrefab;

    public void RefreshItems()
    {
        foreach (Transform child in _ItemSpawnContainer)
        {
            Destroy(child.gameObject);
        }
        foreach (var item in StaticGamemanager.gameDataStructure.itemsCollected)
        {
            addItemUI(item);
        }
    }

    public static void removeAllItemFromSale()
    {
        foreach (Transform item in Instance._itemBuyContainer)
        {
            Destroy(item.gameObject);
        }
    }

    public static void listItemsOnSale(List<Item> items, Action onBuy)
    {
        foreach (var item in items)
        {
            if (item.count > 0)
            {
                Instance.addItemToSale(item, onBuy);
            }
        }
    }

    private void addItemToSale(Item item, Action onBuy)
    {
        var currentItemOnSale = Instantiate(_itemBuyPrefab, _itemBuyContainer);
        if (currentItemOnSale.getComponentByName<Button>("buyButton", out Button buyButton))
        {
            //buy button init
            buyButton.onClick.AddListener(() =>
            {
                buyItem(item, currentItemOnSale.gameObject);
                onBuy?.Invoke();
            });
        }
        if (currentItemOnSale.getComponentByName("buyText", out TextMeshProUGUI buyButtonText))
        {
            //buy text init
            buyButtonText.text += "<size=50%>" + item.priceInCash + "CCC</size>";
        }
        if (currentItemOnSale.getComponentByName("itemText", out TextMeshProUGUI itemText))
        {
            //item text init
            itemText.text = item.name;
        }
    }

    public void buyItem(Item item, GameObject itemObj)
    {
        if (MoneyManager.Buy(true, item.priceInCash))
        {
            StaticGamemanager.gameDataStructure.itemsCollected = StaticGamemanager.gameDataStructure.itemsCollected.addItemIncludeCount(item);
            StaticGamemanager.SaveGameDataStructure();
            if (item.count <= 0)
                Destroy(itemObj);
            RefreshItems();
        }
    }

    public void SetSellItemPanel(bool t)
    {
        sellItemPanel.SetActive(t);
    }
    #endregion



    #region PropertyBuy
    public void SetSellPropertyPanel(bool t)
    {
        sellPropertyPanel.SetActive(t);
    }
    #endregion
}
