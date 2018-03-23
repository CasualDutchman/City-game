using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EnemyManager))]
public class MissionManager : MonoBehaviour {

    public static MissionManager instance;
    EnemyManager enemyManager;

    //public Camp[] availableCamps;

    public Transform currentEnemyPosition;

    public bool finished = false;

    public RectTransform indicator;
    public GroupMovement player;

    public GameObject selectionMenu;

    public Mission[] missions;

    int currentmission;

    [Header("UI texts")]
    public Text[] missionTexts;
    public string textPreset;

    void Awake() {
        instance = this;
    }

    void Start () {
        enemyManager = GetComponent<EnemyManager>();

        GenerateMissions();

        GetMission();

    }

    void GenerateMissions() {
        missions = new Mission[5];

        for (int i = 0; i < 5; i++) {
            UpdateMission(i);
         }
    }

    void UpdateMission(int i) {
        int xAgo = i * 5 + Random.Range(0, 5);
        int zAgo = i * 5 + Random.Range(0, 5);
        missions[i] = new Mission {
            position = new Vector3(xAgo, 0, zAgo)
        };
        missionTexts[i].text = string.Format(textPreset, (i + 1).ToString(), 100 + i * 100, (Vector3.Distance(player.centerOfWolves, missions[i].position * Worldmanager.instance.TileSize)).ToString("F0"));
    }

    void Update () {
        if (currentEnemyPosition != null && !finished) {
            indicator.localEulerAngles = new Vector3(0, 0, -Quaternion.LookRotation(currentEnemyPosition.position - player.centerOfWolves).eulerAngles.y);
        } else if(finished) {
            indicator.localEulerAngles = new Vector3(0, 0, -Quaternion.LookRotation(transform.position - player.centerOfWolves).eulerAngles.y);
        }
	}

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && finished)
            GetMission();
    }

    public void SelectMission(int k) {
        /*for (int i = 0; i < availableCamps.Length; i++) {
            availableCamps[i].destroyed = false;
            availableCamps[i].health = 100;
        }

        finished = false;
        currentCamp = availableCamps[k];
        */

        currentEnemyPosition = EnemyManager.instance.SpawnEnemy(missions[k].position * Worldmanager.instance.TileSize).transform;

        currentmission = k;
        finished = false;

        selectionMenu.SetActive(false);
        //player.GetComponent<PlayerMovement>().enabled = true;
    }

    public void GetMission() {
        if (finished) {
            selectionMenu.SetActive(true);
            //player.GetComponent<PlayerMovement>().KnobInvisible();
            //player.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    public void EnemyDestroyed(Transform t) {
        if (currentEnemyPosition == t) {
            currentEnemyPosition = null;
            finished = true;

            UpdateMission(currentmission);
            GetMission();
        }
    }

    public bool IsMissionObjective(Transform t) {
        return currentEnemyPosition == t;
    }
}

[System.Serializable]
public class Mission {
    public Vector3 position;
}