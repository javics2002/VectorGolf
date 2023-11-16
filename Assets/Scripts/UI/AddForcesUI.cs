using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForcesUI : MonoBehaviour
{
    public LevelData levelData;

    [SerializeField]
    private GameObject scalarPrefab;

    [SerializeField]
    private GameObject vectorialPrefab;

    void Start()
    {
        // Create prefabs
        for (int i = 0; i < levelData.scalars.Count; i++)
        {
            // Instantiate object as children of this (canvas)
            GameObject newObj = Instantiate(scalarPrefab, gameObject.transform);
            newObj.GetComponent<ScalarForce>().SetScalarForce((int) levelData.scalars[i]);
        }

        for (int i = 0; i < levelData.vectors.Count; i++)
        {
            // Instantiate object as children of this (canvas)
            GameObject newObj = Instantiate(vectorialPrefab, gameObject.transform);
            newObj.GetComponent<VectorForce>().SetVectorialForce(levelData.vectors[i]);
        }
    }
}
