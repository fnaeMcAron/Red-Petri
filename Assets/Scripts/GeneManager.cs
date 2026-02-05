using UnityEngine;
using System.Collections.Generic;

public enum GeneType
{
    Skin,
    Lungs,
    Psyche
}

public class GeneManager : MonoBehaviour
{
    public Dictionary<GeneType, int> genes = new();

    void Awake()
    {
        genes[GeneType.Skin] = 0;
        genes[GeneType.Lungs] = 0;
        genes[GeneType.Psyche] = 0;
    }

    public void MutateGene(GeneType type, int amount)
    {
        genes[type] += amount;
        genes[type] = Mathf.Clamp(genes[type], -5, 5);
    }

    public int GetGene(GeneType type)
    {
        return genes[type];
    }
}
