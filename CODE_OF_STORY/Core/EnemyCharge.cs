using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;

namespace CODE_OF_STORY.Core;

public class EnemyCharge : Enemy
{
    protected AnimationPlayer enCRunAnimation;
    protected AnimationPlayer enCAttackAnimation;
    protected AnimationPlayer enCDamageAnimation;
    protected AnimationPlayer enCDeathAnimation;
    protected Texture2D enCRunTexture;
    protected Texture2D enCAttackTexture;
    protected Texture2D enCDamageTexture;
    protected Texture2D enCDeathTexture;


    public EnemyCharge(Texture2D enCRunTexture, Texture2D enCAttackTexture, Texture2D enCDamageTexture,Texture2D enCDeathTexture,
                         Vector2 startPosition, Vector2 patrolEnd, float sightRange, int health, float chargeSpeed)
        : base(startPosition, patrolEnd, sightRange, health)
    {
        this.enCRunTexture = enCRunTexture;
        enCRunAnimation = new AnimationPlayer(enCRunTexture, frameCount: 6, animationSpeed: 0.1f, playOnce: false);
        enCAttackAnimation = new AnimationPlayer(enCAttackTexture, frameCount: 4, animationSpeed: 0.1f, playOnce: true);
        enCDamageAnimation = new AnimationPlayer(enCDamageTexture, frameCount: 2, animationSpeed: 0.1f, playOnce: true);
        enCDeathAnimation = new AnimationPlayer(enCDeathTexture, frameCount: 4, animationSpeed: 0.1f, playOnce: true);
        this.enCRunTexture = enCRunTexture;
        this.enCAttackTexture = enCAttackTexture;
        this.enCDamageTexture = enCDamageTexture;
        this.enCDeathTexture = enCDeathTexture;
    }  

    public override async void Update(GameTime gameTime, Player player) 
    {
        base.Update(gameTime, player);
        if(isAlive)
        {
            if(sightRect.Contains(player.Position))
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
