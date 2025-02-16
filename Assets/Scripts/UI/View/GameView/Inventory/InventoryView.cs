using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryView : UIBehaviour
{
    private Button _closeBtn;
    private ScrollRect _scrollRect;
    private InventorySlotView _equipmentSlotView;
    private InventorySlotView _armorSlotView;
    private InventorySlotView _potionSlotView;
    private InventorySlotView _amuletSlotView;
    private List<InventoryItemView> _equipmentViews = new List<InventoryItemView>();
    private List<InventoryItemView> _consumableViews = new List<InventoryItemView>();
    private List<InventoryItemView> _materialViews = new List<InventoryItemView>();
    private List<InventoryItemView> _itemViews = new List<InventoryItemView>();

    protected override void ParseComponent()
    {
        _closeBtn = FindComponent<Button>("Top/Close");
        _scrollRect = FindComponent<ScrollRect>("Middle/InventoryPanel/Scroll View");

        var equipmentSlotObj = FindGameObject("Middle/PlayerPanel/EquipmentPanel/Equipment");
        _equipmentSlotView = new InventorySlotView();
        _equipmentSlotView.SetDisplayObject(equipmentSlotObj);
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        EventDispatcher.Equip += Equip;
        EventDispatcher.UnEquip += UnEquip;
        RegisterButtonEvent(_closeBtn, OnClickCloseBtn);
    }

    private void Equip(InventoryItemBaseSO itemSO)
    {
        switch (itemSO.itemBaseType)
        {
            case E_InventoryItemBase.Equipment:
                CheckRemoveByItemSO(itemSO);
                _equipmentSlotView.Refresh(itemSO);
                break;
            default:
                Debugger.Warning($"点击了未处理的物品类型：[{itemSO.itemBaseType}]，物品名称：{itemSO.name}");
                break;
        }
    }

    private void CheckRemoveByItemSO(InventoryItemBaseSO itemSO)
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
                if (CheckAddByItemSO(itemSO) == false) CreateEquipmentViewByItemSO(itemSO);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private bool CheckAddByItemSO(InventoryItemBaseSO itemSO)
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

    public override void Show()
    {
        base.Show();
        CreateInventoryItemViews();
    }

    public override void Hide()
    {
        DisposeInventoryItemViews();
        base.Hide();
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
        var equipments = Inventory.Instance.equipments;
        foreach (var equipment in equipments)
        {
            CreateEquipmentView(equipment);
        }
    }

    private void CreateEquipmentView(KeyValuePair<E_InventoryEquipment, InventoryItem> equipment)
    {
        var obj = ResourceLoader.Instance.LoadObjFromResources("UI/InventoryItemView");
        UnityObjectHelper.Instance.SetParent(obj.transform, _scrollRect.content);
        InventoryItemView inventoryItemView = new InventoryItemView();
        inventoryItemView.SetDisplayObject(obj);
        inventoryItemView.Initialise(equipment.Value.itemSO, equipment.Value.number);
        _equipmentViews.Add(inventoryItemView);
    }

    private void CreateEquipmentViewByItemSO(InventoryItemBaseSO itemSO)
    {
        var obj = ResourceLoader.Instance.LoadObjFromResources("UI/InventoryItemView");
        UnityObjectHelper.Instance.SetParent(obj.transform, _scrollRect.content);
        InventoryItemView inventoryItemView = new InventoryItemView();
        inventoryItemView.SetDisplayObject(obj);
        inventoryItemView.Initialise(itemSO, 1);
        _equipmentViews.Add(inventoryItemView);
    }

    private void CreateItemViews()
    {
        var items = Inventory.Instance.items;
        foreach (var item in items)
        {
            CreateItemView(item);
        }
    }

    private void CreateItemView(KeyValuePair<E_InventoryItem, InventoryItem> item)
    {
        var obj = ResourceLoader.Instance.LoadObjFromResources("UI/InventoryItemView");
        UnityObjectHelper.Instance.SetParent(obj.transform, _scrollRect.content);
        InventoryItemView inventoryItemView = new InventoryItemView();
        inventoryItemView.SetDisplayObject(obj);
        inventoryItemView.Initialise(item.Value.itemSO, item.Value.number);
        _itemViews.Add(inventoryItemView);
    }

    private void CreateMaterialViews()
    {
        var materials = Inventory.Instance.materials;
        foreach (var material in materials)
        {
            CreateMaterialView(material);
        }
    }

    private void CreateMaterialView(KeyValuePair<E_InventoryMaterial, InventoryItem> material)
    {
        var obj = ResourceLoader.Instance.LoadObjFromResources("UI/InventoryItemView");
        UnityObjectHelper.Instance.SetParent(obj.transform, _scrollRect.content);
        InventoryItemView inventoryItemView = new InventoryItemView();
        inventoryItemView.SetDisplayObject(obj);
        inventoryItemView.Initialise(material.Value.itemSO, material.Value.number);
        _materialViews.Add(inventoryItemView);
    }

    private void CreateConsumaleViews()
    {
        var consumables = Inventory.Instance.consumables;
        foreach (var consumable in consumables)
        {
            CreateConsumableView(consumable);
        }
    }

    private void CreateConsumableView(KeyValuePair<E_InventoryConsumable, InventoryItem> consumable)
    {
        var obj = ResourceLoader.Instance.LoadObjFromResources("UI/InventoryItemView");
        UnityObjectHelper.Instance.SetParent(obj.transform, _scrollRect.content);
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
        NotifyViewEvent(EventConst.OnClickCloseInventory);
    }

    protected override void RemoveEvent()
    {
        EventDispatcher.Equip -= Equip;
        EventDispatcher.UnEquip -= UnEquip;
        UnregisterButtonEvent(_closeBtn, OnClickCloseBtn);
        base.RemoveEvent();
    }
}