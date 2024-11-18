using System;
using System.Collections.Generic;
using System.Linq;
using CODE_OF_STORY.Core;
using CODE_OF_STORY.Managers;
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
    private List<Enemy> enemies;
    private Gem gem;

    public static Vector2 playerStartPosition;
    public static Vector2 enemyStartPosition;
    public static Vector2 enemyEndPosition;
    private Vector2 gemStartPosition;

    private PausePopupMenu pausePopupMenu;

    private bool popUpMenuTriggerd;
    private bool popUpMenuFired;
    private double deathTimer;
    private const double DeathDelay = 2.0; //2 seconds delay

    public StoneAge()
    {
        playerStartPosition = new Vector2(100, 600);
        enemyStartPosition = new Vector2(400, 600);
        enemyEndPosition = new Vector2(700, 600);
        gemStartPosition = new Vector2(300, 600);

        pausePopupMenu = new PausePopupMenu();

        popUpMenuTriggerd = false;
        popUpMenuFired = false;
        enemies = new List<Enemy>();
    }


    internal override void LoadContent(ContentManager Content)
    {
        Texture2D runTexture = Content.Load<Texture2D>("Player_Level1/Warrior_1/Run");
        Texture2D idleTexture = Content.Load<Texture2D>("Player_Level1/Warrior_1/Idle");
        Texture2D jumpAnimation = Content.Load<Texture2D>("Player_Level1/Warrior_1/Jump2");
        Texture2D attackAnimation = Content.Load<Texture2D>("Player_Level1/Warrior_1/Attack_1");
        Texture2D deathAnimation = Content.Load<Texture2D>("Player_Level1/Warrior_1/Dead");
        Texture2D damageAnimation = Content.Load<Texture2D>("Player_Level1/Warrior_1/Hurt");

        Texture2D enAttackTexture = Content.Load<Texture2D>("Player_Level1/Warrior_2/Run+Attack");
        Texture2D enRunTexture = Content.Load<Texture2D>("Player_Level1/Warrior_2/Run");
        Texture2D enDamageTexture = Content.Load<Texture2D>("Player_Level1/Warrior_2/Hurt");
        Texture2D enDeathTexture = Content.Load<Texture2D>("Player_Level1/Warrior_2/Dead");

        Texture2D gemTexture = Content.Load<Texture2D>("Items/Gems/plate32x8");

        player = new Player(runTexture, idleTexture, jumpAnimation, attackAnimation, deathAnimation, damageAnimation, playerStartPosition, 100);
        
        enemy = new EnemyCharge(enRunTexture, enAttackTexture, enDamageTexture, enDeathTexture, enemyStartPosition, enemyEndPosition, 100f, 100f, 100, 300f);
        enemies.Add(enemy);

        gem = new Gem(gemTexture, new Vector2(300, 600));
        
        pausePopupMenu.LoadContent(Content);

    }

    public void Reset()
    {
        popUpMenuTriggerd = false;
        player.ResetPlayer();

        enemy.ResetEnemy();
        gem.position = gemStartPosition;

    }
    internal override void Update(GameTime gameTime)
    {
        pausePopupMenu.Update(gameTime);

        if (currentGameState == GameState.Playing)
        {
            if (player != null && gem != null && enemies != null)
            {
                player.Update(gameTime, enemies);
                //player.AttackEnemy(enemies);
                gem.Update(gameTime);
                foreach(var enemy in enemies)
                {
                    enemy.Update(gameTime, player);
                    enemy.AttackPlayer(player);
                }
                foreach(var projectile in player.ActiveProjectiles.ToList())
                {
                    projectile.Update(gameTime);
                }
            }
            else
            {
                Console.WriteLine("The player or gem or enemy is null");
            }
        }

        // check button state
        if (!this.popUpMenuFired)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.P) || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.popUpMenuFired = true;
                if (currentGameState == GameState.Playing)
                {
                    popUpMenuTriggerd = true;
                    currentGameState = GameState.Paused;
                }
                else if (currentGameState == GameState.Paused)
                {
                    popUpMenuTriggerd = false;
                    currentGameState = GameState.Playing;
                }
            }
            if (!player.isAlive && !popUpMenuTriggerd)
            {
                deathTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (deathTimer >= DeathDelay)
                {
                    currentGameState = GameState.Paused;
                    popUpMenuTriggerd = true;
                    deathTimer = 0;
                }

            }
        }
        else
        {
            if (Keyboard.GetState().IsKeyUp(Keys.P) && Keyboard.GetState().IsKeyUp(Keys.Escape))
            {
                this.popUpMenuFired = false;
                popUpMenuTriggerd = false;
            }
        }

    }
    internal override void Draw(SpriteBatch spriteBatch)
    {
        if (currentGameState == GameState.Playing || !popUpMenuTriggerd)
        {
            if (player != null)
            {
                player.Draw(spriteBatch);
                gem.Draw(spriteBatch);
                foreach(var enemy in enemies)
                {
                    enemy.Draw(spriteBatch);
                }
                foreach (var projectile in player.ActiveProjectiles)
                {
                    projectile.Draw(spriteBatch);
                }
            }
        }
        if (!player.isAlive && !popUpMenuTriggerd || currentGameState == GameState.Paused && popUpMenuTriggerd)
        {
            pausePopupMenu.Draw(spriteBatch);
            //popUpMenuTriggerd = false;
        }


    }
}