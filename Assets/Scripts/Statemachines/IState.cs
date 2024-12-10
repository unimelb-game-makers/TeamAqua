/*
Handling states as interfaces.
*/
public interface IState
{
    void Enter(); //Called when entering the new state
    void Exit(); //Called when exiting a state
    void Ready(); //Called in the start function
    void Process(); //Called in the update function
}
