using LitJson;
using UnityEngine.Events;

public class SkillDataModel : JSONDataModel
{
    public SkillItemDataModel skillRollData = new SkillItemDataModel();
    public SkillItemDataModel skillIdleBlockData = new SkillItemDataModel();
    public SkillItemDataModel skillCloneData = new SkillItemDataModel();

    public override void ParseData(JsonData jsonData, UnityAction callback = null)
    {
        if (jsonData == null)
        {
            Debugger.Warning($"[SkillData] jsonData is null in {nameof(ParseData)} | Class: {GetType().Name}");
            return;
        }

        skillRollData.skillID = SkillID.Roll;
        skillIdleBlockData.skillID = SkillID.IdleBlock;
        skillCloneData.skillID = SkillID.Clone;

        if (jsonData.Keys.Contains("skillRoll") && base.jsonData["skillRoll"] != null)
        {
            skillRollData.isUlocked = (int)jsonData["skillRoll"] == 1;
        }

        if (jsonData.Keys.Contains("skillIdleBlock") && jsonData["skillIdleBlock"] != null)
        {
            skillIdleBlockData.isUlocked = (int)jsonData["skillIdleBlock"] == 1;
        }

        if (jsonData.Keys.Contains("skillClone") && jsonData["skillClone"] != null)
        {
            skillCloneData.isUlocked = (int)jsonData["skillClone"] == 1;
        }
    }

    public override JsonData GetSaveJsonData()
    {
        JsonData saveJson = new JsonData();
        saveJson["skillRoll"] = skillRollData.isUlocked ? 1 : 0;
        saveJson["skillIdleBlock"] = skillIdleBlockData.isUlocked ? 1 : 0;
        saveJson["skillClone"] = skillCloneData.isUlocked ? 1 : 0;
        return saveJson;
    }
}