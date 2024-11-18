using System;
using System.Collections;
using CODE_OF_STORY.Core;
using CODE_OF_STORY.Scenes.Gateway;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static CODE_OF_STORY.Core.Data;

namespace CODE_OF_STORY.Managers;

internal partial class GatewaysManager : Component
{
    private StoneAge stoneAge;
    private MiddleAge middleAge;
    private ModernAge modernAge;
    private Future future;

    public GatewaysManager()
    {
        stoneAge = new StoneAge();
        middleAge = new MiddleAge();
        modernAge = new ModernAge();
        future = new Future();
    }
    internal override void LoadContent(ContentManager Content)
    {
        stoneAge.LoadContent(Content);
        middleAge.LoadContent(Content);
        modernAge.LoadContent(Content);
        future.LoadContent(Content);

    }

    public void RestartCurrentLevel()
    {
        switch (Data.currentState)
        {
            case Data.Scenes.StoneAge:
                stoneAge.Reset();
                break;
            case Data.Scenes.MiddleAge:
                ResetMiddleAge();
                break;
            case Data.Scenes.ModernAge:
                ResetModernAge();
                break;
            case Data.Scenes.Future:
                ResetFuture();
                break;
        }
    }

    /* private void ResetStoneAge()
    {
        stoneAge = null;
        stoneAge = new StoneAge(); // Re-instantiate stoneAge
    } */

    private void ResetMiddleAge() { }
    private void ResetModernAge() { }
    private void ResetFuture() { }

    internal override void Update(GameTime gameTime)
    {
        switch (Data.currentState)
        {
            case Data.Scenes.StoneAge:
                {
                    stoneAge.Update(gameTime);
                    Console.WriteLine("Is the player alive? " + Player.checkIsAlive);
                    break;
                }
            case Data.Scenes.MiddleAge:
                middleAge.Update(gameTime);
                break;
            case Data.Scenes.ModernAge:
                modernAge.Update(gameTime);
                break;
            case Data.Scenes.Future:
                future.Update(gameTime);
                break;
        }
    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
        switch (Data.currentState)
        {
            case Data.Scenes.StoneAge:
                stoneAge.Draw(spriteBatch);
                break;
            case Data.Scenes.MiddleAge:
                middleAge.Draw(spriteBatch);
                break;
            case Data.Scenes.ModernAge:
                modernAge.Draw(spriteBatch);
                break;
            case Data.Scenes.Future:
                future.Draw(spriteBatch);
                break;
        }
    }
}
