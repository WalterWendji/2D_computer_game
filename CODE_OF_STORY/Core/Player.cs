using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CODE_OF_STORY.Scenes.Gateway;
using CODE_OF_STORY.Managers;

using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using Microsoft.Xna.Framework.Audio;

namespace CODE_OF_STORY.Core;

/* Implements the player character. 
Used to load, draw, and update the character. */
public class Player
{
    private readonly AnimationPlayer runAnimation;
    private readonly AnimationPlayer idleAnimation;
    private readonly AnimationPlayer jumpAnimation;
    private readonly AnimationPlayer attackAnimation;
    private readonly AnimationPlayer deathAnimation;
    private readonly AnimationPlayer damageAnimation;
    private bool facingRight;
    //laufen var
    private Vector2 position;
    private float speed;
    public bool isMoving { get; private set; }
    //fighting var
    private bool isAttacking;
    private bool hasDealtDamage = false;
    public bool damageTaken;
    public int health {get; private set;}
    public int maxHealth {get; private set;}
    public bool isAlive => health > 0;
    private readonly float shootCd = 2f;
    private float startShootCd = 0f;
    public static bool checkIsAlive;
    //Waffenwechsel
    private bool prevQKeyPressed = false;
    private bool RangedMode = false;
    public List<Projectile> ActiveProjectiles { get; private set; } = new List<Projectile>();
    //projektil var
    private Texture2D arrowTexture;
    private List<Projectile> projectiles;
    private int maxProjektiles = 5;
    //sprung var
    private bool isJumping;
    private float jumpSpeed = 0f;
    private float jumpPower = 300f;
    private float gravity = 500f;
    public float groundLevel;

    private bool isDeathSoundEffectPlayed;
    private float footStepsRunSpeed;
    SoundEffect footStepsRunSoundEffect, jumpSoundEffect, attackSoundEffect, landSoundEffect, deathSoundEffect, damageSoundEffect;
    SoundEffectInstance footStepsRunSoundEffectInstance, jumpSoundEffectInstance, attackSoundEffectInstance, landSoundEffectInstance, deathSoundEffectInstance, damageSoundEffectInstance;
    public int Score { get; private set; }


    //zugriff aus die aktuelle position für enemy
    public Vector2 Position
    {
        get { return position; }
    }
    public Player(Vector2 position, int currentHealth)
    {
        runAnimation = new AnimationPlayer(ResourceManager.runTexture, frameCount: 6, animationSpeed: 0.1f, playOnce: false);
        idleAnimation = new AnimationPlayer(ResourceManager.idleTexture, frameCount: 6, animationSpeed: 0.1f, playOnce: false);
        jumpAnimation = new AnimationPlayer(ResourceManager.jumpTexture, frameCount: 2, animationSpeed: 0.5f, playOnce: false);
        attackAnimation = new AnimationPlayer(ResourceManager.attackTexture, frameCount: 4, animationSpeed: 0.2f, playOnce: true);
        deathAnimation = new AnimationPlayer(ResourceManager.deathTexture, frameCount: 4, animationSpeed: 0.2f, playOnce: true);
        damageAnimation = new AnimationPlayer(ResourceManager.damageTexture, frameCount: 2, animationSpeed: 0.2f, playOnce: true);

        this.position = position;
        this.speed = 200f;
        this.groundLevel = position.Y;
        this.health = currentHealth;
        //this.maxHealth = currentMaxHealth;

        projectiles = new List<Projectile>();

        checkIsAlive = isAlive;

        footStepsRunSpeed = 0.05f;
        isDeathSoundEffectPlayed = false;
    }

    public void ResetPlayer()
    {
        position = StoneAge.playerStartPosition;
        speed = 200f;
        groundLevel = position.Y;
        health = 100;
        checkIsAlive = isAlive;

        projectiles = new List<Projectile>();

        isDeathSoundEffectPlayed = false;
    }

    public async void TakeDamage(int damage)
    {
        
        if (health <= 0)
        {
            health = 0;//todesanimation und entfernen
        }
        else
        {
            health -= damage;
            damageTaken = true;
            await Task.Delay(200);
        }
        damageTaken = false;
    }
    public void AttackEnemy(Enemy enemy)
    {
        if (isAttacking && !hasDealtDamage && IsEnemyInRange(enemy))
        {
            enemy.TakeDamage(10, position);
            hasDealtDamage = true;
            enemy.damageTaken = true;
        }
    }

