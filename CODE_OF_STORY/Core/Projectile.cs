using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace CODE_OF_STORY.Core;

public class Projectile
{
    public Texture2D texture {get; private set;} 
    private AnimationPlayer animationArrow;
    public Vector2 Position {get; set; }
    private Vector2 initialDirection;
    private Vector2 velocity;
    private float gravity = 750f;
    public float speed {get; set;}
    public bool IsActive;
    private float flightDistance;
    private float maxDisFall = 10000;

    public Projectile(Texture2D texture, Vector2 startPosition, Vector2 direction, float speed)
    {
        this.texture = texture;
        this.Position = startPosition;
        this.initialDirection = Vector2.Normalize(direction);
        this.velocity = initialDirection * speed;
        this.speed = speed;
        this.flightDistance = 0f;
        this.IsActive = true;
        animationArrow = new AnimationPlayer(texture, frameCount: 1, animationSpeed: 0.1f, playOnce: false);
    }

    public Vector2 Direction => initialDirection;
    
    public void Update(GameTime gameTime)
    {
        if(IsActive)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            flightDistance += velocity.X + velocity.Y* deltaTime;

            if(flightDistance > maxDisFall)
            {
                velocity.Y += gravity * deltaTime;
            }

            Position += velocity * deltaTime;

            if(Position.Y > 700)
            {
                IsActive = false;
            }
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        SpriteEffects flipEffect = (velocity.X < 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        if(IsActive)
        {
            animationArrow.Draw(spriteBatch, Position, flipEffect);    
        }
    }
    
}
