using System;
using Base.Runtime.Manager;
using Base.Runtime.UI;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Upgrade;

public class UIUpgradePanel : BaseUIPanel
{
    [SerializeField] private Color valueTextColor;
    
    [SerializeField] private Sprite tabLightBackgroundSprite;
    [SerializeField] private Sprite tabDarkBackgroundSprite;
    [SerializeField] private Sprite itemDarkBackgroundSprite;
    [SerializeField] private Sprite itemLightBackgroundSprite;
    [SerializeField] private Sprite itemDarkButtonSprite;
    [SerializeField] private Sprite itemLightButtonSprite;
    [SerializeField] private Sprite itemCoinSprite;
    [SerializeField] private Sprite itemTickSprite;


    [SerializeField] private UpgradeWindow characterUpgradeWindow;
    [SerializeField] private UpgradeWindow chickenUpgradeWindow;
    [SerializeField] private UpgradeWindow incubationUpgradeWindow;

    private UpgradeWindow _activeWindow;

    private void Start()
    {
        SwitchToCharacterUpgradeWindow();
    }

    public override void EnablePanel()
    {
        base.EnablePanel();
        //TouchManager.Instance.ChangeJoystickState(false);
    }

    public override void DisablePanel()
    {
        base.DisablePanel();
        //TouchManager.Instance.ChangeJoystickState(true);
    }

    public override void UpdateContent()
    {
        base.UpdateContent();
        
        if(_activeWindow==null)
            return;
        
        foreach (var upgradeItem in _activeWindow.upgradeItems)
        {
            var upgradeLevel = upgradeItem.upgradeData.level;
            
            if (upgradeLevel >= upgradeItem.upgradeData.amountAndCosts.Length)
            {
                DisableItem(upgradeItem);
                continue;
            }

            if (MoneyManager.Instance.Money < upgradeItem.upgradeData.amountAndCosts[upgradeLevel].cost)
            {
                upgradeItem.upgradeButton.interactable = false;
            }
            else
            {
                upgradeItem.upgradeButton.interactable = true;
            }
            
            upgradeItem.iconLevelText.text = upgradeLevel.ToString();
            upgradeItem.buttonCostText.text = upgradeItem.upgradeData.amountAndCosts[upgradeLevel].cost.ToString();

            for (var i = 0; i < upgradeItem.valueText.Length; i++)
            {
                var valueText = upgradeItem.valueText[i];
                valueText.text = FormatAndReturnValue(upgradeItem.upgradeData.amountAndCosts[upgradeLevel].amount,
                    upgradeItem.upgradeData.foreword, upgradeItem.upgradeData.name[i]);
            }
        }
    }

    public void SwitchToCharacterUpgradeWindow()
    {
        EnableWindowObject(characterUpgradeWindow);
        characterUpgradeWindow.tabBackgroundImage.sprite = tabLightBackgroundSprite;
        _activeWindow = characterUpgradeWindow;
        UpdateContent();
    }
    
    public void SwitchToChickenUpgradeWindow()
    {
        EnableWindowObject(chickenUpgradeWindow);
        chickenUpgradeWindow.tabBackgroundImage.sprite = tabLightBackgroundSprite;
        _activeWindow = chickenUpgradeWindow;
        UpdateContent();
    }
    
    public void SwitchToIncubationUpgradeWindow()
    {
        EnableWindowObject(incubationUpgradeWindow);
        incubationUpgradeWindow.tabBackgroundImage.sprite = tabLightBackgroundSprite;
        _activeWindow = incubationUpgradeWindow;
        UpdateContent();
    }

    private void DisableItem(UpgradeItem upgradeItem)
    {
        upgradeItem.upgradeButton.interactable = false;
        upgradeItem.backgroundImage.sprite = itemDarkBackgroundSprite;
        upgradeItem.buttonBackgroundImage.sprite = itemDarkButtonSprite;
        upgradeItem.buttonIconImage.sprite = itemTickSprite;
        upgradeItem.buttonCostText.text = "MAX";
    }
    
    private void EnableItem(UpgradeItem upgradeItem)
    {
        upgradeItem.upgradeButton.interactable = true;
        upgradeItem.backgroundImage.sprite = itemLightBackgroundSprite;
        upgradeItem.buttonBackgroundImage.sprite = itemLightButtonSprite;
        upgradeItem.buttonIconImage.sprite = itemCoinSprite;
    }

    private string FormatAndReturnValue(int value, string foreword,string valueName)
    {
        string valueString="";
        
        valueString += foreword + $" <color=#{valueTextColor.ToHexString()}>" +
                       value + "</color> " + valueName + "\n";

        return valueString;
    }

    private void EnableWindowObject(UpgradeWindow upgradeWindow)
    {
        DisableAllWindows();
        upgradeWindow.windowObject.SetActive(true);
    }

    private void DisableAllWindows()
    {
        characterUpgradeWindow.windowObject.SetActive(false);
        characterUpgradeWindow.tabBackgroundImage.sprite = tabDarkBackgroundSprite;
        chickenUpgradeWindow.windowObject.SetActive(false);
        chickenUpgradeWindow.tabBackgroundImage.sprite = tabDarkBackgroundSprite;
        incubationUpgradeWindow.windowObject.SetActive(false);
        incubationUpgradeWindow.tabBackgroundImage.sprite = tabDarkBackgroundSprite;
    }
}

[Serializable]
internal class UpgradeWindow
{
    public GameObject windowObject;
    public Image tabBackgroundImage;
    public UpgradeItem[] upgradeItems;
}

[Serializable]
internal class UpgradeItem
{
    public UpgradesSO upgradeData;
    public Button upgradeButton;
    public TMP_Text iconLevelText;
    public TMP_Text[] valueText;
    public TMP_Text buttonCostText;
    public Image buttonBackgroundImage;
    public Image buttonIconImage;
    public Image backgroundImage;
    
}