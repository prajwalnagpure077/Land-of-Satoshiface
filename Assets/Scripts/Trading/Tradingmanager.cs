using TMPro;
using UnityEngine;

public class Tradingmanager : SingleTon<Tradingmanager>
{
    [SerializeField] AttachedData _UI_ItemPrefab, _UI_PropertyPrefab;
    [SerializeField] Transform _ItemSpawnContainer, _PropertySpawnContainer;
    public override void main() { }

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
}
