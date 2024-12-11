using System;
using Microsoft.Xna.Framework.Input;

namespace CODE_OF_STORY.Core;

public static class PlayerControls
{
    public static ControlSettings Settings{get; set;} = new ControlSettings();
    
    public static void UpdateControls(ControlSettings newSettings)
    {
        Settings = newSettings;
    }
}
