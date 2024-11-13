using System;
//using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
    private bool facingRight;
    private Texture2D idleTexture;
    //laufen var
    private Vector2 position;
    private float speed;
    public bool isMoving { get; private set;}
    //fighting var
    private bool isAttacking;
    private bool hasDealtDamage = false;
    public bool damageTaken;
    private int health = 100;
    public bool isAlive => health > 0;
    //sprung var
    private bool isJumping;
    private float jumpSpeed = 0f;
    private float jumpPower = 300f;
    private float gravity = 500f;
    private float groundLevel;

    //zugriff aus die aktuelle position für enemy
    public Vector2 Position
    {
        get { return position;}
    }
    public Player(Texture2D runTexture, Texture2D idleTexture, Texture2D jumpTexture,Texture2D attackTexture, Texture2D deathTexture, Vector2 position, int initialHealth)
    {
        this.idleTexture = idleTexture;
        runAnimation = new AnimationPlayer(runTexture, frameCount: 6, animationSpeed: 0.1f, playOnce: false);
        idleAnimation = new AnimationPlayer(idleTexture, frameCount: 6, animationSpeed: 0.1f, playOnce: false);
        jumpAnimation = new AnimationPlayer(jumpTexture, frameCount: 2, animationSpeed: 0.5f, playOnce: false);
        attackAnimation = new AnimationPlayer(attackTexture, frameCount: 4, animationSpeed: 0.2f, playOnce: true);
        deathAnimation = new AnimationPlayer(deathTexture, frameCount: 4, animationSpeed: 0.2f, playOnce: true);
        this.position = position;
        this.speed = 200f;
        this.groundLevel = position.Y;
        this.health = initialHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            health = 0;//todesanimation und entfernen
        }
        else
        {
            //schadensanimation
        }
    }
    public void AttackEnemy(Enemy enemy)
    {
        if(isAttacking && !hasDealtDamage && IsEnemyInRange(enemy))
        {
            enemy.TakeDamage(10);
            hasDealtDamage = true;
            enemy.damageTaken = true;
        }
    }


    private bool IsEnemyInRange(Enemy enemy)
    {
        float attackRange = 50f;
        if(facingRight)
        {
            return(enemy.Position.X > position.X && enemy.Position.X <= position.X + attackRange);
        }
        else
        {
             return(enemy.Position.X < position.X && enemy.Position.X >= position.X - attackRange);
        }
    }
    public void Update(GameTime gameTime)
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
        else if(state.IsKeyDown(Keys.D))
        {    
            position.X += speed * deltaTime;
            facingRight = true;
            isMoving = true;
        }
        if(isJumping)
        {
            jumpAnimation.Update(gameTime);
        }
        else if(isMoving)
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

        if(isJumping)
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
            interagieren
        if (state.IsKeyDown(Keys.Q))
            Waffewechseln
            */
        if (mouseState.LeftButton == ButtonState.Pressed && !isAttacking)
        {
            isAttacking = true;
            hasDealtDamage = false;
            attackAnimation.Reset();
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
        if(!isAlive)
        {
            deathAnimation.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        SpriteEffects flipEffect = facingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        if(isAlive)
        {
            if(isAttacking)
            {
                attackAnimation.Draw(spriteBatch, position, flipEffect);
            }
            else if(isJumping)
            {
                jumpAnimation.Draw(spriteBatch, position, flipEffect);
            }
            else if(isMoving && !isJumping)
            {
                runAnimation.Draw(spriteBatch, position, flipEffect);
            }
            else if(damageTaken)
            {

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
