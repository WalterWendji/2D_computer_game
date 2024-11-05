using System;
using System.Runtime.CompilerServices;

//using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CODE_OF_STORY.Core;

/* Implements an enemy in the game. 
Used to load, draw, and update an enemy. */
public class Enemy
{
    //bewegung
    private AnimationPlayer enRunAnimation;
    private Texture2D enRunTexture;
    private Vector2 position;
    private float speed;
    private Vector2 patrolStart;
    private Vector2 patrolEnd;
    private bool movingRight = true;
    //sicht
    private float sightRange = 100f;
    private Rectangle sightRect;

    public Enemy(Texture2D enRuntexture, Vector2 startposition, Vector2 patrolEnd, float sightRange)
    {
        this.enRunTexture = enRuntexture;
        enRunAnimation = new AnimationPlayer(enRunTexture, frameCount: 6, animationSpeed: 0.1f);
        this.position = startposition;
        this.patrolStart = startposition;
        this.patrolEnd = patrolEnd;
        this.speed = 150f;
        this.movingRight = true;
        this.sightRange = sightRange;
    }

    public void Update(GameTime gameTime, Vector2 playerPosition)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if(movingRight)
        {
            position.X += speed * deltaTime;
            sightRect = new Rectangle((int)position.X, (int)position.Y, (int)sightRange, 64);

            if(position.X >= patrolEnd.X)
            {
                position.X = patrolEnd.X;
                
                movingRight = false;
            }
        }
        else
        {
            position.X -= speed * deltaTime;
            sightRect = new Rectangle((int)position.X - (int)sightRange, (int)position.Y, (int)sightRange, 64);

            if(position.X <= patrolStart.X)
            {
                position.X = patrolStart.X;
                movingRight = true;
            }
        }

        //was soll passieren wenn der gegner den spieler sieht
        if(sightRect.Contains(playerPosition))
        {
                speed = 300f;
        }

        enRunAnimation.Update(gameTime);
    }


    public void Draw(SpriteBatch spriteBatch)
    {
        SpriteEffects flipEffect = movingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        
        enRunAnimation.Draw(spriteBatch, position, flipEffect);
        
    }
}
