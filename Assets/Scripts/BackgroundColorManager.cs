using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorManager : MonoBehaviour
{
    [SerializeField] Material skyboxMaterial = default;

    [SerializeField] List<Color> colors1 = default;
    [SerializeField] List<Color> colors2 = default;

    void Awake()
    {
        skyboxMaterial = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeColor();
            Debug.Log("Change");
        }
    }

    public void ChangeColor()
    {
        StartCoroutine(ChangeColorOverTime(
            colors1[0], colors1[1], 
            colors2[0], colors2[1])
            );
    }

    IEnumerator ChangeColorOverTime(
        Color from1, Color to1,
        Color from2, Color to2
        )
    {
        for (float ft = 0f; ft <= 1; ft += 0.01f)
        {
            skyboxMaterial.SetColor("_Color1", Color.Lerp(to1, from1, ft));
            skyboxMaterial.SetColor("_Color2", Color.Lerp(to2, from2, ft));
            yield return new WaitForSeconds(.1f);
        }
    }
}
