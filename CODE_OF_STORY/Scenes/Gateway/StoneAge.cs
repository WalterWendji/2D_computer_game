using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
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
    private Shopkeeper shopkeeper;
    private List<Enemy> enemies;
    private Gem gem;

    public static Vector2 playerStartPosition;
    public static Vector2 enemyStartPosition;
    public static Vector2 enemyEndPosition;
    private static Vector2 enemyStartPosition2;
    public static Vector2 enemyEndPosition2;
    private static Vector2 enemyStartPosition3;
    public static Vector2 enemyEndPosition3;
    public static Vector2 shopkeeperPosition;
    private Vector2 gemStartPosition;

    private PausePopupMenu pausePopupMenu;

    private GameOver gameOver;

    public static bool popUpMenuTriggerd;
    private bool popUpMenuFired;
    private bool isGameOverRendered;

    public StoneAge()
    {
        playerStartPosition = new Vector2(100, 600);
        enemyStartPosition = new Vector2(400, 600);
        enemyStartPosition2 = new Vector2(400, 600);
        enemyEndPosition2 = new Vector2(700, 600);
        enemyStartPosition3 = new Vector2(700, 600);
        enemyEndPosition3 = new Vector2(1200, 600);
        enemyEndPosition = new Vector2(700, 600);
        gemStartPosition = new Vector2(300, 600);
        shopkeeperPosition = new Vector2(1500, 600);

        pausePopupMenu = new PausePopupMenu();
        gameOver = new GameOver();

        popUpMenuTriggerd = false;
        popUpMenuFired = false;
        isGameOverRendered = false;

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

        Texture2D enNAttackTexture = Content.Load<Texture2D>("Player_Level1/EnemyN/enRun+Attack");
        Texture2D enNRunTexture = Content.Load<Texture2D>("Player_Level1/EnemyN/enRun");
        Texture2D enNDamageTexture = Content.Load<Texture2D>("Player_Level1/EnemyN/enHurt");
        Texture2D enNDeathTexture = Content.Load<Texture2D>("Player_Level1/EnemyN/enDead");

        Texture2D enCAttackTexture = Content.Load<Texture2D>("Player_Level1/Warrior_2/Run+Attack");
        Texture2D enCRunTexture = Content.Load<Texture2D>("Player_Level1/Warrior_2/Run");
        Texture2D enCDamageTexture = Content.Load<Texture2D>("Player_Level1/Warrior_2/Hurt");
        Texture2D enCDeathTexture = Content.Load<Texture2D>("Player_Level1/Warrior_2/Dead");

        Texture2D enTAttackTexture = Content.Load<Texture2D>("Player_Level1/Warrior_3/Attack_1");
        Texture2D enTWalkTexture = Content.Load<Texture2D>("Player_Level1/Warrior_3/Walk");
        Texture2D enTDamageTexture = Content.Load<Texture2D>("Player_Level1/Warrior_3/Hurt");
        Texture2D enTDeathTexture = Content.Load<Texture2D>("Player_Level1/Warrior_3/Dead");
        Texture2D enTBlockTexture = Content.Load<Texture2D>("Player_Level1/Warrior_3/Protect");

        Texture2D gemTexture = Content.Load<Texture2D>("Items/Gems/plate32x8");

        Texture2D shIdleTexture = Content.Load<Texture2D>("NPCs/Shopkeeper/Idle");
        Texture2D shApprovalTexture = Content.Load<Texture2D>("NPCs/Shopkeeper/Approval");
        Texture2D shGreetingTexture = Content.Load<Texture2D>("NPCs/Shopkeeper/Idle_2");
        Texture2D shDialogueTexture = Content.Load<Texture2D>("NPCs/Shopkeeper/Dialogue");

        player = new Player(runTexture, idleTexture, jumpAnimation, attackAnimation, deathAnimation, damageAnimation, playerStartPosition, 100);
        player.LoadContent(Content);

        enemy = new EnemyTank(enTWalkTexture, enTAttackTexture, enTDamageTexture, enTDeathTexture, enTBlockTexture, enemyStartPosition2, enemyEndPosition2, 150f, 100);
        enemies.Add(enemy);
        enemy = new EnemyCharge(enCRunTexture, enCAttackTexture, enCDamageTexture, enCDeathTexture, enemyStartPosition, enemyEndPosition, 100f, 100, 300f);
        enemies.Add(enemy);
        enemy = new EnemyNahkampf(enNRunTexture, enNAttackTexture, enNDamageTexture, enNDeathTexture, enemyStartPosition3, enemyEndPosition3, 100f, 100);
        enemies.Add(enemy);

        gem = new Gem(gemTexture, new Vector2(300, 600));

        shopkeeper = new Shopkeeper(shopkeeperPosition, shIdleTexture, shDialogueTexture, shGreetingTexture, shApprovalTexture);
        
        pausePopupMenu.LoadContent(Content);
        gameOver.LoadContent(Content);

    }

    public void Reset()
    {
        popUpMenuTriggerd = false;
        isGameOverRendered = false;

        player.ResetPlayer();

        enemy.ResetEnemy();
        gem.position = gemStartPosition;

    }
    internal override void Update(GameTime gameTime)
    {

        if (currentGameState == GameState.Playing)
        {
            if (player != null && gem != null && enemies != null)
            {
                player.Update(gameTime, enemies);
                gem.Update(gameTime);
                shopkeeper.Update(gameTime, player);
                foreach (var projectile in player.ActiveProjectiles.ToList())
                {
                    foreach (var enemy in enemies)
                    {
                        enemy.CheckProjectileCollision(projectile);
                    }
                    projectile.Update(gameTime);
                }
                foreach (var enemy in enemies)
                {
                    enemy.Update(gameTime, player);
                    enemy.AttackPlayer(player);
                }
            }
            else
            {
                Console.WriteLine("The player or gem or enemy is null");
            }
        }
        else if (currentGameState == GameState.Paused)
            pausePopupMenu.Update(gameTime);

        if (!player.isAlive && !popUpMenuTriggerd)
        {
            isGameOverRendered = true;
            gameOver.Update(gameTime);
        }

        // check button state
        if (!this.popUpMenuFired)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.P) && !isGameOverRendered || Keyboard.GetState().IsKeyDown(Keys.Escape) && !isGameOverRendered)
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
        }
        else
        {
            if (Keyboard.GetState().IsKeyUp(Keys.P) && Keyboard.GetState().IsKeyUp(Keys.Escape))
            {
                this.popUpMenuFired = false;
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
                shopkeeper.Draw(spriteBatch);
                foreach (var enemy in enemies)
                {
                    enemy.Draw(spriteBatch);
                }
                foreach (var projectile in player.ActiveProjectiles)
                {
                    if (projectile.IsActive)
                    {
                        projectile.Draw(spriteBatch);
                    }
                }
            }
        }

        if (!player.isAlive && !popUpMenuTriggerd)
        {
            gameOver.Draw(spriteBatch);
            //popUpMenuTriggerd = false;
        }

        if (currentGameState == GameState.Paused && popUpMenuTriggerd)
            pausePopupMenu.Draw(spriteBatch);
    }
}