using UnityEngine;

public class ObstaclesSpawn : MonoBehaviour {

    public Transform[] spawnPoint;
    public GameObject collider;
    public GameObject item;
    public GameObject colliderPole;
    public float startTime;
    public float spawnTime = 0f;
    public float startWaveTime = 2f;
    float waveTime;
    public float timeincrease = 0.04f;
    int randindex, randindex_old = 0, spawnlimit;
    GameManager manager;
    bool itemspawned;
    bool polespawned;
    bool[] spawnslotused;
    Vector3 spawnpos;
    

	void Start () {
        manager = FindObjectOfType<GameManager>();
        waveTime = startWaveTime;
        startTime = Time.time;
        manager.runningGame = false;
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (manager.runningGame)
        {
            if ((Time.time - startTime) >= spawnTime)
            {
                SpawnColliders();
                spawnTime += waveTime;
                Time.timeScale += timeincrease;
            }
        }
	}

    public void newGame() {
        FindObjectOfType<ObstaclesSpawn>().startTime = Time.time; // equal to this.startTime = Time.time?
        waveTime = startWaveTime;
        spawnTime = 0f;
        startTime = Time.time;
    }

    

    void SpawnColliders()
    {
        int[] spawnslotused = new int[3];
        spawnslotused[0] = 0; 
        spawnslotused[1] = 0; 
        spawnslotused[2] = 0;
        int blocksspawned = 0;

        itemspawned = false;
        polespawned = false;
        spawnlimit = 2;
        for (int i = 0; i <= manager.turn / 10 && i < spawnlimit; i++) //i represents the number of obstacles or items to spawn in a single wave
        {
            randindex_old = randindex;
            do
            {
                randindex = Random.Range(0, spawnPoint.Length);
            } while (spawnslotused[randindex] == 1);
            //while(randindex_old == randindex);
        
            if (manager.checkforitem() && !itemspawned) //Spawn an Item
            {
                Instantiate(item, spawnPoint[randindex].position, Quaternion.identity);
                itemspawned = true;
                spawnlimit++;
                spawnslotused[randindex] = 1;

            }
            else
            {
                if ((Random.value <= manager.propability() || blocksspawned > 1 ) && !polespawned) //Spawn a High Pole
                {
                    spawnpos = spawnPoint[randindex].position;
                    spawnpos.y = 1.4f;
                    spawnpos.x--;
                    Instantiate(colliderPole, spawnpos, Quaternion.AngleAxis(90, new Vector3(0, 1, 0)));
                    polespawned = true;
                    spawnslotused[randindex] = 1;
                }
                else //Spawn a Block
                {
                    Instantiate(collider, spawnPoint[randindex].position, Quaternion.AngleAxis(90,new Vector3(0,1,0)));
                    spawnslotused[randindex] = 1;
                    blocksspawned++;
                }
            }
        }
        manager.turn++;
    }
}
