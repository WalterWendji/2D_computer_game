using System;
using CODE_OF_STORY.Core;
using CODE_OF_STORY.Scenes;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CODE_OF_STORY.Managers;

internal partial class GameStateManager : Component
{

    private MenuScene menueScene = new MenuScene();
    private NewGameScene newGameScene = new NewGameScene();
    internal override void LoadContent(ContentManager Content)
    {
        menueScene.LoadContent(Content);
        newGameScene.LoadContent(Content);
    }

    internal override void Update(GameTime gameTime)
    {
        switch (Data.currentState)
        {
            case Data.Scenes.Menu:
                menueScene.Update(gameTime);
                break;
            case Data.Scenes.NewGame:
                newGameScene.Update(gameTime);
                break;
            case Data.Scenes.Settings:
                break;
        }
    }

    internal override void Draw(SpriteBatch spriteBatch)
    {
       switch (Data.currentState)
        {
            case Data.Scenes.Menu:
                menueScene.Draw(spriteBatch);
                break;
            case Data.Scenes.NewGame:
            newGameScene.Draw(spriteBatch);
                break;
            case Data.Scenes.Settings:
                break;
        }
    }
}
