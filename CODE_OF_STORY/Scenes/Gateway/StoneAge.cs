using System;
using CODE_OF_STORY.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static CODE_OF_STORY.Core.Data;

namespace CODE_OF_STORY.Scenes.Gateway;

internal class StoneAge : Component
{
    private Player player;
    private Enemy enemy;
    private Gem gem;
    private PausePopupMenu pausePopupMenu;

    private bool popUpMenuFired = false;

    public StoneAge()
    {
        pausePopupMenu = new PausePopupMenu();
    }

    internal override void LoadContent(ContentManager Content)
    {
        Texture2D runTexture = Content.Load<Texture2D>("Player_Level1/Warrior_1/Run");
        Texture2D idleTexture = Content.Load<Texture2D>("Player_Level1/Warrior_1/Idle");
        Texture2D jumpAnimation = Content.Load<Texture2D>("Player_Level1/Warrior_1/Jump2");
        Texture2D attackAnimation = Content.Load<Texture2D>("Player_Level1/Warrior_1/Attack_1");
        Texture2D enRunTexture = Content.Load<Texture2D>("Player_Level1/Warrior_2/Run");
        Texture2D gemTexture = Content.Load<Texture2D>("Items/Gems/plate32x8");

        player = new Player(runTexture, idleTexture, jumpAnimation, attackAnimation, new Vector2(100, 600));
        enemy = new EnemyCharge(enRunTexture, new Vector2(400, 600), new Vector2(700, 600), 100f, 100f, 100, 300f);
        gem = new Gem(gemTexture, new Vector2(300, 600));

        pausePopupMenu.LoadContent(Content);

    }

    internal override void Update(GameTime gameTime)
    {
        pausePopupMenu.Update(gameTime);

        if (currentGameState == GameState.Playing)
        {
            if (player != null && gem != null && enemy != null)
            {
                player.Update(gameTime);
                gem.Update(gameTime);
                enemy.Update(gameTime, player.Position);
            }
            else
            {
                Console.WriteLine("The player or gem or enemy is null");
            }
        }

        // check button state
        if (!this.popUpMenuFired) { 
            if (Keyboard.GetState().IsKeyDown(Keys.P) || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.popUpMenuFired = true;
                if (currentGameState == GameState.Playing)
                {
                    currentGameState = GameState.Paused;
                }
                else if (currentGameState == GameState.Paused)
                {
                    currentGameState = GameState.Playing;
                }
            }
        } else
        {
            if (Keyboard.GetState().IsKeyUp(Keys.P) && Keyboard.GetState().IsKeyUp(Keys.Escape))
            {
                this.popUpMenuFired = false;
            }
        }

    }
    internal override void Draw(SpriteBatch spriteBatch)
    {
        if (currentGameState == GameState.Playing)
        {
            if (player != null)
            {
                player.Draw(spriteBatch);
                gem.Draw(spriteBatch);
                enemy.Draw(spriteBatch);
            }
        }
        else if (currentGameState == GameState.Paused)
        {
            pausePopupMenu.Draw(spriteBatch);
        }


    }
}
