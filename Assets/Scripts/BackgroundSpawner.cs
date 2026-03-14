using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPoolerBackgroundObjects objectPoolerBackgroundObjects;
    [SerializeField] private float spawnRangeDown = -2.7f;
    [SerializeField] private float spawnRangeUp = 2.7f;
    [SerializeField] private float spawnRateRangeDown = 0f;
    [SerializeField] private float spawnRateRangeUp = 0.05f;
    [SerializeField, Range(0f, 1.5f)] private float objectMoveSpeedEasingFactor = 0.4f;
    [SerializeField] private float objectMoveSpeedFactor = 1f;
    [SerializeField] private float objectMoveSpeedRandomFactor = 0.1f;

    private float randomIntFrequency = 0.5f;

    void Start()
    {
        
    }


    float timer = 0f;
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= randomIntFrequency)
        {
            timer = 0f;

            randomIntFrequency = Random.Range(spawnRateRangeDown, spawnRateRangeUp);
            
            GameObject obj =  objectPoolerBackgroundObjects.GetPooledObject();
            float yPosition = Random.Range(spawnRangeDown, spawnRangeUp);
            obj.transform.position = transform.position + new Vector3(0, yPosition, 0);
            BacgroundObject bacgroundObject = obj.GetComponent<BacgroundObject>();

            bacgroundObject.speed = Mathf.Abs(yPosition) * objectMoveSpeedEasingFactor + Random.Range(objectMoveSpeedFactor, objectMoveSpeedFactor + objectMoveSpeedRandomFactor); 
            obj.SetActive(true);
        }
    }
}
