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
    private LoadScene loadScene= new LoadScene();
    private SettingsScene settingsScene= new SettingsScene();
    private GatewaysScene gatewaysScene= new GatewaysScene();
    internal override void LoadContent(ContentManager Content)
    {
        menueScene.LoadContent(Content);
        loadScene.LoadContent(Content);
        settingsScene.LoadContent(Content);
        gatewaysScene.LoadContent(Content);
    }

    internal override void Update(GameTime gameTime)
    {
        switch (Data.currentState)
        {
            case Data.Scenes.Menu:
                menueScene.Update(gameTime);
                break;
            case Data.Scenes.Load:
                loadScene.Update(gameTime);
                break;
            case Data.Scenes.Settings:
                settingsScene.Update(gameTime);
                break;
            case Data.Scenes.Gateways:
                gatewaysScene.Update(gameTime);
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
            case Data.Scenes.Load:
                loadScene.Draw(spriteBatch);
                break;
            case Data.Scenes.Settings:
                settingsScene.Draw(spriteBatch);
                break;
            case Data.Scenes.Gateways:
                gatewaysScene.Draw(spriteBatch);
                break;
        }
    }
}