    public void ShootArrow(Vector2 targetPosition)
    {
        if (projectiles.Count > 0)
        {
            if (startShootCd < shootCd)
                return;
            startShootCd = 0;
            var arrow = projectiles.FirstOrDefault(p => !p.IsActive);
            if (arrow == null)
                return;
            MouseState mouseState = Mouse.GetState();
            targetPosition = new Vector2(mouseState.X, mouseState.Y - 80);
            Vector2 direction = targetPosition - position;
            direction.Normalize();
            arrow = new Projectile(arrowTexture, position + new Vector2(facingRight ? 30 : -30, 40), direction, 500f);
            arrow.IsActive = true;
            ActiveProjectiles.Add(arrow);

        }
    }

    public bool CheckPlayerProjectileCollision(Projectile projectile)
    {
        if(!isAlive || !projectile.IsActive)
            return false;
        Rectangle playerBounds = new Rectangle((int)position.X, (int)position.Y, 64, 50);
        Rectangle projectileBounds = new Rectangle((int)projectile.Position.X, (int)Position.Y, 16, 5);
        if(playerBounds.Intersects(projectileBounds))
        {
            TakeDamage(15);
            projectile.IsActive = false;
            return true;
        }
        return false;
    }
    public void RefillArrows(int count)
    {

        for (int i = 0; i < count; i++)
        {
            if (projectiles.Count < maxProjektiles)
            {
                projectiles.Add(new Projectile(arrowTexture, Vector2.Zero, Vector2.Zero, 0f));
            }
        }
    }

    private bool IsEnemyInRange(Enemy enemy)
    {
        float attackRange = 50f;
        if (facingRight)
        {
            return (enemy.Position.X > position.X && enemy.Position.X <= position.X + attackRange);
        }
        else
        {
            return (enemy.Position.X < position.X && enemy.Position.X >= position.X - attackRange);
        }
    }

    public void Heal(int amount)
    {
       // Health = Math.Min(health + amount, currentMaxHealth)
    }

    public void IncreaseScore(int points)
     {
            Score += points;
     }

    public void LoadContent(ContentManager content)
    {
        arrowTexture = content.Load<Texture2D>("Player_Level1/Warrior_1/arrow");
        for (int i = 0; i < maxProjektiles; i++)
        {
            projectiles.Add(new Projectile(arrowTexture, Vector2.Zero, Vector2.Zero, 0f) { IsActive = false });
        }

        footStepsRunSoundEffect = content.Load<SoundEffect>("Audio/Sound_animation/Footsteps_Gravel_Run_02");
        jumpSoundEffect = content.Load<SoundEffect>("Audio/Sound_animation/Voice_Male_V1_Jump_Mono_05");
        attackSoundEffect = content.Load<SoundEffect>("Audio/Sound_animation/Voice_Male_V1_Attack_Short_Mono_09");
        landSoundEffect = content.Load<SoundEffect>("Audio/Sound_animation/Voice_Male_V1_Land_Mono_05");
        deathSoundEffect = content.Load<SoundEffect>("Audio/Sound_animation/death_4_sean");
        damageSoundEffect = content.Load<SoundEffect>("Audio/Sound_animation/damage_1_sean");

        footStepsRunSoundEffectInstance = footStepsRunSoundEffect.CreateInstance();
        jumpSoundEffectInstance = jumpSoundEffect.CreateInstance();
        attackSoundEffectInstance = attackSoundEffect.CreateInstance();
        landSoundEffectInstance = landSoundEffect.CreateInstance();
        deathSoundEffectInstance = deathSoundEffect.CreateInstance();
        damageSoundEffectInstance = damageSoundEffect.CreateInstance();
    }

