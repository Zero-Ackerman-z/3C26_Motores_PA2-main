using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CandyGeneratorData", menuName = "ScriptableObjects/CandyGeneratorData", order = 1)]
public class ObjectsGeneratorData : ScriptableObject
{
    public float timeToCreate = 4f;
    public float limitSuperior;
    public float limitInferior;
    public GameObject[] Objects;
}