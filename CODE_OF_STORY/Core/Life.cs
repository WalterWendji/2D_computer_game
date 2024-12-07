using System;
using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CODE_OF_STORY.Core;
/** Implements a floating Heart in the game.
 Heart are collectable by the player, 
 gives the Player Chances & a set amount Lifes.
Hearts are used to load, draw, and update a Heart. */

public class Life
{
    public Vector2 Position{get; private set;}
    
public Life(Vector2 position)
{
    Position = position;

}
public void Update(GameTime gameTime)
{
    
}

public void Draw(SpriteBatch spriteBatch)
{
}

}
