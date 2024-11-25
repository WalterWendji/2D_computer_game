using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CODE_OF_STORY.Core;

internal abstract class Component
{
    internal abstract void LoadContent(ContentManager Content);
    internal abstract void Update(GameTime gameTime);
    internal abstract void Draw(SpriteBatch spriteBatch);	
}
