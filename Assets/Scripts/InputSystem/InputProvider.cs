/*
Input provider script for player control.

This is necessary for things like Dialogue system which restricts player control.
Mainly use for player, but can also possibly use for controlling npcs in separate instances.

Not sure whether to make it a Scriptable object or a Singleton.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "InputProvider", menuName = "ScriptableObjects/InputProvider", order = 3)]
public class InputProvider : ScriptableObject
{
    public bool can_move = true;
    public Vector3 move_input;
    public Vector3 move_velocity;

}
