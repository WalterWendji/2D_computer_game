using System;
using CODE_OF_STORY.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace CODE_OF_STORY.Scenes;

internal class GatewaysScene : Component
{
    private MouseState currentMouseState, oldMouseState;

    private Rectangle currentMouseStateRectangle;
    private Texture2D gatewaysTexture;
    private Rectangle gatewaysRectangle;
    private Rectangle[] stageRectangle;
    private Texture2D pixel;

    private int[] xPositionStageRectangle;
    private int[] yPositionStageRectangle;
    int stageRectangleWidth; //The width of each gateways in the game
    int stageRectangleHeight; // The height of each gateways in the game
    int initialPositionYStage; // Y position of the first gateways in the top left
    int initialPositionXStage; // X position of the first gateway in the top left

    Color bgColor;
    Color stageRectangleColor;

    public GatewaysScene()
    {
        byte redValue = 1;
        byte greenValue = 17;
        byte blueValue = 25;

        stageRectangleWidth = 521;
        stageRectangleHeight = 388;
        initialPositionXStage = 262;
        initialPositionYStage = 21;

        xPositionStageRectangle = new int[] { 262, (stageRectangleWidth + initialPositionXStage + 33) };
        yPositionStageRectangle = new int[] { initialPositionYStage, (stageRectangleHeight + initialPositionYStage + 30) };

        stageRectangle = new Rectangle[4];

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
        gatewaysRectangle = new Rectangle(xPosition, 0, gatewaysTexture.Width, (int)(gatewaysTexture.Height / 1.18));

        pixel = new Texture2D(Game1._graphics.GraphicsDevice, 1, 1);
        pixel.SetData(new[] { Color.White });

        for (int i = 0; i < yPositionStageRectangle.Length; i++)
        {
            for (int j = 0; j < stageRectangle.Length; j++)
            {
                if (i != 1 && j < 2)
                {
                    stageRectangle[j] = new Rectangle(xPositionStageRectangle[j], yPositionStageRectangle[i], stageRectangleWidth, stageRectangleHeight);
                }
                if (i == 1 && j >= 2)
                {
                    stageRectangle[j] = new Rectangle(xPositionStageRectangle[j - 2], yPositionStageRectangle[i], stageRectangleWidth, stageRectangleHeight);
                }
            }
        }
        stageRectangleColor = bgColor;

    }

    internal override void Update(GameTime gameTime)
    {
        oldMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();
        currentMouseStateRectangle = new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);

        if (currentMouseState.LeftButton == ButtonState.Pressed)
        {
            if (currentMouseStateRectangle.Intersects(stageRectangle[0]))
                Data.currentState = Data.Scenes.StoneAge;
            else if (currentMouseStateRectangle.Intersects(stageRectangle[1]))
                Data.currentState = Data.Scenes.MiddleAge;

        }
    }

    internal override void Draw(SpriteBatch spriteBatch)
    //#01121A
    {
        Game1._graphics.GraphicsDevice.Clear(bgColor);

        spriteBatch.Draw(gatewaysTexture, gatewaysRectangle, Color.White);
        for (int i = 0; i < stageRectangle.Length; i++)
        {
            if (i == 0)
            {
                spriteBatch.Draw(pixel, stageRectangle[i], stageRectangleColor * 0f);
            }
            else
            {
                spriteBatch.Draw(pixel, stageRectangle[i], stageRectangleColor * 0.5f);
            }

            if (currentMouseStateRectangle.Intersects(stageRectangle[i]))
            {
                spriteBatch.Draw(pixel, stageRectangle[i], Color.SeaGreen * 0.2f);
            }
        }

    }
}
