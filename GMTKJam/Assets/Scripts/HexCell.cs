using UnityEngine;

public class HexCell : MonoBehaviour {

	public HexCoordinates coordinates;

    public GameObject EnemyPrefab;
	public Color colour;
    private UnitTypes targetUT;

    void Start()
    {
        SpawnRandomEnemy();
    }
    public void SpawnRandomEnemy()
    {
        if (!OverallHexCoordsDict.GameDictionary.TryGetValue(coordinates, out targetUT))
        {
            int chanceToSpawnEnemy = Random.Range(0, 10);
            if(chanceToSpawnEnemy >= 8)
            {
                Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}