    public void Update(GameTime gameTime, List<Enemy> enemies, List<Projectile> enemyProjectiles)
    {
        if (projectiles == null)
        {
            projectiles = new List<Projectile>();
        }
        if (isAlive)
        {
            var keyboardState = Keyboard.GetState();
            KeyboardState prevKeyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            startShootCd += (float)gameTime.ElapsedGameTime.TotalSeconds;
            isMoving = false;

            foreach(var projectile in enemyProjectiles)
            {
                CheckPlayerProjectileCollision(projectile);
            }
            if (keyboardState.IsKeyDown(PlayerControls.Settings.MoveLeft) || keyboardState.IsKeyDown(PlayerControls.Settings.MoveRight))
            {
                isMoving = true;

                footStepsRunSoundEffectInstance.Play();
                footStepsRunSoundEffectInstance.Pitch = footStepsRunSpeed;
            }
            if (keyboardState.IsKeyDown(PlayerControls.Settings.MoveLeft))
            {
                position.X -= speed * deltaTime;
                facingRight = false;
            }
            else if (keyboardState.IsKeyDown(PlayerControls.Settings.MoveRight))
            {
                position.X += speed * deltaTime;
                facingRight = true;
            }
            if (isJumping)
            {
                jumpAnimation.Update(gameTime);
            }
            else if (isMoving)
            {
                runAnimation.Update(gameTime);
            }
            else
            {
                idleAnimation.Update(gameTime);
            }

            if (keyboardState.IsKeyDown(PlayerControls.Settings.Jump) && !isJumping)
            {
                jumpSoundEffectInstance.Play();

                isJumping = true;
                jumpSpeed = -jumpPower;
            }

            if (isJumping)
            {
                jumpSpeed += gravity * deltaTime;
                position.Y += jumpSpeed * deltaTime;

                footStepsRunSoundEffectInstance.Stop();

                if (position.Y >= groundLevel)
                {
                    landSoundEffectInstance.Play();
                    footStepsRunSoundEffectInstance.Resume();

                    position.Y = groundLevel;
                    isJumping = false;
                    jumpSpeed = 0f;
                }
            }
            
            bool isQKeyPressed = keyboardState.IsKeyDown(PlayerControls.Settings.Interact);
            if (isQKeyPressed && !prevQKeyPressed && !isAttacking)
            {
                RangedMode = !RangedMode;
                Console.WriteLine("range mode" + RangedMode);
            }
            prevQKeyPressed = isQKeyPressed;
            if (mouseState.LeftButton == ButtonState.Pressed && !isAttacking)
            {
                if (RangedMode && projectiles.Count > 0)
                {
                    ShootArrow(mouseState.Position.ToVector2());

                }
                else
                {
                    attackSoundEffectInstance.Play();

                    isAttacking = true;
                    hasDealtDamage = false;
                    attackAnimation.Reset();
                }
            }

            foreach (var enemy in enemies)
            {
                if (enemy != null)
                {
                    AttackEnemy(enemy);
                }
            }

            foreach (var projectile in projectiles)
            {
                projectile.Update(gameTime);
            }

            if (isAttacking)
            {
                attackAnimation.Update(gameTime);
                if (attackAnimation.IsFinished)
                {
                    isAttacking = false;
                    hasDealtDamage = false;
                }
            }

            if (damageTaken)
            {
                damageSoundEffectInstance.Play();

                damageAnimation.Update(gameTime);
            }
        }
        else
        {
            if(!isDeathSoundEffectPlayed)
            {
                deathSoundEffectInstance.Play();
                isDeathSoundEffectPlayed = true;
            }
            
            deathAnimation.Update(gameTime);
            checkIsAlive = false;
        }
    }
    public Rectangle Bounds 
    {
        get 
        {
            return new Rectangle((int)Position.X, (int)Position.Y, 64, 64); // غير العرض والطول لو لازم
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        SpriteEffects flipEffect = facingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        if (isAlive)
        {
            if (isAttacking)
            {
                attackAnimation.Draw(spriteBatch, position, flipEffect);
            }
            else if (isJumping)
            {
                jumpAnimation.Draw(spriteBatch, position, flipEffect);
            }
            else if (isMoving && !isJumping && !damageTaken)
            {
                runAnimation.Draw(spriteBatch, position, flipEffect);
            }
            else if (damageTaken)
            {
                damageAnimation.Draw(spriteBatch, position, flipEffect);
            }
            else
            {
                idleAnimation.Draw(spriteBatch, position, flipEffect);
            }
        }
        else
        {
            deathAnimation.Draw(spriteBatch, position, flipEffect);
        }
    }
}
