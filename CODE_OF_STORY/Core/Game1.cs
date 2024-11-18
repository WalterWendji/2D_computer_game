using CODE_OF_STORY.Managers;
using CODE_OF_STORY.Scenes.Gateway;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CODE_OF_STORY.Core;

/* Implements major game components 
such as content and level loading, 
HUD management and display, and game 
object updating. That is also the "Game1" class. */
public class Game1 : Game
{
    public static GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameStateManager gameStateManager;
    private GatewaysManager gatewaysManager;
    public static MouseState currentMouseState, oldMouseState;
    public static Rectangle currentMouseStateRectangle;
    public static int xPosition;
    public static int yPosition;
    public static Texture2D backButton;
    public static Texture2D backButtonColored;
    public static Rectangle backButtonRect;
    public static Rectangle backButtonRectColored;
    private Vector2 backButtonPosition;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = Data.screenW;
        _graphics.PreferredBackBufferHeight = Data.screenH;

        _graphics.ApplyChanges();

        gameStateManager = new GameStateManager();
        gatewaysManager = new GatewaysManager();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        gameStateManager.LoadContent(Content);
        gatewaysManager.LoadContent(Content);

        Viewport viewport = _graphics.GraphicsDevice.Viewport;

        backButtonPosition = new Vector2(viewport.Width-(viewport.Width-10), viewport.Height-60);
        
        xPosition = (int)backButtonPosition.X;
        yPosition = (int)backButtonPosition.Y;

        backButton = Content.Load<Texture2D>("Buttons/Back_Square_Button");
        backButtonColored = Content.Load<Texture2D>("Buttons/ColoredButtons/Back_col_Square_Button");

        backButtonRect = new Rectangle(xPosition, yPosition, backButton.Width/4, backButton.Height/4);
        backButtonRectColored = new Rectangle(xPosition ,yPosition, backButtonColored.Width/4, backButtonColored.Height/4);
    }

    protected override void Update(GameTime gameTime)
    {
        oldMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();
        currentMouseStateRectangle = new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);
        
        if (Data.Exit)
            Exit();

        gameStateManager.Update(gameTime);

        if (Data.currentState == Data.Scenes.StoneAge || Data.currentState == Data.Scenes.MiddleAge || Data.currentState == Data.Scenes.ModernAge || Data.currentState == Data.Scenes.Future)
            gatewaysManager.Update(gameTime);
        
        if (!Player.checkIsAlive && PausePopupMenu.isClicked)
        {
            gatewaysManager.RestartCurrentLevel();
            PausePopupMenu.isClicked = false;
        }
            
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        gameStateManager.Draw(_spriteBatch);
        if (Data.currentState == Data.Scenes.StoneAge || Data.currentState == Data.Scenes.MiddleAge || Data.currentState == Data.Scenes.ModernAge || Data.currentState == Data.Scenes.Future)
            gatewaysManager.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
