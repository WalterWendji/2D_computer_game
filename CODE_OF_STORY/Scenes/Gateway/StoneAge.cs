using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using CODE_OF_STORY.Core;
using CODE_OF_STORY.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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

    private KeyboardState currentKeyboardState;
    private KeyboardState prevKeyboardState;

    public static Vector2 playerStartPosition;
    public static Vector2 enemyStartPosition;
    public static Vector2 enemyEndPosition;
    private static Vector2 enemyStartPosition2;
    public static Vector2 enemyEndPosition2;
    private static Vector2 enemyStartPosition3;
    public static Vector2 enemyEndPosition3;
    private static Vector2 enemyStartPosition4;
    public static Vector2 enemyEndPosition4;
    public static Vector2 shopkeeperPosition;
    private Vector2 gemStartPosition;

    private PausePopupMenu pausePopupMenu;

    private GameOver gameOver;

    public static bool popUpMenuTriggerd;
    private bool popUpMenuFired;
    private bool isGameOverRendered;

    SoundEffect backgroundSoundScenario1;
    SoundEffectInstance backgroundSoundScenario1Instance;

    public StoneAge()
    {
        playerStartPosition = new Vector2(100, 600);
        enemyStartPosition = new Vector2(400, 600);
        enemyStartPosition2 = new Vector2(400, 600);
        enemyEndPosition2 = new Vector2(700, 600);
        enemyStartPosition3 = new Vector2(700, 600);
        enemyEndPosition3 = new Vector2(1200, 600);
        enemyEndPosition = new Vector2(700, 600);
        enemyStartPosition4 = new Vector2 (400, 600);
        enemyEndPosition4 = new Vector2(700, 600);
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
        ResourceManager.LoadContent(Content);

        Texture2D gemTexture = Content.Load<Texture2D>("Items/Gems/plate32x8");
        Texture2D arrowTexture = Content.Load<Texture2D>("Player_Level1/Warrior_1/arrow");

        player = new Player(playerStartPosition, 100);
        player.LoadContent(Content);

        enemy = new EnemyTank(enemyStartPosition2, enemyEndPosition2, 150f, 50f, 100);
        enemies.Add(enemy);
        enemy = new EnemyCharge(enemyStartPosition, enemyEndPosition, 100f, 50f, 100, 300f);
        enemies.Add(enemy);
        enemy = new EnemyNahkampf(enemyStartPosition3, enemyEndPosition3, 100f, 50f, 100);
        enemies.Add(enemy);
        enemy = new EnemyFernkampf(enemyStartPosition4, enemyEndPosition4, 300f, 300f, 50, arrowTexture);
        enemies.Add(enemy);

        gem = new Gem(gemTexture, new Vector2(300, 600));

        //Texture2D shopBackground = Content.Load<Texture2D>("GameUI/shop");
        SpriteFont font = Content.Load<SpriteFont>("Arial");

        Texture2D hPotionImage = Content.Load<Texture2D>("Items/ShopItem/hPotion");
        Texture2D asPotionImage = Content.Load<Texture2D>("Items/ShopItem/asPotion");
        Texture2D swordImage = Content.Load<Texture2D>("Items/ShopItem/sword");
        Texture2D shildImage = Content.Load<Texture2D>("Items/ShopItem/shild");
        Texture2D lifeImage = Content.Load<Texture2D>("Items/ShopItem/life");
        Texture2D bootImage = Content.Load<Texture2D>("Items/ShopItem/boot");

        List<ShopItem> shopItems = new List<ShopItem>
        {
            new ShopItem("Heiltrank", hPotionImage, 100),
            new ShopItem("Angriffsgeschwindikeit", asPotionImage, 150),
            new ShopItem("Nahkampfschaden", swordImage, 100),
            new ShopItem("Schild", shildImage, 100),
            new ShopItem("Extra Leben", lifeImage, 100),
            new ShopItem("Doppelsprung", bootImage, 100)
        };
        shopkeeper = new Shopkeeper(shopkeeperPosition, /*shIdleTexture, shDialogueTexture, shGreetingTexture, shApprovalTexture,*/ font, shopItems);
        
        pausePopupMenu.LoadContent(Content);
        gameOver.LoadContent(Content);
        shopkeeper.shopWindow.LoadContent(Content);

        backgroundSoundScenario1 = Content.Load<SoundEffect>("Audio/25-Raid_FolkMetal2W");
        backgroundSoundScenario1Instance = backgroundSoundScenario1.CreateInstance();


    }

    public void Reset()
    {
        backgroundSoundScenario1Instance.Play();

        popUpMenuTriggerd = false;
        isGameOverRendered = false;

        player.ResetPlayer();

        enemy.ResetEnemy();
        gem.position = gemStartPosition;

    }
    internal override void Update(GameTime gameTime)
    {
        /*KeyboardState keyboardState = Keyboard.GetState();
        KeyboardState prevKeyboardState = Keyboard.GetState();
        */
        prevKeyboardState = currentKeyboardState;
        currentKeyboardState = Keyboard.GetState();

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
                    backgroundSoundScenario1Instance.Pause();
                }
                else if (currentGameState == GameState.Paused)
                {
                    popUpMenuTriggerd = false;
                    currentGameState = GameState.Playing;

                    backgroundSoundScenario1Instance.Resume();
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

        if (currentGameState == GameState.Playing)
        {
            if (player != null && gem != null && enemies != null)
            {
                backgroundSoundScenario1Instance.Play(); 
                backgroundSoundScenario1Instance.Volume = 0.03f;
                
                player.Update(gameTime, enemies);
                gem.Update(gameTime);
                shopkeeper.Update(gameTime, player, currentKeyboardState, prevKeyboardState);

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

        if (player != null && !player.isAlive && !popUpMenuTriggerd)
        {
            isGameOverRendered = true;
            gameOver.Update(gameTime);

            backgroundSoundScenario1Instance.Stop();
        }


    }
    internal override void Draw(SpriteBatch spriteBatch)
    {
        if (currentGameState == GameState.Playing || !popUpMenuTriggerd)
        {
            if (player != null)
            {
                if(shopkeeper.isInteracting)
                {
                    shopkeeper.shopWindow.Draw(spriteBatch);
                }
                
                player.Draw(spriteBatch);
                gem.Draw(spriteBatch);
                shopkeeper.Draw(spriteBatch, player);


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

        if (player != null && !player.isAlive && !popUpMenuTriggerd)
        {
            gameOver.Draw(spriteBatch);
            //popUpMenuTriggerd = false;
        }

        if (currentGameState == GameState.Paused && popUpMenuTriggerd)
            pausePopupMenu.Draw(spriteBatch);
    }
}