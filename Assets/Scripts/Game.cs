using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    [System.Serializable]
    public class Wave
    {
        // Note: add to this in the future for any wave attributes
        public int waveSize;
        public int numberOfWaves;
    }

    public enum GameState
    { START, DEFEND, REST, END }

    public GameState currentState;
    public float defendDuration;
    public float restDuration;
    public GameObject[] enemies;
	public GameObject[] enemies0;
	public GameObject[] enemies1;
	public GameObject[] enemies2;
	public GameObject[] enemies3;


    // Information regarding how waves behave (i.e. What and How Many enemies gets spawned)
	[Header("Current Wave Information")]
	public int wave;
    public Wave waveInfo;

    GameObject playerObj;
    PlayerManager playerMngr;
    SpawnPoint[] enemySpawners;

    private float timer;
    private float spawnTimer;
    private int enemiesRemaining;
    private float waveTimer;
	private UI_Game _ui;
	private bool _hasShownAlert;

    // Use this for initialization
    void Start () {
        playerObj = GameObject.Find("Player");
        enemySpawners = GameObject.FindObjectsOfType<SpawnPoint>();

		_ui = GameObject.Find ("PlayerUI").GetComponent<UI_Game> ();

        playerMngr = playerObj.GetComponent<PlayerManager>();

        // Disable the player initially
        playerObj.GetComponent<PlayerMovement>().enabled = false;
		_hasShownAlert = false;

		wave = 1;
        currentState = GameState.START;
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentState)
        {
		case GameState.START:
			
			if (!_hasShownAlert) {
				_ui.ShowAlert ("WAVE " + wave.ToString (), "Press Enter", (wave==1?float.PositiveInfinity:5));
				_hasShownAlert = true;
			}
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    timer = 0.0f;
                    spawnTimer = 0.0f;
                    currentState = GameState.DEFEND;
                    // Enable the player
                    playerObj.GetComponent<PlayerMovement>().enabled = true;
                    waveTimer = (defendDuration / waveInfo.numberOfWaves);

                    Debug.Log("Game State: " + currentState.ToString());
                }
                break;
		case GameState.DEFEND:

                // Update the time
				timer += Time.deltaTime;
				spawnTimer += Time.deltaTime;
				
				_hasShownAlert = false;

                // If the player dies
                if (playerMngr.currentHealth <= 0)
                {
                    currentState = GameState.END;
                    ClearMapEntities();

                    Debug.Log("Game State: " + currentState.ToString());
                }

                // Split wave spawn rate to = duration of wave / number of waves
                if (spawnTimer > waveTimer && timer <= defendDuration)
                {
                    SpawnNextWave();
                    spawnTimer = 0.0f;

                    Debug.Log("Spawning Wave...");
                }

                enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy").Length;

                // End of Defend State
                if (timer > defendDuration && enemiesRemaining <= 0)
                { 
                    timer = 0.0f;
                    ClearMapEntities();
					wave++;
                    currentState = GameState.REST;

					if (wave % 3 == 0) {
						waveInfo.waveSize += 1;
					}
					if (wave % 5 == 0) {
						waveInfo.numberOfWaves += 1;
					}

                    Debug.Log("Game State: " + currentState.ToString());
                }
                break;
            case GameState.REST:
                timer += Time.deltaTime;

                // End of Rest State
                if (timer > restDuration)
                {
                    timer = 0.0f;
                    currentState = GameState.DEFEND;

                    waveTimer = (defendDuration / waveInfo.numberOfWaves);

                    Debug.Log("Game State: " + currentState.ToString());
                }
                break;
		case GameState.END:
			if (!_hasShownAlert) {
				_ui.ShowAlert ("GAME OVER", "Press Enter", float.PositiveInfinity, true);
				_hasShownAlert = true;
			}
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                break;
        }
	}

    void ClearMapEntities()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject e in enemies)
        {
            Destroy(e);
        }
    }

    // Next wave to spawn.
    // NOTE: Number of enemies per wave is also base off of the number of spawners that we have
    //          on the map.
    void SpawnNextWave()
    {
		if (wave >= 1 && wave <= 3) {
			GameObject[] wave = new GameObject[waveInfo.waveSize];
			for (int i = 0; i < waveInfo.waveSize; i++)
			{
				int enemyIdx = (int)UnityEngine.Random.Range(0, enemies.Length);
				wave[i] = enemies[enemyIdx];
			}

			foreach (SpawnPoint sp in enemySpawners)
			{
				sp.SpawnWave(wave);
			}
		}

		if (wave >= 4 && wave <= 6) {
			GameObject[] wave = new GameObject[waveInfo.waveSize];
			for (int i = 0; i < waveInfo.waveSize; i++)
			{
				int enemyIdx = (int)UnityEngine.Random.Range(0, enemies0.Length);
				wave[i] = enemies0[enemyIdx];
			}

			foreach (SpawnPoint sp in enemySpawners)
			{
				sp.SpawnWave(wave);
			}
		}

      

		if (wave >= 7 && wave <= 9) {
			GameObject[] wave = new GameObject[waveInfo.waveSize];
			for (int i = 0; i < waveInfo.waveSize; i++)
			{
				int enemyIdx = (int)UnityEngine.Random.Range(0, enemies1.Length);
				wave[i] = enemies1[enemyIdx];
			}

			foreach (SpawnPoint sp in enemySpawners)
			{
				sp.SpawnWave(wave);
			}
		}

		if (wave >= 10 && wave <= 12) {
			GameObject[] wave = new GameObject[waveInfo.waveSize];
			for (int i = 0; i < waveInfo.waveSize; i++)
			{
				int enemyIdx = (int)UnityEngine.Random.Range(0, enemies2.Length);
				wave[i] = enemies2[enemyIdx];
			}

			foreach (SpawnPoint sp in enemySpawners)
			{
				sp.SpawnWave(wave);
			}
		}

		if (wave >= 13) {
			GameObject[] wave = new GameObject[waveInfo.waveSize];
			for (int i = 0; i < waveInfo.waveSize; i++)
			{
				int enemyIdx = (int)UnityEngine.Random.Range(0, enemies3.Length);
				wave[i] = enemies3[enemyIdx];
			}

			foreach (SpawnPoint sp in enemySpawners)
			{
				sp.SpawnWave(wave);
			}
		}

    }
}
