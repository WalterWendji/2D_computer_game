using System;
using Microsoft.Xna.Framework.Input;
using System.Text.Json.Serialization;

namespace CODE_OF_STORY.Core;

public class ControlSettings
{
    public Keys MoveLeft {get; set;}
    public Keys MoveRight {get; set;}
    public Keys Jump {get; set;}
    public Keys Interact {get; set;}
    public Keys SwitchAttackMode {get; set;}
    public Keys UseItem {get; set;}

    public ControlSettings()
    {
        MoveLeft = Keys.A;
        MoveRight = Keys.D;
        Jump = Keys.Space;
        Interact = Keys.F;
        SwitchAttackMode = Keys.Q;
        UseItem = Keys.E;
    }
    
    public Keys[] ToKeysArray()
    {
        return new Keys[] {MoveLeft, MoveRight, Jump,Interact, SwitchAttackMode, UseItem};
    }

    public int ActionsCount()
    {
        return 6;
    }
}
