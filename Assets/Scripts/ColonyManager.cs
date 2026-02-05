using UnityEngine;

public class ColonyManager : MonoBehaviour
{
    public int population = 100;
    public int potato = 50;
    public int stability = 50;

    public void ApplyTick(GeneManager genes)
    {
        population += genes.GetGene(GeneType.Lungs);
        stability += genes.GetGene(GeneType.Psyche);
        potato += genes.GetGene(GeneType.Skin);

        population = Mathf.Clamp(population, 0, 999);
        potato = Mathf.Clamp(potato, 0, 999);
        stability = Mathf.Clamp(stability, 0, 100);
    }
}
