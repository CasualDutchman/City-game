using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public static EnemyManager instance;

    List<Enemy> enemies = new List<Enemy>();
    public GameObject enemyPrefab;

	void Awake () {
        instance = this;
	}
	
	public Enemy SpawnEnemy(Vector3 pos) {
        GameObject go = Instantiate(enemyPrefab);
        go.transform.position = pos;

        enemies.Add(go.GetComponent<Enemy>());

        return go.GetComponent<Enemy>();
    }

    public Enemy FromPosition(Vector3 pos, float radius) {
        foreach (Enemy enemy in enemies) {
            bool betweenX = Mathf.Abs(enemy.transform.position.x - pos.x) < radius;
            bool betweenZ = Mathf.Abs(enemy.transform.position.z - pos.z) < radius;

            if (betweenX && betweenZ) {
                return enemy;
            }
        }

        return null;
    }

    public void RemoveEnemyAtChunk(Vector2 chunkPos) {
        Enemy enemy = FromPosition(new Vector3(chunkPos.x * Worldmanager.instance.TileSize, 0, chunkPos.y * Worldmanager.instance.TileSize), Worldmanager.instance.TileSize);
        if (enemy != null && !MissionManager.instance.IsMissionObjective(enemy.transform)) {
            GameObject go = enemy.gameObject;
            enemies.Remove(enemy);
            Destroy(go);
        }
    }

    public void RemoveEnemy(Enemy enemy) {
        enemies.Remove(enemy);
    }
}
