using System;
using CODE_OF_STORY.Core;
using CODE_OF_STORY.Scenes.Gateway;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CODE_OF_STORY.Managers;

internal partial class GatewaysManager : Component
{
    private StoneAge stoneAge = new StoneAge();
    private MiddleAge middleAge= new MiddleAge();
    private ModernAge modernAge= new ModernAge();
    private Future future = new Future();

    internal override void LoadContent(ContentManager Content)
    {
        stoneAge.LoadContent(Content);
        middleAge.LoadContent(Content);
        modernAge.LoadContent(Content);
        future.LoadContent(Content);

    }

    internal override void Update(GameTime gameTime)
    {
        switch (Data.currentState)
        {
            case Data.Scenes.StoneAge:
                stoneAge.Update(gameTime);
                break;
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
