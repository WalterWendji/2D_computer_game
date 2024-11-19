using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;

namespace CODE_OF_STORY.Core;

public class EnemyTank : Enemy
{
    protected AnimationPlayer enTWalkAnimation;
    protected AnimationPlayer enTAttackAnimation;
    protected AnimationPlayer enTDamageAnimation;
    protected AnimationPlayer enTDeathAnimation;
    protected Texture2D enTWalkTexture;
    protected Texture2D enTAttackTexture;
    protected Texture2D enTDamageTexture;
    protected Texture2D enTDeathTexture;
    public EnemyTank(Texture2D enTWalkTexture, Texture2D enTAttackTexture, Texture2D enTDamageTexture,Texture2D enTDeathTexture,
                         Vector2 startPosition, Vector2 patrolEnd, float sightRange, int health)
        : base(startPosition, patrolEnd, sightRange, health)
    {
        this.enTWalkTexture = enTWalkTexture;
        enTWalkAnimation = new AnimationPlayer(enTWalkTexture, frameCount: 8, animationSpeed: 0.2f, playOnce: false);
        enTAttackAnimation = new AnimationPlayer(enTAttackTexture, frameCount: 4, animationSpeed: 0.1f, playOnce: true);
        enTDamageAnimation = new AnimationPlayer(enTDamageTexture, frameCount: 3, animationSpeed: 0.1f, playOnce: true);
        enTDeathAnimation = new AnimationPlayer(enTDeathTexture, frameCount: 4, animationSpeed: 0.1f, playOnce: true);

        this.enTWalkTexture = enTWalkTexture;
        this.enTAttackTexture = enTAttackTexture;
        this.enTDamageTexture = enTDamageTexture;
        this.enTDeathTexture = enTDeathTexture;
        this.patrolStart = new Vector2(300,600);
        this.patrolEnd = new Vector2(600,600);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage -5);
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
       
    }

    public override async void Draw(SpriteBatch spriteBatch)
    {
        SpriteEffects flipEffect = movingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        if(isAlive)
        {
            if(isAttacking)
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
