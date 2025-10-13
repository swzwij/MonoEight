using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEight;

public class Player : GameObject
{
    private Animator _animator;
    private int _count;

    public Player(string texture)
    {
        _animator = new Animator
        (
            new SpriteSheet(Content.Load<Texture2D>(texture), 16),
            [
                new("Idle", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1),
                new("Squish", 0, 2, 3, 2, 0) { Loop = false }
            ]
        )
        {
            FrameDuration = 0.5f
        };

        AddComponent(_animator);

        _animator.Play("Idle");
        _animator.OnFinished += OnAnimationFinished;

        _count = PlayerPrefs.Get("Count", 0);
    }

    protected override void Update()
    {
        Position += new Vector2(Input.InputAxis.X, Input.InputAxis.Y) * Time.DeltaTime * 10;

        if (Input.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.E))
        {
            _animator.Play("Squish");
            _count++;
            PlayerPrefs.Set("Count", _count);
        }

        if (Input.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            SceneManager.Load("Test 1");
    }

    protected override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        Canvas.DrawText(spriteBatch, $"Count: {_count}", FontSize.S, new(0, -10), MEColors.Black);
    }

    private void OnAnimationFinished(string animationName)
    {
        if (animationName == "Squish")
            _animator.Play("Idle");
    }
}