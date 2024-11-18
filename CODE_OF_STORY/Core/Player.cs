using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CODE_OF_STORY.Scenes.Gateway;

using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
//using Vector2 = System.Numerics.Vector2;

namespace CODE_OF_STORY.Core;

/* Implements the player character. 
Used to load, draw, and update the character. */
public class Player
{
    private AnimationPlayer runAnimation;
    private AnimationPlayer idleAnimation;
    private AnimationPlayer jumpAnimation;
    private AnimationPlayer attackAnimation;
    private AnimationPlayer deathAnimation;
    private AnimationPlayer damageAnimation;
    private bool facingRight;
    private Texture2D idleTexture;
    //laufen var
    private Vector2 position;
    private float speed;
    public bool isMoving { get; private set; }
    //fighting var
    private bool isAttacking;
    private bool hasDealtDamage = false;
    public bool damageTaken;
    private int health = 100;
    public bool isAlive => health > 0;
    public static bool checkIsAlive; 
    //Waffenwechsel
    private bool RangedMode = false;
    //public IEnumerable<Projectile> ActiveProjectiles => projectiles?.Where(p => p.IsActive) ?? Enumerable.Empty<Projectile>();
    public List<Projectile> ActiveProjectiles {get; private set;} = new List<Projectile>();
    //projektil var
    private Texture2D arrowTexture;
    private List<Projectile> projectiles;
    private int maxProjektiles = 5;
    private float projektilSpeed = 500f;
    //sprung var
    private bool isJumping;
    private float jumpSpeed = 0f;
    private float jumpPower = 300f;
    private float gravity = 500f;
    public float groundLevel;

    //zugriff aus die aktuelle position für enemy
    public Vector2 Position
    {
        get { return position; }
    }
    public Player(Texture2D runTexture, Texture2D idleTexture, Texture2D jumpTexture, Texture2D attackTexture, Texture2D deathTexture, Texture2D damageTexture,
                     Vector2 position, int initialHealth)
    {
        this.idleTexture = idleTexture;
        runAnimation = new AnimationPlayer(runTexture, frameCount: 6, animationSpeed: 0.1f, playOnce: false);
        idleAnimation = new AnimationPlayer(idleTexture, frameCount: 6, animationSpeed: 0.1f, playOnce: false);
        jumpAnimation = new AnimationPlayer(jumpTexture, frameCount: 2, animationSpeed: 0.5f, playOnce: false);
        attackAnimation = new AnimationPlayer(attackTexture, frameCount: 4, animationSpeed: 0.2f, playOnce: true);
        deathAnimation = new AnimationPlayer(deathTexture, frameCount: 4, animationSpeed: 0.2f, playOnce: true);
        damageAnimation = new AnimationPlayer(damageTexture, frameCount: 2, animationSpeed: 0.2f, playOnce: true);
        this.position = position;
        this.speed = 200f;
        this.groundLevel = position.Y;
        this.health = initialHealth;
        projectiles = new List<Projectile>();
        checkIsAlive = isAlive;
    }

    public void ResetPlayer()
    {
        position = StoneAge.playerStartPosition;
        speed = 200f;
        groundLevel = position.Y;
        health = 100;
        checkIsAlive = isAlive;
        projectiles = new List<Projectile>();
    }

    public async void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;//todesanimation und entfernen
        }
        else
        {
            damageTaken = true;
            await Task.Delay(200);
        }
        damageTaken = false;
    }
    public void AttackEnemy(Enemy enemy)
    {
        if (isAttacking && !hasDealtDamage && IsEnemyInRange(enemy))
        {
            enemy.TakeDamage(10);
            hasDealtDamage = true;
            enemy.damageTaken = true;
        }
    }
    public void ShootArrow(Vector2 targetPosition)
    {
        if(projectiles.Count > 0)
        {
            var arrow = projectiles.FirstOrDefault(p => !p.IsActive);
            if(arrow == null)
            return;
            
            Vector2 direction = targetPosition - position;
            direction.Normalize();
            //Vector2 velocity = direction * arrow.speed;
            arrow = new Projectile(arrowTexture, position + new Vector2(facingRight ? 30 : -30, 0), direction, projektilSpeed);
            /*arrow.Position = position + new Vector2(facingRight ? 30 : -30, 0);
            arrow.Direction = direction;
            arrow.speed = projektilSpeed;*/
            arrow.IsActive = true;
            ActiveProjectiles.Add(arrow);
            
        }
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

    public void LoadContent(ContentManager content)
    {
        arrowTexture = content.Load<Texture2D>("Player_Level1/Warrior_1/arrow");
        for (int i = 0; i < maxProjektiles; i++)
        {
            projectiles.Add(new Projectile(arrowTexture,Vector2.Zero,Vector2.Zero,0f) {IsActive = false});
        }
    }
    public void Update(GameTime gameTime, List<Enemy> enemies)
    {
        if(projectiles == null)
        {
            projectiles = new List<Projectile>();
        }
        if(isAlive)
        {
            KeyboardState state = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            isMoving = false;

            if (state.IsKeyDown(Keys.A))
            {
                position.X -= speed * deltaTime;
                facingRight = false;
                isMoving = true;
            }
            else if (state.IsKeyDown(Keys.D))
            {
                position.X += speed * deltaTime;
                facingRight = true;
                isMoving = true;
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

            if (state.IsKeyDown(Keys.Space) && !isJumping)
            {
                isJumping = true;
                jumpSpeed = -jumpPower;
            }

            if (isJumping)
            {
                jumpSpeed += gravity * deltaTime;
                position.Y += jumpSpeed * deltaTime;


            if(position.Y >= groundLevel)
            {
                position.Y = groundLevel;
                isJumping = false;
                jumpSpeed = 0f;
            }
        }
        /*if (state.IsKeyDown(Keys.F))
            interagieren*/
        if (state.IsKeyDown(Keys.Q) && !isAttacking)
        {
            RangedMode = !RangedMode;
        }
        
        if (mouseState.LeftButton == ButtonState.Pressed && !isAttacking)
        {
            if(RangedMode && projectiles.Count > 0)
            {
                ShootArrow(mouseState.Position.ToVector2());
                
            }
            else
            {
                isAttacking = true;
                hasDealtDamage = false;
                attackAnimation.Reset();
            }
        }

        foreach(var enemy in enemies)
        {
            if(enemy != null)
            {
                AttackEnemy(enemy);
            }
        }

        foreach (var projectile in projectiles)
        {
            projectile.Update(gameTime);
        }
        
        if(isAttacking)
        {
            attackAnimation.Update(gameTime);
            if(attackAnimation.IsFinished)
            {
                isAttacking = false;
                hasDealtDamage = false;
            }
        }

            if (damageTaken)
            {
                damageAnimation.Update(gameTime);
            }
        }
        else
        {
            deathAnimation.Update(gameTime);
            checkIsAlive = false;
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

        //spriteBatch.Draw(texture, position, Color.White);
    }
}
