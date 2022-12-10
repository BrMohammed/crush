using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class endles_creation : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    const int wordY = 12;
    const int wordX = 12;
    const int row_count = 12;
    int count_of_cubes = 0;
    int counter = 0;
    const int time_to_move = 1;
    Vector3[] SpawnPositions;
    float index = time_to_move;
    void Awake()
    {
        Instantiate_cubs();

    }

    // Update is called once per frame
    void Update()
    {
        if (time_to_move <= index)
        {
            StartCoroutine(Mov_parent_on_y());
        }
        index += Time.deltaTime;
        //Debug.Log(index);
         
    }

    private IEnumerator Mov_parent_on_y()
    {
        
        index = 0;
        yield return new WaitForSeconds(time_to_move);
        transform.position = new Vector3(transform.position.x,
                                                        transform.position.y - 1, 0);
        Instantiate_cubs();
    }

    private void Instantiate_cubs()
    {
        count_of_cubes = 0;
        SpawnPositions = new Vector3[row_count];
        Vector3 startingswapposition = new Vector3(-6, 5, 0);
        Vector3 currentSpawnposetion = startingswapposition;
        counter = 0;
        for (int x = 0; x < wordY; x++)
        {
            SpawnPositions[counter] = currentSpawnposetion;
            counter++;
            currentSpawnposetion.x++;
        }
        float HtoR = UnityEngine.Random.Range(0, 270);
        foreach (Vector3 pos in SpawnPositions)
        {
            int i = UnityEngine.Random.Range(0, 3);
            if (i != 0)
            {
                GameObject c = Instantiate(cube, pos, cube.transform.rotation,transform);
                c.GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(HtoR / 360, 1, 70f / 100);
                MeshRenderer renderer = c.GetComponent<MeshRenderer>();
                Material material = renderer.material;
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", Color.HSVToRGB(HtoR / 360, 1, 70f / 100));
                GameObject child = c.transform.GetChild(0).gameObject;
                child.GetComponent<TextMeshPro>().text = "";
                child = c.transform.GetChild(1).gameObject;
                child.GetComponent<TextMeshPro>().text = "";
            }
        }
    }
}
