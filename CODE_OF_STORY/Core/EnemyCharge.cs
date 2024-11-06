using System;
//using System.Numerics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CODE_OF_STORY.Core;

public class EnemyCharge : Enemy
{
    private float chargeSpeed;

    public EnemyCharge(Texture2D enRunTexture, Vector2 startPosition, Vector2 patrolEnd, float speed, float sightRange, int health, float chargeSpeed)
        : base(enRunTexture, startPosition, patrolEnd, sightRange, health)
    {
        this.health = health;
        this.enRunTexture = enRunTexture;
    }  

    public override void Update(GameTime gameTime, Vector2 playerPosition) 
    {
        base.Update(gameTime, playerPosition);

        if(sightRect.Contains(playerPosition))
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
        
        enRunAnimation.Draw(spriteBatch, position, flipEffect);
    }
}
