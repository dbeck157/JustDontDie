using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour {

    public GameObject zombie;       //The enemy that is instantiated
    public float zombieCount;       //Enemies per wave
    public float spawnWait;         //Time between enemies spawning in a wave
    public float startWait;         //Time before first wave spawns
    public float waveWait;          //Time between waves

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnWaves());
	}
	
	IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while(true)
        {
            for(int i = 0; i < zombieCount; i++)
            {
                Instantiate(zombie, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}
