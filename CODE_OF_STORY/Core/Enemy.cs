using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;


//using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CODE_OF_STORY.Core;

/* Implements an enemy in the game. 
Used to load, draw, and update an enemy. */
public abstract class Enemy
{
    protected int health;
    //bewegung
    protected AnimationPlayer enRunAnimation;
    protected Texture2D enRunTexture;
    protected Vector2 position;
    protected float speed;
    protected Vector2 patrolStart;
    protected Vector2 patrolEnd;
    protected bool movingRight = true;
    //sicht
    protected float sightRange;
    protected Rectangle sightRect;

    public Enemy(Texture2D enRuntexture, Vector2 startposition, Vector2 patrolEnd, float sightRange, int health)
    {
        this.enRunTexture = enRuntexture;
        enRunAnimation = new AnimationPlayer(enRunTexture, frameCount: 6, animationSpeed: 0.1f);
        this.position = startposition;
        this.patrolStart = startposition;
        this.patrolEnd = patrolEnd;
        this.speed = 150f;
        this.health = health;
        this.movingRight = true;
        this.sightRange = sightRange;
    }

    public virtual void Update(GameTime gameTime, Vector2 playerPosition)
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
        

        enRunAnimation.Update(gameTime);
    }


    public abstract void Draw(SpriteBatch spriteBatch);
    
}
