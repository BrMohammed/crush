using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Criation_new_map : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Texture2D ImageRaw;
    [SerializeField]
    private GameObject cube;
    GameObject c;

    private void Start()
    {
        Color[] pixels = ImageRaw.GetPixels();


        int wordY = ImageRaw.height;
        int wordX = ImageRaw.width;

        Vector3[] Spawnositions = new Vector3[pixels.Length];
        Vector3 startingswapposition = new Vector3(-Mathf.Round(wordX / 2), -Mathf.Round(wordY / 2), 0);
        Vector3 currentSpawnposetion = startingswapposition;

        int counter = 0;

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
                GameObject c = Instantiate(cube, pos, cube.transform.rotation);
                c.GetComponent<MeshRenderer>().material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                c.GetComponent<MeshRenderer>().material.color = new Color(cc.r, cc.g, cc.b, 0.2f);
                GameObject child = c.transform.GetChild(0).gameObject;
                child.GetComponent<TextMeshPro>().text = "2";
                child = c.transform.GetChild(1).gameObject;
                child.GetComponent<TextMeshPro>().text = "2";
            }
            counter++;
        }
    }

}
