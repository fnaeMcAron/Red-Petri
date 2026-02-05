using UnityEngine;

public enum MentalState
{
    Mania,
    Apathy
}

public class MentalStateManager : MonoBehaviour
{
    public MentalState currentState;

    public void RollState()
    {
        currentState = Random.value > 0.5f
            ? MentalState.Mania
            : MentalState.Apathy;
    }

    public int AllowedMutations()
    {
        return currentState == MentalState.Mania ? 2 : 1;
    }
}
