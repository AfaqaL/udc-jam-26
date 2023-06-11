using UnityEngine;

[System.Serializable]
public class SizingBullet : MonoBehaviour
{
    public float SizingSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(transform.localScale.x + (Time.deltaTime * SizingSpeed), transform.localScale.y + (Time.deltaTime * SizingSpeed));
    }
}
