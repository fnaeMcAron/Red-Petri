using UnityEngine;

public class GeneUI : MonoBehaviour
{
    public GeneManager geneManager;

    public void MutateSkin()
    {
        geneManager.MutateGene(GeneType.Skin, 1);
    }

    public void MutateLungs()
    {
        geneManager.MutateGene(GeneType.Lungs, 1);
    }

    public void MutatePsyche()
    {
        geneManager.MutateGene(GeneType.Psyche, 1);
    }
}
