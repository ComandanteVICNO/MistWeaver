using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSuitCaseColor : MonoBehaviour
{

    public GameObject[] suitCaseParts;

    Color randomColor;
    Material prefabMat;

    private void Start()
    {
            ChangeColor();
    }

    public void ChangeColor()
    {
        Color newColor = CreateRandomColor();

        foreach(GameObject part in suitCaseParts)
        {
            Renderer partRenderer = part.GetComponent<Renderer>();
            Material myMaterialInstance = new Material(partRenderer.sharedMaterial);
            partRenderer.material = myMaterialInstance;
            myMaterialInstance.color = newColor;
        }

    }

    public Color CreateRandomColor()
    {
        float randomRed = Random.Range(0f, 1f);
        float randomGreen = Random.Range(0f, 1f);
        float randomBlue = Random.Range(0f, 1f);

        randomColor = new Color(randomRed, randomGreen, randomBlue);
        return randomColor;
    }
    //// Start is called before the first frame update
    //void Start()
    //{
    //    // Create a new material instance for this GameObject
    //    Material myMaterialInstance = new Material(GetComponent<Renderer>().sharedMaterial);

    //    // Assign the new material instance to the Renderer
    //    GetComponent<Renderer>().material = myMaterialInstance;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    // Example: Change color to red when the space key is pressed
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        ChangeMaterialColor(Color.red);
    //    }
    //}

    //// Method to change the material color
    //void ChangeMaterialColor(Color newColor)
    //{
    //    // Get the material of the Renderer
    //    Material myMaterial = GetComponent<Renderer>().material;

    //    // Change the color of the material
    //    myMaterial.color = newColor;
    //}
}
