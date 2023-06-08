using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public GameObject DefaultMagicPrefab;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetDefaultMagicPrefab(GameObject obj)
    {
        DefaultMagicPrefab = obj;
    }

    public GameObject GetDefaultMagicPrefab()
    {
        return DefaultMagicPrefab;
    }
}
