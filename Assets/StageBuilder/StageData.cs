using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/StageData")]
public class StageData : ScriptableObject
{
    [HideInInspector]
    public List<Tile3D.Block> blocks;

    [Space]
    // Mesh
    public Mesh renderMesh;
    public Mesh colliderMesh;

    [Space]
    // CSV
    public TextAsset data;
}
