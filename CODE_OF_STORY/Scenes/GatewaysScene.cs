using System;
using CODE_OF_STORY.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace CODE_OF_STORY.Scenes;

internal class GatewaysScene : Component
{

    private Texture2D gatewaysTexture;
    private Rectangle gatewaysRectangle;
    Color bgColor;


    public GatewaysScene()
    {
        byte redValue = 1;
        byte greenValue = 17;
        byte blueValue = 25;
        gatewaysRectangle = new Rectangle();
        bgColor = new Color(redValue, greenValue, blueValue);
    }
    internal override void LoadContent(ContentManager Content)
    {
        gatewaysTexture = Content.Load<Texture2D>("Items/gateways");
        

        Viewport viewport = Game1._graphics.GraphicsDevice.Viewport;
        int screenWidth = viewport.Width;
        int screenHeight = viewport.Height;

        int xPosition = screenWidth / 6;
        int yPosition = screenHeight / 16;
        gatewaysRectangle = new Rectangle(xPosition, 0, gatewaysTexture.Width, (int)(gatewaysTexture.Height/1.18));
    }

    internal override void Update(GameTime gameTime)
    {

    }
    internal override void Draw(SpriteBatch spriteBatch)
    //#01121A
    {
        Game1._graphics.GraphicsDevice.Clear(bgColor);
        spriteBatch.Draw(gatewaysTexture, gatewaysRectangle, Color.White);
    }
}
