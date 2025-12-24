using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

/// <summary>
/// Represents the main game class for a MonoEight application, handling initialization, updating, and rendering.
/// </summary>
public class Main : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private RenderTarget2D _renderTarget;

    public Main()
    {
        _graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        MonoEight.Content.Initialize(Content, "Content");
        MEWindow.StartFullscreen = false;
        MEWindow.Resolution = new Point(128, 96);
        MEWindow.Initialize(_graphics, Window);
        Debugger.Initialize(_graphics);
        Input.Add("Exit", [Keys.Escape, Keys.Back], []);
        Input.Add("A", [Keys.Z, Keys.C, Keys.K, Keys.Space], []);
        Input.Add("B", [Keys.X, Keys.L], []);
        PlayerPrefs.Initialize();

        SceneManager.Add("Loading", new LoadingScene());
        SceneManager.Add("Test 1", new TestScene());
        SceneManager.Add("Test 2", new TestScene2());

        SceneManager.Load("Loading");

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _renderTarget = new RenderTarget2D(GraphicsDevice, MEWindow.Resolution.X, MEWindow.Resolution.Y);
    }

    protected override void Update(GameTime gameTime)
    {
        Input.Update();
        Time.Update(gameTime);

        if (Input.IsKeyPressed(Keys.F11))
            MEWindow.ToggleFullscreen();

        SceneManager.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(_renderTarget);
        GraphicsDevice.Clear(SceneManager.ActiveScene.Camera.BackgroundColor);

        _spriteBatch.Begin
        (
            SpriteSortMode.FrontToBack,
            null,
            SamplerState.PointClamp,
            null,
            null,
            null,
            SceneManager.ActiveScene.Camera.Transform
        );

        SceneManager.Draw(_spriteBatch);

        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        Rectangle targetRect = GraphicsHelper.CalculateDisplayRect(GraphicsDevice);

        _spriteBatch.Draw(_renderTarget, targetRect, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}