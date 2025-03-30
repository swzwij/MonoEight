using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public class Main : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private RenderTarget2D _renderTarget;

    public Main()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        ContentLoader.Initialize(Content);
        GameWindow.StartFullscreen = true;
        GameWindow.Initialize(_graphics, Window);
        Camera.Initialize();

        StateManager.AddState("Loading", new LoadingState());
        StateManager.AddState("Title", new TitleState());
        StateManager.AddState("Game", new GameState());

        StateManager.ChangeState("Loading");

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _renderTarget = new RenderTarget2D(GraphicsDevice, GameWindow.Width, GameWindow.Height);
    }

    protected override void Update(GameTime gameTime)
    {
        Input.Update();

        if (Input.IsKeyPressed(Keys.OemPlus))
            GameWindow.Scale++;

        if (Input.IsKeyPressed(Keys.OemMinus))
            GameWindow.Scale--;

        if (Input.IsKeyPressed(Keys.F11))
            GameWindow.ToggleFullscreen();

        if (Input.IsBackPressed && StateManager.CurrentStateName == "Title")
            Exit();

        StateManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(_renderTarget);
        GraphicsDevice.Clear(Camera.BackgroundColor);

        _spriteBatch.Begin(transformMatrix: Camera.Transform, samplerState: SamplerState.PointClamp);
        StateManager.Draw(_spriteBatch);
        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        Rectangle targetRect = GameWindow.IsFullscreen
            ? GraphicsHelper.CalculateFullscreenRect(GraphicsDevice)
            : new Rectangle(0, 0, GameWindow.ScaledWidth, GameWindow.ScaledHeight);

        _spriteBatch.Draw(_renderTarget, targetRect, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}