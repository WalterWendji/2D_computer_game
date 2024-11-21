using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;

namespace CODE_OF_STORY.Core;

public class EnemyNahkampf : Enemy
{
    protected AnimationPlayer enNRunAnimation;
    protected AnimationPlayer enNAttackAnimation;
    protected AnimationPlayer enNDamageAnimation;
    protected AnimationPlayer enNDeathAnimation;
    protected Texture2D enNRunTexture;
    protected Texture2D enNAttackTexture;
    protected Texture2D enNDamageTexture;
    protected Texture2D enNDeathTexture;

    public EnemyNahkampf(Texture2D enNRunTexture, Texture2D enNAttackTexture, Texture2D enNDamageTexture,Texture2D enNDeathTexture,
                         Vector2 startPosition, Vector2 patrolEnd, float sightRange, int health)
        : base(startPosition, patrolEnd, sightRange, health)
    {
        this.enNRunTexture = enNRunTexture;
        enNRunAnimation = new AnimationPlayer(enNRunTexture, frameCount: 6, animationSpeed: 0.1f, playOnce: false);
        enNAttackAnimation = new AnimationPlayer(enNAttackTexture, frameCount: 4, animationSpeed: 0.1f, playOnce: true);
        enNDamageAnimation = new AnimationPlayer(enNDamageTexture, frameCount: 2, animationSpeed: 0.1f, playOnce: true);
        enNDeathAnimation = new AnimationPlayer(enNDeathTexture, frameCount: 4, animationSpeed: 0.1f, playOnce: true);
        this.enNRunTexture = enNRunTexture;
        this.enNAttackTexture = enNAttackTexture;
        this.enNDamageTexture = enNDamageTexture;
        this.enNDeathTexture = enNDeathTexture;
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
