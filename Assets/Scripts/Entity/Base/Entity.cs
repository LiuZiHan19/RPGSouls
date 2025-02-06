using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public string id;
    public string entityName;
    public Animator animator;
    public Collider collider;
    public float moveSpeed;
    public float attackRange;
    public bool isFacingRight = true;
    public float facingDir = 1;
    public EntityStats entityStats;

    protected virtual void Awake()
    {
        id = System.Guid.NewGuid().ToString();
        animator = GetComponentInChildren<Animator>();
        collider = GetComponent<Collider>();
        entityStats = GetComponent<EntityStats>();
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    protected virtual void LateUpdate()
    {
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void OnDisable()
    {
    }

    protected virtual void OnDestroy()
    {
    }

    protected virtual void OnApplicationPause(bool pauseStatus)
    {
    }

    protected virtual void OnApplicationFocus(bool hasFocus)
    {
    }

    protected virtual void OnApplicationQuit()
    {
    }

    protected virtual void Die()
    {
        Logger.Info(entityName + "Die.");
    }
}