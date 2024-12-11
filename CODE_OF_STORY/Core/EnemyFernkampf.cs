using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using CODE_OF_STORY.Managers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CODE_OF_STORY.Core;

public class EnemyFernkampf : Enemy
{
    protected AnimationPlayer enFRunAnimation;
    protected AnimationPlayer enFAttackAnimation;
    protected AnimationPlayer enFDamageAnimation;
    protected AnimationPlayer enFDeathAnimation;
    //projektil var
    protected bool isShooting;
    private readonly float shootCd = 2f;
    private float startShootCd = 0f;
    private Texture2D arrowTexture;
    private List<Projectile> enemyProjectiles;

    public EnemyFernkampf(Vector2 startPosition, Vector2 patrolEnd, float sightRange, float attackRange, int health, Texture2D arrowTexture)
        : base(startPosition, patrolEnd, sightRange, attackRange, health)
    {
        enFRunAnimation = new AnimationPlayer(ResourceManager.enFWalkTexture, frameCount: 8, animationSpeed: 0.1f, playOnce: false);
        enFAttackAnimation = new AnimationPlayer(ResourceManager.enFShot_1Texture, frameCount: 14, animationSpeed: 0.1f, playOnce: true);
        enFDamageAnimation = new AnimationPlayer(ResourceManager.enFDamageTexture, frameCount: 3, animationSpeed: 0.1f, playOnce: true);
        enFDeathAnimation = new AnimationPlayer(ResourceManager.enFDeathTexture, frameCount: 3, animationSpeed: 0.1f, playOnce: true);

        this.enemyProjectiles = new List<Projectile>();
        this.arrowTexture = arrowTexture;
    }

    private void Shoot(Vector2 targetPosition)
    {
        if(startShootCd < shootCd)
            return;
        isShooting = true;
        startShootCd =0;
        Vector2 direction = targetPosition - position;
        direction.Normalize();

        Projectile arrow = new Projectile(arrowTexture, position + new Vector2(-30, 40), direction, 300f);
        enemyProjectiles.Add(arrow);
    }
    public override void AttackPlayer(Player player)
    {
        //leer gelassen um zu verhindern das doppelt schaden berrechnet wird.
    }

    public override async void Update(GameTime gameTime, Player player) 
    {
        base.Update(gameTime, player);
        startShootCd += (float)gameTime.ElapsedGameTime.TotalSeconds;

        for(int i = enemyProjectiles.Count -1; i >= 0; i--)
        {
            var projectile = enemyProjectiles[i];
            projectile.Update(gameTime);

            if(player.CheckPlayerProjectileCollision(projectile))
            {
                enemyProjectiles.RemoveAt(i);
            }
            else if(!projectile.IsActive)
            {
                enemyProjectiles.RemoveAt(i);
            }
        }
        if(isAlive)
        {
            float distanceToPlayer = Vector2.Distance(position, player.Position);
            if(distanceToPlayer <= attackRange && startShootCd > shootCd)
            {
                Shoot(player.Position);
            }
            if(damageTaken)
            {
                await Task.Delay(300);
                damageTaken = false;
            }
            if (isShooting)
            {
                speed = 0f;
                enFAttackAnimation.Update(gameTime);
                await Task.Delay(1400);
                AttackPlayer(player);
                if (enFAttackAnimation.IsFinished)
                {
                    enFAttackAnimation.Reset();
                    isShooting = false;
                    hasDealtDamage = false;
                }
            }
            else
            {
            enFRunAnimation.Update(gameTime);
            speed = 100;
            }
        }
        else
        {
            enFDeathAnimation.Update(gameTime);
            speed = 0f;
        }  
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        SpriteEffects flipEffect = movingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        if(isAlive)
        {
            if(isShooting && startShootCd < shootCd)
            {
                enFAttackAnimation.Draw(spriteBatch, position, flipEffect);
            }
            else if(damageTaken)
            {
                enFDamageAnimation.Draw(spriteBatch, position, flipEffect);
            }
            else
            {
                enFRunAnimation.Draw(spriteBatch, position, flipEffect);
            }
        }
        else
        {
            enFDeathAnimation.Draw(spriteBatch, position, flipEffect);
        }
        foreach (var projectile in enemyProjectiles)
        {
            projectile.Draw(spriteBatch);
        }
    }
}
