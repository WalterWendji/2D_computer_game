using System;
//using System.Numerics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CODE_OF_STORY.Core;

public class EnemyCharge : Enemy
{
    private float chargeSpeed;

    public EnemyCharge(Texture2D enRunTexture, Texture2D enAttackTexture, Vector2 startPosition, Vector2 patrolEnd, float speed, float sightRange, int health, float chargeSpeed)
        : base(enRunTexture, enAttackTexture, startPosition, patrolEnd, sightRange, health)
    {
        this.enRunTexture = enRunTexture;
        this.enAttackTexture = enAttackTexture;
    }  

    public override void Update(GameTime gameTime, Player player) 
    {
        base.Update(gameTime, player);

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

    public override void Draw(SpriteBatch spriteBatch)
    {
        SpriteEffects flipEffect = movingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        if(isAlive)
        {
            if(isAttacking)
            {
                enAttackAnimation.Draw(spriteBatch, position, flipEffect);
            }
            else
            {
                enRunAnimation.Draw(spriteBatch, position, flipEffect);
            }
        }
    }
}
