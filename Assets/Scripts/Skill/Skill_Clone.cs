using UnityEngine;

public class Skill_Clone : Skill
{
    public GameObject clonePrefab;

    public void Clone(Entity target)
    {
        GameObject playerClone = Instantiate(clonePrefab, target.transform.position + Vector3.right * -target.facingDir,
            Quaternion.identity);
        PlayerCloneController playerCloneController = playerClone.GetComponent<PlayerCloneController>();
        playerCloneController.Attack();
    }
}