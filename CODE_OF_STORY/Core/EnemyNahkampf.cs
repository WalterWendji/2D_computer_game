using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using CODE_OF_STORY.Managers;

namespace CODE_OF_STORY.Core;

public class EnemyNahkampf : Enemy
{
    protected AnimationPlayer enNRunAnimation;
    protected AnimationPlayer enNAttackAnimation;
    protected AnimationPlayer enNDamageAnimation;
    protected AnimationPlayer enNDeathAnimation;

    public EnemyNahkampf(Vector2 startPosition, Vector2 patrolEnd, float sightRange, float attackRange, int health)
        : base(startPosition, patrolEnd, sightRange, attackRange, health)
    {
        enNRunAnimation = new AnimationPlayer(ResourceManager.enNRunTexture, frameCount: 6, animationSpeed: 0.1f, playOnce: false);
        enNAttackAnimation = new AnimationPlayer(ResourceManager.enNAttackTexture, frameCount: 4, animationSpeed: 0.1f, playOnce: true);
        enNDamageAnimation = new AnimationPlayer(ResourceManager.enNDamageTexture, frameCount: 2, animationSpeed: 0.1f, playOnce: true);
        enNDeathAnimation = new AnimationPlayer(ResourceManager.enNDeathTexture, frameCount: 4, animationSpeed: 0.1f, playOnce: true);
    }  

    public override async void Update(GameTime gameTime, Player player) 
    {
        base.Update(gameTime, player);
        if(isAlive)
        {
            enNRunAnimation.Update(gameTime);
            speed = 100;
        }
        else
        {
            enNDeathAnimation.Update(gameTime);
            speed = 0f;
        }
        if (isAttacking)
            {
                enNAttackAnimation.Update(gameTime);
                AttackPlayer(player);
                if (enNAttackAnimation.IsFinished)
                {
                    await Task.Delay(200);
                    enNAttackAnimation.Reset();
                    hasDealtDamage = false;
                }
            }

    }

    public override async void Draw(SpriteBatch spriteBatch)
    {
        SpriteEffects flipEffect = movingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        if(isAlive)
        {
            if(isAttacking)
            {
                enNAttackAnimation.Draw(spriteBatch, position, flipEffect);
            }
            else if(damageTaken)
            {
                enNDamageAnimation.Draw(spriteBatch, position, flipEffect);
                await Task.Delay(200);
                damageTaken = false;
            }
            else
            {
                enNRunAnimation.Draw(spriteBatch, position, flipEffect);
            }
        }
        else
        {
            enNDeathAnimation.Draw(spriteBatch, position, flipEffect);
        }
    }
}
