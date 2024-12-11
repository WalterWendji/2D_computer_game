using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CODE_OF_STORY.Scenes.Gateway;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CODE_OF_STORY.Core;

/* Implements an enemy in the game. 
Used to load, draw, and update an enemy. */
public abstract class Enemy
{
    //fighting var
    protected int health;
    public bool isAlive => health > 0;
    protected bool isAttacking;
    public bool damageTaken;
    public Vector2 Position => position;
    protected bool hasDealtDamage = false;
    //bewegung
    
    protected Vector2 position;
    protected float speed;
    protected Vector2 patrolStart;
    protected Vector2 patrolEnd;
    protected bool movingRight;
    //sicht
    private float sightRange;
    protected float attackRange;
    protected Rectangle sightRect;

    protected Enemy(Vector2 startposition, Vector2 patrolEnd, float sightRange, float attackRange, int initialhealth)
    {

        this.position = startposition;
        this.patrolStart = startposition;
        this.patrolEnd = patrolEnd;
        this.speed = 150f;
        this.health = initialhealth;
        this.movingRight = true;
        this.sightRange = sightRange;
        this.attackRange = attackRange;
    }

    public virtual void TakeDamage(int damage, Vector2 attackerPosition)
    {
        
        health -= damage;
        if (health <= 0)
        {
            health = 0;
        }
    }

    public void ResetEnemy()
    {
        patrolStart = StoneAge.enemyStartPosition;
        position = StoneAge.enemyStartPosition;
        speed = 150f;
        sightRange = 100f;
        health = 100;
        patrolEnd = StoneAge.enemyEndPosition;
    }
    public virtual void AttackPlayer(Player player)
    {
        if (isAttacking && !hasDealtDamage && IsPlayerInRange(player))
        {
            player.TakeDamage(10);
            hasDealtDamage = true;
        }
    }

    public bool IsPlayerInRange(Player player)
    {
        
        if (movingRight)
        {
            return (player.Position.X > position.X && player.Position.X <= position.X + attackRange);
        }
        else
        {
            return (player.Position.X < position.X && player.Position.X >= position.X - attackRange);
        }
    }

    public virtual bool CheckProjectileCollision(Projectile projectile)
    {
        if(isAlive)
        {
            if(!projectile.IsActive)
                return false;
            Rectangle enemyBounds = new Rectangle((int)Position.X,(int)Position.Y, 64, 64);
            Rectangle projectileBounds = new Rectangle((int)projectile.Position.X,(int)projectile.Position.Y, 16, 5);
            if(enemyBounds.Intersects(projectileBounds))
            {
                TakeDamage(20, position);
                projectile.IsActive=false;
                damageTaken = true;
                return true;
            }
        }
        return false;
    }

    public virtual void Update(GameTime gameTime, Player player)
    {
        if (isAlive)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (movingRight)
            {
                position.X += speed * deltaTime;
                sightRect = new Rectangle((int)position.X, (int)position.Y, (int)sightRange, 64);

                if (position.X >= patrolEnd.X)
                {
                    position.X = patrolEnd.X;

                    movingRight = false;
                }
            }
            else
            {
                position.X -= speed * deltaTime;
                sightRect = new Rectangle((int)position.X - (int)sightRange, (int)position.Y, (int)sightRange, 64);

                if (position.X <= patrolStart.X)
                {
                    position.X = patrolStart.X;
                    movingRight = true;
                }
            }
           
            if (IsPlayerInRange(player))
            {
                isAttacking = true;
            }
            else
            {
                isAttacking = false;
                hasDealtDamage = false;
            } 
        }
    }


    public abstract void Draw(SpriteBatch spriteBatch);

}
