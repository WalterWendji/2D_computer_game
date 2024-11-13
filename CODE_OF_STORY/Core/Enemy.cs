using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
//using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CODE_OF_STORY.Core;

/* Implements an enemy in the game. 
Used to load, draw, and update an enemy. */
public abstract class Enemy
{
    //fighting var
    protected int health = 100;
    public bool isAlive => health > 0;
    protected bool isAttacking;
    public bool damageTaken;
    public Vector2 Position => position;
    private bool hasDealtDamage = false;
    //bewegung
    protected AnimationPlayer enRunAnimation;
    protected AnimationPlayer enAttackAnimation;
    protected AnimationPlayer enDamageAnimation;
    protected AnimationPlayer enDeathAnimation;
    protected Texture2D enRunTexture;
    protected Texture2D enAttackTexture;
    protected Texture2D enDamageTexture;
    protected Texture2D enDeathTexture;
    protected Vector2 position;
    protected float speed;
    protected Vector2 patrolStart;
    protected Vector2 patrolEnd;
    protected bool movingRight = true;
    //sicht
    protected float sightRange;
    protected Rectangle sightRect;

    public Enemy(Texture2D enRunTexture, Texture2D enAttackTexture, Texture2D enDamageTexture, Texture2D enDeathTexture, Vector2 startposition, Vector2 patrolEnd, float sightRange, int initialhealth)
    {
        this.enRunTexture = enRunTexture;
        enRunAnimation = new AnimationPlayer(enRunTexture, frameCount: 6, animationSpeed: 0.1f, playOnce: false);
        enAttackAnimation = new AnimationPlayer(enAttackTexture, frameCount: 4, animationSpeed: 0.1f, playOnce: true);
        enDamageAnimation = new AnimationPlayer(enDamageTexture, frameCount: 2, animationSpeed: 0.1f, playOnce: true);
        enDeathAnimation = new AnimationPlayer(enDeathTexture, frameCount: 4, animationSpeed: 0.1f, playOnce: true);
        this.position = startposition;
        this.patrolStart = startposition;
        this.patrolEnd = patrolEnd;
        this.speed = 150f;
        this.health = initialhealth;
        this.movingRight = true;
        this.sightRange = sightRange;
    }

    public virtual void TakeDamage(int damage)
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

    public void AttackPlayer(Player player)
    {
        if(isAttacking && !hasDealtDamage && IsPlayerInRange(player))
        {
            player.TakeDamage(10);
            hasDealtDamage = true;
        }
    }


    public bool IsPlayerInRange(Player player)
    {
        float attackRange = 50f;
        if(movingRight)
        {
            return(player.Position.X > position.X && player.Position.X <= position.X + attackRange);
        }
        else
        {
             return(player.Position.X < position.X && player.Position.X >= position.X - attackRange);
        }
    }

    public virtual async void Update(GameTime gameTime, Player player)
    {
        if(isAlive)
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

        if(IsPlayerInRange(player))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
            hasDealtDamage = false;
        }

        if(isAttacking)
        {
            enAttackAnimation.Update(gameTime);
            AttackPlayer(player);
            if(enAttackAnimation.IsFinished)
            {
                await Task.Delay(200);
                enAttackAnimation.Reset();
                hasDealtDamage = false;
            }
        }
        //was soll passieren wenn der gegner den spieler sieht
        
        enRunAnimation.Update(gameTime);
        }
        else
        {
          enDeathAnimation.Update(gameTime);  
        }
    }


    public abstract void Draw(SpriteBatch spriteBatch);
    
}
