using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using CODE_OF_STORY.Managers;

namespace CODE_OF_STORY.Core;

public class EnemyCharge : Enemy
{
    protected AnimationPlayer enCRunAnimation;
    protected AnimationPlayer enCAttackAnimation;
    protected AnimationPlayer enCDamageAnimation;
    protected AnimationPlayer enCDeathAnimation;

    public EnemyCharge(Vector2 startPosition, Vector2 patrolEnd, float sightRange, float attackRange, int health, float chargeSpeed, Player player)
        : base(startPosition, patrolEnd, sightRange, attackRange, health, player)
    {
        enCRunAnimation = new AnimationPlayer(ResourceManager.enCRunTexture, frameCount: 6, animationSpeed: 0.1f, playOnce: false);
        enCAttackAnimation = new AnimationPlayer(ResourceManager.enCAttackTexture, frameCount: 4, animationSpeed: 0.1f, playOnce: true);
        enCDamageAnimation = new AnimationPlayer(ResourceManager.enCDamageTexture, frameCount: 2, animationSpeed: 0.1f, playOnce: true);
        enCDeathAnimation = new AnimationPlayer(ResourceManager.enCDeathTexture, frameCount: 4, animationSpeed: 0.1f, playOnce: true);
    }  

    public override async void Update(GameTime gameTime, Player player) 
    {
        base.Update(gameTime, player);
        if(isAlive)
        {
            if(sightRect.Contains(player.Position) && player.isAlive)
            {
                float chargeSpeed;
                chargeSpeed = 300f;
                speed = chargeSpeed;
            }
            else
            {
                speed = 100f;
            }
        }
        else
        {
            enCDeathAnimation.Update(gameTime);
            speed = 0f;
        }
        if (isAttacking)
            {
                enCAttackAnimation.Update(gameTime);
                AttackPlayer(player);
                if (enCAttackAnimation.IsFinished)
                {
                    await Task.Delay(200);
                    enCAttackAnimation.Reset();
                    hasDealtDamage = false;
                }
            }

        enCRunAnimation.Update(gameTime);
    }

            
    
    
    public override async void Draw(SpriteBatch spriteBatch)
    {
        SpriteEffects flipEffect = movingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        if(isAlive)
        {
            if(isAttacking)
            {
                enCAttackAnimation.Draw(spriteBatch, position, flipEffect);
            }
            else if(damageTaken)
            {
                enCDamageAnimation.Draw(spriteBatch, position, flipEffect);
                await Task.Delay(200);
                damageTaken = false;
            }
            else
            {
                enCRunAnimation.Draw(spriteBatch, position, flipEffect);
            }
        }
        else
        {
            enCDeathAnimation.Draw(spriteBatch, position, flipEffect);
        }
    }
}
