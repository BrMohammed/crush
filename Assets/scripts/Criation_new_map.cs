using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Criation_new_map : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Texture2D[] ImageRaw;
    [SerializeField]
    private GameObject cube;
    [SerializeField]
    private GameObject parent;
    static public int count_of_cubes;
    static public int maps_count;
    int counter;
    Vector3[] Spawnositions;
    Color[] pixels;
    int courent_map ;

    private void Awake()
    {
        courent_map = int.Parse(SimpelDb.read("level")) - 1;
        maps_count = ImageRaw.Length;
        pixels = ImageRaw[courent_map].GetPixels();
        count_of_cubes = 0;
        int wordY = ImageRaw[courent_map].height;
        int wordX = ImageRaw[courent_map].width;

        Spawnositions = new Vector3[pixels.Length];//concatinate all positions
        Vector3 startingswapposition = new Vector3(-Mathf.Round(wordX / 2),
                                                    -Mathf.Round(wordY / 2), 0);
       
        Vector3 currentSpawnposetion = startingswapposition;

        counter = 0;

        for (int y = 0; y < wordY; y++)
        {
            for (int x = 0; x < wordX; x++)
            {
                Spawnositions[counter] = currentSpawnposetion;
                counter++;
                currentSpawnposetion.x++;
            }
            currentSpawnposetion.x = startingswapposition.x;
            currentSpawnposetion.y++;
        }
        counter = 0;
        foreach (Vector3 pos in Spawnositions)
        {
            Color cc = pixels[counter];
            if (cc.a != 0)
            {
                count_of_cubes++;
            }
            counter++;
        }
    }

    public void init_map()
    {
        Awake();
        counter = 0;
        foreach (Vector3 pos in Spawnositions)
        {
            Color cc = pixels[counter];
            if (cc.a != 0)
            {
                GameObject c = Instantiate(cube, pos, cube.transform.rotation, parent.transform);
                //c.GetComponent<MeshRenderer>().material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                c.GetComponent<MeshRenderer>().material.color = new Color(cc.r, cc.g, cc.b, 0.2f);
                MeshRenderer renderer = c.GetComponent<MeshRenderer>();
                Material material = renderer.material;
                material.SetColor("_EmissionColor", cc);
                material.EnableKeyword("_EMISSION");
               
                GameObject child = c.transform.GetChild(0).gameObject;
                child.GetComponent<TextMeshPro>().text = "";
                child = c.transform.GetChild(1).gameObject;
                child.GetComponent<TextMeshPro>().text = "";
            }
            counter++;
        }
    }

    public void Make_map(int Number_of_map)
    {
        maps_count = ImageRaw.Length;
        pixels = ImageRaw[Number_of_map].GetPixels();
        count_of_cubes = 0;


        int wordY = ImageRaw[Number_of_map].height;
        int wordX = ImageRaw[Number_of_map].width;

        Spawnositions = new Vector3[pixels.Length];
        Vector3 startingswapposition = new Vector3(-Mathf.Round(wordX / 2), -Mathf.Round(wordY / 2), 0);
        Vector3 currentSpawnposetion = startingswapposition;

        counter = 0;

        for (int y = 0; y < wordY; y++)
        {
            for (int x = 0; x < wordX; x++)
            {
                Spawnositions[counter] = currentSpawnposetion;
                counter++;
                currentSpawnposetion.x++;
            }
            currentSpawnposetion.x = startingswapposition.x;
            currentSpawnposetion.y++;
        }
        counter = 0;
        foreach (Vector3 pos in Spawnositions)
        {
            Color cc = pixels[counter];
            if (cc.a != 0)
            {
                GameObject c = Instantiate(cube, pos, cube.transform.rotation, parent.transform);
                //c.GetComponent<MeshRenderer>().material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                c.GetComponent<MeshRenderer>().material.color = new Color(cc.r, cc.g, cc.b, 0.2f);
                MeshRenderer renderer = c.GetComponent<MeshRenderer>();
                Material material = renderer.material;
                material.SetColor("_EmissionColor", cc);
                material.EnableKeyword("_EMISSION");
                GameObject child = c.transform.GetChild(0).gameObject;
                child.GetComponent<TextMeshPro>().text = "";
                child = c.transform.GetChild(1).gameObject;
                child.GetComponent<TextMeshPro>().text = "";
                count_of_cubes++;
            }
            counter++;
        }
        timer.timelift = count_of_cubes * 2;
        timer.maxtime = count_of_cubes * 2;
    }

}
