using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryView : UIBehaviour
{
    private Button _closeBtn;
    private ScrollRect _inventoryScrollRect;
    private Image _equpSlotImage;
    private List<InventoryItemView> _equipmentViews = new List<InventoryItemView>();
    private List<InventoryItemView> _consumableViews = new List<InventoryItemView>();
    private List<InventoryItemView> _materialViews = new List<InventoryItemView>();
    private List<InventoryItemView> _itemViews = new List<InventoryItemView>();
    private Text _maxHealthStatText;
    private Text _damageStatText;
    private Text _magicPowerStatText;
    private Text _armorStatText;
    private Text _magicResistanceStatText;
    private Text _igniteStatText;
    private Text _chillStatText;
    private Text _lightingStatText;
    private Text _agilityStatText;
    private Text _intelligenceStatText;
    private Text _strengthStatText;
    private Text _vitalityStatText;
    private Text _criticalStatText;
    private Text evasionStatText;

    protected override void ParseComponent()
    {
        _maxHealthStatText = FindComponent<Text>("Middle/Left_Panel/Stats/Stat_MaxHealth/Value");
        _damageStatText = FindComponent<Text>("Middle/Left_Panel/Stats/Stat_Damage/Value");
        _magicPowerStatText = FindComponent<Text>("Middle/Left_Panel/Stats/Stat_MagicDamage/Value");
        _armorStatText = FindComponent<Text>("Middle/Left_Panel/Stats/Stat_Armor/Value");
        _magicResistanceStatText = FindComponent<Text>("Middle/Left_Panel/Stats/Stat_MagicResistance/Value");
        _igniteStatText = FindComponent<Text>("Middle/Left_Panel/Stats/Stat_Ignite/Value");
        _chillStatText = FindComponent<Text>("Middle/Left_Panel/Stats/Stat_Chill/Value");
        _lightingStatText = FindComponent<Text>("Middle/Left_Panel/Stats2/Stat_Lighting/Value");
        _agilityStatText = FindComponent<Text>("Middle/Left_Panel/Stats2/Stat_Agility/Value");
        _intelligenceStatText = FindComponent<Text>("Middle/Left_Panel/Stats2/Stat_Intelligence/Value");
        _strengthStatText = FindComponent<Text>("Middle/Left_Panel/Stats2/Stat_Strength/Value");
        _vitalityStatText = FindComponent<Text>("Middle/Left_Panel/Stats2/Stat_Vtality/Value");
        _criticalStatText = FindComponent<Text>("Middle/Left_Panel/Stats2/Stat_Critical/Value");
        evasionStatText = FindComponent<Text>("Middle/Left_Panel/Stats2/Stat_Evasion/Value");

        _closeBtn = FindComponent<Button>("Middle/TopBar/Button_Home");
        _inventoryScrollRect = FindComponent<ScrollRect>("Middle/Right_Panel/ScrollRect");

        _equpSlotImage = FindComponent<Image>("Middle/Left_Panel/Character/EquipSlot_L/EquipFrameEmpty/Icon");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        EventDispatcher.Equip += Equip;
        EventDispatcher.UnEquip += UnEquip;
        RegisterButtonEvent(_closeBtn, OnClickCloseBtn);
    }

    public override void Show()
    {
        base.Show();
        CreateInventoryItemViews();
        RefreshStat();
    }

    private void RefreshStat()
    {
        PlayerStats playerStats = PlayerManager.Instance.player.playerStats;
        _maxHealthStatText.text = playerStats.maxHealth.GetValue().ToString();
        _damageStatText.text = playerStats.attackPower.GetValue().ToString();
        _magicPowerStatText.text = playerStats.magicPower.GetValue().ToString();
        _armorStatText.text = playerStats.armor.GetValue().ToString();
        _magicResistanceStatText.text = playerStats.magicResistance.GetValue().ToString();
        _igniteStatText.text = playerStats.ignite.GetValue().ToString();
        _chillStatText.text = playerStats.chill.GetValue().ToString();
        _lightingStatText.text = playerStats.lighting.GetValue().ToString();
        _agilityStatText.text = playerStats.agility.GetValue().ToString();
        _intelligenceStatText.text = playerStats.intelligence.GetValue().ToString();
        _strengthStatText.text = playerStats.strength.GetValue().ToString();
        _vitalityStatText.text = playerStats.vitality.GetValue().ToString();
        _criticalStatText.text = playerStats.criticalPower.GetValue().ToString();
        evasionStatText.text = playerStats.evasion.GetValue().ToString();
    }

    public override void Hide()
    {
        DisposeInventoryItemViews();

        base.Hide();
    }

    private void Equip(InventoryItemBaseSO itemSO)
    {
        switch (itemSO.itemBaseType)
        {
            case E_InventoryItemBase.Equipment:
                RemoveInventoryItemViewByItemSO(itemSO);
                _equpSlotImage.sprite = itemSO.sprite;
                RefreshStat();
                break;
            default:
                Debugger.Warning($"点击了未处理的物品类型：[{itemSO.itemBaseType}]，物品名称：{itemSO.name}");
                break;
        }
    }

    private void RemoveInventoryItemViewByItemSO(InventoryItemBaseSO itemSO)
    {
        foreach (var equipment in _equipmentViews)
        {
            if (equipment.itemSO == itemSO)
            {
                int number = int.Parse(equipment.numberText.text);
                if (number - 1 == 0)
                {
                    _equipmentViews.Remove(equipment);
                    equipment.Dispose();
                    return;
                }
                else
                {
                    equipment.numberText.text = (number - 1).ToString();
                }
            }
        }
    }

    private void UnEquip(InventoryItemBaseSO itemSO)
    {
        switch (itemSO.itemBaseType)
        {
            case E_InventoryItemBase.Equipment:
                if (AddInventoryItemViewByItemSO(itemSO) == false) CreateEquipmentViewByItemSO(itemSO);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private bool AddInventoryItemViewByItemSO(InventoryItemBaseSO itemSO)
    {
        foreach (var equipment in _equipmentViews)
        {
            if (equipment.itemSO == itemSO)
            {
                equipment.numberText.text = (int.Parse(equipment.numberText.text) + 1).ToString();
                return true;
            }
        }

        return false;
    }

    #region Create Inventory Item View

    private void CreateInventoryItemViews()
    {
        CreateConsumaleViews();
        CreateMaterialViews();
        CreateItemViews();
        CreateEquipmentViews();
    }

    private void CreateEquipmentViews()
    {
        var equipments = InventoryManager.Instance.equipments;
        foreach (var equipment in equipments)
        {
            CreateEquipmentView(equipment);
        }
    }

    private void CreateEquipmentView(KeyValuePair<E_InventoryEquipment, InventoryItem> equipment)
    {
        var obj = ResourceLoader.Instance.LoadObjFromResources("UI/InventoryItemView");
        UnityObjectHelper.Instance.SetParent(obj.transform, _inventoryScrollRect.content);
        InventoryItemView inventoryItemView = new InventoryItemView();
        inventoryItemView.SetDisplayObject(obj);
        inventoryItemView.Initialise(equipment.Value.itemSO, equipment.Value.number);
        _equipmentViews.Add(inventoryItemView);
    }

    private void CreateEquipmentViewByItemSO(InventoryItemBaseSO itemSO)
    {
        var obj = ResourceLoader.Instance.LoadObjFromResources("UI/InventoryItemView");
        UnityObjectHelper.Instance.SetParent(obj.transform, _inventoryScrollRect.content);
        InventoryItemView inventoryItemView = new InventoryItemView();
        inventoryItemView.SetDisplayObject(obj);
        inventoryItemView.Initialise(itemSO, 1);
        _equipmentViews.Add(inventoryItemView);
    }

    private void CreateItemViews()
    {
        var items = InventoryManager.Instance.items;
        foreach (var item in items)
        {
            CreateItemView(item);
        }
    }

    private void CreateItemView(KeyValuePair<E_InventoryItem, InventoryItem> item)
    {
        var obj = ResourceLoader.Instance.LoadObjFromResources("UI/InventoryItemView");
        UnityObjectHelper.Instance.SetParent(obj.transform, _inventoryScrollRect.content);
        InventoryItemView inventoryItemView = new InventoryItemView();
        inventoryItemView.SetDisplayObject(obj);
        inventoryItemView.Initialise(item.Value.itemSO, item.Value.number);
        _itemViews.Add(inventoryItemView);
    }

    private void CreateMaterialViews()
    {
        var materials = InventoryManager.Instance.materials;
        foreach (var material in materials)
        {
            CreateMaterialView(material);
        }
    }

    private void CreateMaterialView(KeyValuePair<E_InventoryMaterial, InventoryItem> material)
    {
        var obj = ResourceLoader.Instance.LoadObjFromResources("UI/InventoryItemView");
        UnityObjectHelper.Instance.SetParent(obj.transform, _inventoryScrollRect.content);
        InventoryItemView inventoryItemView = new InventoryItemView();
        inventoryItemView.SetDisplayObject(obj);
        inventoryItemView.Initialise(material.Value.itemSO, material.Value.number);
        _materialViews.Add(inventoryItemView);
    }

    private void CreateConsumaleViews()
    {
        var consumables = InventoryManager.Instance.consumables;
        foreach (var consumable in consumables)
        {
            CreateConsumableView(consumable);
        }
    }

    private void CreateConsumableView(KeyValuePair<E_InventoryConsumable, InventoryItem> consumable)
    {
        var obj = ResourceLoader.Instance.LoadObjFromResources("UI/InventoryItemView");
        UnityObjectHelper.Instance.SetParent(obj.transform, _inventoryScrollRect.content);
        InventoryItemView inventoryItemView = new InventoryItemView();
        inventoryItemView.SetDisplayObject(obj);
        inventoryItemView.Initialise(consumable.Value.itemSO, consumable.Value.number);
        _consumableViews.Add(inventoryItemView);
    }

    #endregion

    #region Dispose Inventory Item Views

    private void DisposeInventoryItemViews()
    {
        DisposeEquipmentViews();
        DisposeConsumableViews();
        DisposeMaterialViews();
        DisposeItemViews();
    }

    private void DisposeEquipmentViews()
    {
        foreach (var itemView in _equipmentViews)
        {
            itemView.Dispose();
        }

        _equipmentViews.Clear();
    }

    private void DisposeConsumableViews()
    {
        foreach (var itemView in _consumableViews)
        {
            itemView.Dispose();
        }

        _consumableViews.Clear();
    }

    private void DisposeMaterialViews()
    {
        foreach (var itemView in _materialViews)
        {
            itemView.Dispose();
        }

        _materialViews.Clear();
    }

    private void DisposeItemViews()
    {
        foreach (var itemView in _itemViews)
        {
            itemView.Dispose();
        }

        _itemViews.Clear();
    }

    #endregion

    private void OnClickCloseBtn()
    {
        TimeManager.Instance.ResumeTime();
        Hide();
    }

    protected override void RemoveEvent()
    {
        EventDispatcher.Equip -= Equip;
        EventDispatcher.UnEquip -= UnEquip;
        UnRegisterButtonEvent(_closeBtn, OnClickCloseBtn);
        base.RemoveEvent();
    }
}