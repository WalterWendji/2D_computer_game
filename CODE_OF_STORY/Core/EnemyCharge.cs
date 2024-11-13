using System;
//using System.Numerics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;

namespace CODE_OF_STORY.Core;

public class EnemyCharge : Enemy
{
    private float chargeSpeed;

    public EnemyCharge(Texture2D enRunTexture, Texture2D enAttackTexture, Texture2D enDamageTexture,Texture2D enDeathTexture,
                         Vector2 startPosition, Vector2 patrolEnd, float speed, float sightRange, int health, float chargeSpeed)
        : base(enRunTexture, enAttackTexture, enDamageTexture, enDeathTexture, startPosition, patrolEnd, sightRange, health)
    {
        this.enRunTexture = enRunTexture;
        this.enAttackTexture = enAttackTexture;
        this.enDamageTexture = enDamageTexture;
        this.enDeathTexture = enDeathTexture;
    }  

    public override void Update(GameTime gameTime, Player player) 
    {
        base.Update(gameTime, player);
        if(isAlive)
        {
        if(sightRect.Contains(player.Position))
        {
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
            speed = 0f;
        }
    } 

    public override async void Draw(SpriteBatch spriteBatch)
    {
        SpriteEffects flipEffect = movingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        if(isAlive)
        {
            if(isAttacking)
            {
                enAttackAnimation.Draw(spriteBatch, position, flipEffect);
            }
            else if(damageTaken)
            {
                enDamageAnimation.Draw(spriteBatch, position, flipEffect);
                await Task.Delay(200);
                damageTaken = false;
            }
            else
            {
                enRunAnimation.Draw(spriteBatch, position, flipEffect);
            }
        }
        else
        {
            enDeathAnimation.Draw(spriteBatch, position, flipEffect);
        }
    }
}
