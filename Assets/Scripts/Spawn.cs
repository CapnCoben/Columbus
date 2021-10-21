using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Spawn : MonoBehaviour
{
    [Header("Prefab Settings")]
    public List<GameObject> GoldPrefabs = new List<GameObject>();
    public Transform spawnPoint;
    public float horizontalRange = 20;
    public float verticalRange = 20;
    public int goldCount = 10;
    public CanvasGroup VictoryScreen;
    [SerializeField] public TMP_Text currencyText;
    [SerializeField] public TMP_Text goldNum;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <=goldCount; i++)
        {
            Vector3 spawnPos = spawnPoint.position + new Vector3(Random.Range(0, horizontalRange), 0, (Random.Range(0, verticalRange)));

            Instantiate(GoldPrefabs.ToArray()[Random.Range(0, GoldPrefabs.Count)], spawnPos, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
