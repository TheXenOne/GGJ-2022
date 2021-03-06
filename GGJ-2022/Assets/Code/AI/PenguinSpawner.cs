using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinSpawner : MonoBehaviour
{
    public float SpawnDelay = 1.0f;
    public GameObject penguin;
    GameObject ply;
    int ThisIsOurMaxChoinks;

    // Start is called before the first frame update
    void Start()
    {
        ply = GameObject.FindGameObjectWithTag("Player");
        if(penguin != null)
        {
            ThisIsOurMaxChoinks = penguin.GetComponent<Chonkfactory>().MaxChonkas;
        }
        StartCoroutine(SpawnPenguin());
    }

    private IEnumerator SpawnPenguin()
    {
        while (true)
        {
            if (penguin != null)
            {
                int i = 0;
                bool penguResult = false;
                while (i < 100 && !penguResult)
                {
                    penguResult = TrySpawnPengy();
                }
            }
            yield return new WaitForSeconds(SpawnDelay);
        }
    }

    bool TrySpawnPengy()
    {
        int randBorder = Random.Range(0, 4);
        Vector2 positionOutOfView = new Vector2();
        float anyRandom = Random.value;
        float outRandomMax = Random.Range(1.1f, 1.5f);
        float outRandomMin = Random.Range(-0.5f, 0.1f);

        switch (randBorder)
        {
            case 0://up
                positionOutOfView.x = anyRandom;
                positionOutOfView.y = outRandomMax;
                break;
            case 1://right
                positionOutOfView.x = outRandomMax;
                positionOutOfView.y = anyRandom;
                break;
            case 2://down
                positionOutOfView.x = anyRandom;
                positionOutOfView.y = outRandomMin;
                break;
            case 3://left
                positionOutOfView.x = outRandomMin;
                positionOutOfView.y = anyRandom;
                break;
        }

        Vector3 v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(positionOutOfView.x, positionOutOfView.y, 0.0f));
        v3Pos.z = -4.351125f;

        LayerMask mask = LayerMask.GetMask("PenguinCollider");
        RaycastHit2D hit = Physics2D.BoxCast(v3Pos, penguin.GetComponent<Collider2D>().bounds.size * 0.5f, 0f, Vector2.zero, mask);
        if (hit)
        {
            return false;
        }

        GameObject newPengu = Instantiate(penguin, v3Pos, Quaternion.identity);

        int randomChonk = Random.Range(0, ThisIsOurMaxChoinks);
        Chonkfactory chedoinks = newPengu.GetComponent<Chonkfactory>();
        if (chedoinks) chedoinks.Chonk = randomChonk;

        return true;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
