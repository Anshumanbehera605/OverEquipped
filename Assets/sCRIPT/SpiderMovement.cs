using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public float maxZ;
    public float minZ;
    float noiseOffsetX = 0;
    float noiseOffsetY = 0;
    public float frequency = 50;
    Vector2 lastPosition = Vector2.zero;
    public Transform spiderbody;
    public Level1Manager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        noiseOffsetX = Random.Range(0, 1000);
        noiseOffsetY = Random.Range(0, 1000);
    }

    void Update()
    {
        noiseOffsetX += Time.deltaTime * frequency;
        noiseOffsetY += Time.deltaTime * frequency;

        float x = Mathf.PerlinNoise(noiseOffsetX, 0) * 2 - 1;
        float y = Mathf.PerlinNoise(0, noiseOffsetY) * 2 - 1;

        Vector2 currentPosition = new(x, y);
        Vector2 del = currentPosition - lastPosition;
        float angle = 180*Mathf.Atan2(del.y, del.x) / 3.14156f;

        Vector3 localAngles = spiderbody.localEulerAngles;
        localAngles.z = angle + 90;
        spiderbody.localEulerAngles = localAngles;

        transform.localPosition = new Vector3(x, y) * 0.5f;

        lastPosition = currentPosition;
    }

    void OnDestroy()
    {
        gameManager.GameWon();
    }
}
