using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using CODE_OF_STORY.Managers;
using System.Collections.Generic;

namespace CODE_OF_STORY.Core;

public class EnemyFernkampf : Enemy
{
    protected AnimationPlayer enFRunAnimation;
    protected AnimationPlayer enFAttackAnimation;
    protected AnimationPlayer enFDamageAnimation;
    protected AnimationPlayer enFDeathAnimation;
    //projektil var
    private readonly float shootCd = 2f;
    private float startShootCd = 0f;
    private Texture2D arrowTexture;
    private List<Projectile> projectiles;

    public EnemyFernkampf(Vector2 startPosition, Vector2 patrolEnd, float sightRange, float attackRange, int health, Texture2D arrowTexture)
        : base(startPosition, patrolEnd, sightRange, attackRange, health)
    {
        enFRunAnimation = new AnimationPlayer(ResourceManager.enFWalkTexture, frameCount: 8, animationSpeed: 0.1f, playOnce: false);
        enFAttackAnimation = new AnimationPlayer(ResourceManager.enFShot_1Texture, frameCount: 14, animationSpeed: 0.1f, playOnce: true);
        enFDamageAnimation = new AnimationPlayer(ResourceManager.enFDamageTexture, frameCount: 3, animationSpeed: 0.1f, playOnce: true);
        enFDeathAnimation = new AnimationPlayer(ResourceManager.enFDeathTexture, frameCount: 3, animationSpeed: 0.1f, playOnce: true);

        this.projectiles = new List<Projectile>();
        this.arrowTexture = arrowTexture;
    }

    private void Shoot(Vector2 targetPosition)
    {
        if(startShootCd < shootCd)
            return;
        isAttacking = true;
        startShootCd =0;
        Vector2 direction = targetPosition - position;
        direction.Normalize();

        Projectile arrow = new Projectile(arrowTexture, position, direction, 300f);
        projectiles.Add(arrow);
    }

    public override async void Update(GameTime gameTime, Player player) 
    {
        base.Update(gameTime, player);
        startShootCd += (float)gameTime.ElapsedGameTime.TotalSeconds;

        for(int i = projectiles.Count -1; i >= 0; i--)
        {
            projectiles[i].Update(gameTime);
            if(!projectiles[i].IsActive)
            {
                projectiles.RemoveAt(i);
            }
        }
        if(isAlive)
        {
            float distanceToPlayer = Vector2.Distance(position, player.Position);
            if(distanceToPlayer <= attackRange)
            {
                Shoot(player.Position);
            }

            if (isAttacking)
            {
                speed = 0f;
                enFAttackAnimation.Update(gameTime);
                await Task.Delay(1400);
                AttackPlayer(player);
                if (enFAttackAnimation.IsFinished)
                {
                    enFAttackAnimation.Reset();
                    isAttacking = false;
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

    public override async void Draw(SpriteBatch spriteBatch)
    {
        SpriteEffects flipEffect = movingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        if(isAlive)
        {
            if(isAttacking && startShootCd < shootCd)
            {
                enFAttackAnimation.Draw(spriteBatch, position, flipEffect);
            }
            else if(damageTaken)
            {
                enFDamageAnimation.Draw(spriteBatch, position, flipEffect);
                await Task.Delay(200);
                damageTaken = false;
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
        foreach (var projectile in projectiles)
        {
            projectile.Draw(spriteBatch);
        }
    }
}
