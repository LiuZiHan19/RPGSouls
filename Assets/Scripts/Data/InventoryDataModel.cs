using System.Collections.Generic;
using LitJson;
using UnityEngine.Events;

public class InventoryDataModel : JsonModel
{
    public string weapon;
    public List<string> equiomentIDList = new List<string>();
    public List<int> equipmentNumberList = new List<int>();

    public override void ParseData(JsonData jsonData, UnityAction callback = null)
    {
        base.ParseData(jsonData);

        if (jsonData == null)
        {
            Debugger.Warning($"[InventoryData] jsonData is null in {nameof(ParseData)} | Class: {GetType().Name}");
            return;
        }

        ParseWeapon(jsonData);

        ParseEquipmentData(jsonData);
    }

    private void ParseWeapon(JsonData jsonData)
    {
        if (jsonData.Keys.Contains("weapon") && jsonData["weapon"] != null)
        {
            weapon = jsonData["weapon"].ToString();
        }

        InventoryManager.Instance.weapon = InventoryManager.Instance.LoadDataByGUID(weapon) as InventoryEquipmentSO;
    }

    private void ParseEquipmentData(JsonData jsonData)
    {
        if (jsonData.Keys.Contains("equipments") && jsonData["equipments"] != null && jsonData["equipments"].Count > 0)
        {
            for (int i = 0; i < jsonData["equipments"].Count; i++)
            {
                if (jsonData["equipments"][i].Keys.Contains("guid") && jsonData["equipments"][i]["guid"] != null)
                {
                    equiomentIDList.Add(jsonData["equipments"][i]["guid"].ToString());
                }

                if (jsonData["equipments"][i].Keys.Contains("number") && jsonData["equipments"][i]["number"] != null)
                {
                    equipmentNumberList.Add(int.Parse(jsonData["equipments"][i]["number"].ToString()));
                }
            }
        }

        for (int i = 0; i < equiomentIDList.Count; i++)
        {
            var so = InventoryManager.Instance.LoadDataByGUID(equiomentIDList[i]);
            InventoryManager.Instance.equipmentDict.Add((so as InventoryEquipmentSO).equipmentType,
                new InventoryItem(so, equipmentNumberList[i]));
        }
    }

    public override JsonData GetSaveJsonData()
    {
        JsonData saveJson = new JsonData();

        saveJson["weapon"] = InventoryManager.Instance.weapon == null ? "" : InventoryManager.Instance.weapon.id;

        var equipmentDict = InventoryManager.Instance.equipmentDict;
        JsonData equipmentListJson = new JsonData();
        equipmentListJson.SetJsonType(LitJson.JsonType.Array);
        foreach (var kv in equipmentDict)
        {
            JsonData equipmentJson = new JsonData();
            equipmentJson["guid"] = kv.Value.itemSO.id;
            equipmentJson["number"] = kv.Value.number;
            equipmentListJson.Add(equipmentJson);
        }

        saveJson["equipments"] = equipmentListJson;

        return saveJson;
    }
}