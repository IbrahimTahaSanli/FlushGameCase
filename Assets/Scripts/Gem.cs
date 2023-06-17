using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GemScrObj", order = 1)]
public class Gem : ScriptableObject
{
    [field: SerializeField] public string GemName { get; private set; }
    [field:SerializeField] [field:Min(0.0f)] public float GemBasePrice { get; private set; }
    [field: SerializeField] public Sprite GemSprite { get; private set; }
    [field: SerializeField] public Mesh GemMesh { get; private set; }
    [field: SerializeField] public Material GemMaterial { get; private set; }
}
