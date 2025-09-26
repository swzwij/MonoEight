using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class LoadingScene : Scene
{
    private const float WAIT_TIME = 1f;

    private float _timer;
    private bool _isAnimationFinished;

    public override void Awake()
    {
        _timer = 0;
    }

    public override void LoadContent()
    {
        Camera.BackgroundColor = MEColors.Black;

        GameObject logo = new();
        Animator animator = new(logo, new(Content.LoadFromRoot<Texture2D>("Assets", "MonoEightAnimation"), 64))
        {
            FrameDuration = 0.05f,
            Loop = false,
            Scale = Math.Min(MEWindow.Width / 32, MEWindow.Height / 32) / 2
        };

        animator.OnStopped += () => _isAnimationFinished = true;
        animator.Play();

        Add(logo);

        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
        if (_isAnimationFinished)
        {
            _timer += Time.DeltaTime;

            if (_timer >= WAIT_TIME)
                SceneManager.Load("Test 1");
        }

        base.Update(gameTime);
    }
}