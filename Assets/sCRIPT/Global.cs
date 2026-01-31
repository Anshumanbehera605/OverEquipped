using UnityEngine;

public class GlobalReferences : MonoBehaviour
{
    public static GlobalReferences Instance;

    public GameObject insectHitEffect;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
