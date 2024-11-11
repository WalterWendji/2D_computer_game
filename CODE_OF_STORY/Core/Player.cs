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
    private bool facingRight;
    private Texture2D idleTexture;
    //laufen var
    private Vector2 position;
    private float speed;
    public bool isMoving { get; private set;}
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
    public Player(Texture2D runTexture, Texture2D idleTexture,Texture2D jumpTexture, Vector2 position)
    {
        this.idleTexture = idleTexture;
        runAnimation = new AnimationPlayer(runTexture, frameCount: 6, animationSpeed: 0.1f);
        idleAnimation = new AnimationPlayer(idleTexture, frameCount: 6, animationSpeed: 0.1f);
        jumpAnimation = new AnimationPlayer(jumpTexture, frameCount: 2, animationSpeed: 0.3f);
        this.position = position;
        this.speed = 200f;
        this.groundLevel = position.Y;
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
        if (mouseState.LeftButton == ButtonState.Pressed)
            angreifen
        */
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        SpriteEffects flipEffect = facingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        if(isJumping)
        {
            jumpAnimation.Draw(spriteBatch, position, flipEffect);
        }
        else if(isMoving && !isJumping)
        {
            runAnimation.Draw(spriteBatch, position, flipEffect);
        }
        else
        {
            idleAnimation.Draw(spriteBatch, position, flipEffect);
        }
        
        //spriteBatch.Draw(texture, position, Color.White);
    }
}
