using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using CODE_OF_STORY.Managers;

namespace CODE_OF_STORY.Core;

public class EnemyTank : Enemy
{
    protected AnimationPlayer enTWalkAnimation;
    protected AnimationPlayer enTAttackAnimation;
    protected AnimationPlayer enTDamageAnimation;
    protected AnimationPlayer enTDeathAnimation;
    protected AnimationPlayer enTBlockAnimation;
    private bool isBlocking;
    public EnemyTank(Vector2 startPosition, Vector2 patrolEnd, float sightRange, float attackRange, int health, Player player)
        : base(startPosition, patrolEnd, sightRange, attackRange, health, player)
    {
        enTWalkAnimation = new AnimationPlayer(ResourceManager.enTWalkTexture, frameCount: 8, animationSpeed: 0.2f, playOnce: false);
        enTAttackAnimation = new AnimationPlayer(ResourceManager.enTAttackTexture, frameCount: 4, animationSpeed: 0.1f, playOnce: true);
        enTDamageAnimation = new AnimationPlayer(ResourceManager.enTDamageTexture, frameCount: 3, animationSpeed: 0.1f, playOnce: true);
        enTDeathAnimation = new AnimationPlayer(ResourceManager.enTDeathTexture, frameCount: 4, animationSpeed: 0.1f, playOnce: true);
        enTBlockAnimation = new AnimationPlayer(ResourceManager.enTBlockTexture, frameCount: 2, animationSpeed: 0.1f, playOnce: true);

        this.patrolStart = new Vector2(300,600);
        this.patrolEnd = new Vector2(600,600);
    }
    public override void TakeDamage(int damage, Vector2 attackerPosition)
    {
        bool attackFromFront = movingRight
            ? attackerPosition.X > Position.X
            : attackerPosition.X < Position.X;
        if(attackFromFront)
        {
            damageTaken = false;
            isBlocking = true;
            return;
        }
        base.TakeDamage(damage -5, position);
    }
    public override bool CheckProjectileCollision(Projectile projectile)
    {
        
        if(!projectile.IsActive)
            return false;
        Rectangle enemyBounds = new Rectangle((int)Position.X,(int)Position.Y, 64, 64);
        Rectangle projectileBounds = new Rectangle((int)projectile.Position.X,(int)projectile.Position.Y, 16, 5);
        if(enemyBounds.Intersects(projectileBounds) && isAlive)
        {
            TakeDamage(20, projectile.Position);
            projectile.IsActive = false;
            damageTaken = true;
            return true;
        }
        return false;
    }

    public override async void Update(GameTime gameTime, Player player) 
    {
        base.Update(gameTime, player);
        if(isAlive)
        {
            enTWalkAnimation.Update(gameTime);
            speed = 50;
        }
        else
        {
            enTDeathAnimation.Update(gameTime);
            speed = 0f;
        }
        if (isAttacking)
        {
            enTAttackAnimation.Update(gameTime);
            AttackPlayer(player);
            if (enTAttackAnimation.IsFinished)
            {
                await Task.Delay(400);
                enTAttackAnimation.Reset();
                hasDealtDamage = false;
            }
        }
        if(isBlocking)
        {
            enTBlockAnimation.Update(gameTime);
            await Task.Delay(200);
            isBlocking =false;
        }
       
    }

    public override async void Draw(SpriteBatch spriteBatch)
    {
        SpriteEffects flipEffect = movingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        if(isAlive)
        {
            if(isBlocking)
            {
                enTBlockAnimation.Draw(spriteBatch, position, flipEffect);
            }
            else if(isAttacking)
            {
                enTAttackAnimation.Draw(spriteBatch, position, flipEffect);
            }
            else if(damageTaken)
            {
                enTDamageAnimation.Draw(spriteBatch, position, flipEffect);
                await Task.Delay(200);
                damageTaken = false;
            }
            else
            {
                enTWalkAnimation.Draw(spriteBatch, position, flipEffect);
            }
        }
        else
        {
            enTDeathAnimation.Draw(spriteBatch, position, flipEffect);
        }
    }
}
