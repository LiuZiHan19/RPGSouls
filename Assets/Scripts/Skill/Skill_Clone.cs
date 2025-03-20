using UnityEngine;

public class Skill_Clone : Skill
{
    public GameObject clonePrefab;

    public override void Release(params object[] parameters)
    {
        base.Release(parameters);
        Entity target = (Entity)parameters[0];
        GameObject playerClone = Instantiate(clonePrefab, target.transform.position + Vector3.right * -target.facingDir,
            Quaternion.identity);
        PlayerCloneController playerCloneController = playerClone.GetComponent<PlayerCloneController>();
        playerCloneController.Attack();
    }
}