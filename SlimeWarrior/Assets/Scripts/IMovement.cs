using UnityEngine;

public interface IMovement
{
    //Get the Current Speed
    float GetCurrentSpeed();
    //Setup Movement
    void Setup();
    //Update the Movement for direction and jumping
    void UpdateMove(Vector3 directionToMove, bool isJumping);
    //Manage Death Behaviour
    void OnDeath();
}
