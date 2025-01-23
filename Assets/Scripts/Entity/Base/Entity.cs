using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public string id;
    public string entityName;
    public Animator animator;
    public Collider collider;

    protected virtual void Awake()
    {
        id = System.Guid.NewGuid().ToString();
        animator = GetComponentInChildren<Animator>();
        collider = GetComponent<Collider>();
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