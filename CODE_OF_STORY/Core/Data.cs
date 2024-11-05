using System;

namespace CODE_OF_STORY.Core;

public static class Data
{
    public static int screenW{ get; set; } = 1600;
    public static int screenH{ get; set;} = 900;
    public static bool Exit{ get; set; } = false;

    public enum Scenes {Menu, NewGame, Load, Settings, Gateways, StoneAge, MiddleAge, ModernAge, Future}
    public static Scenes currentState { get; set; } 
}
