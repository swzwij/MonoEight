using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class GameStateManager
{
    private static GameStateManager _instance;
    private readonly Dictionary<string, GameState> _states;
    private GameState _currentState;
    private string _currentStateName;

    public GameState CurrentState => _currentState;

    public string CurrentStateName => _currentStateName;

    public static GameStateManager Instance
    {
        get
        {
            _instance ??= new GameStateManager();
            return _instance;
        }
    }

    private GameStateManager()
    {
        _states = [];
    }

    public void AddState(string stateName, GameState state)
    {
        if (_states.TryAdd(stateName, state))
            return;
        throw new ArgumentException($"State '{stateName}' already exists in the manager");
    }

    public void ChangeState(string stateName)
    {
        if (_states.TryGetValue(stateName, out GameState value))
        {
            _currentState?.UnloadContent();

            _currentStateName = stateName;
            _currentState = value;

            _currentState.Initialize();
            _currentState.LoadContent();
        }
        else
        {
            throw new ArgumentException($"State '{stateName}' does not exist in the manager");
        }
    }

    public void Update(GameTime gameTime)
    {
        _currentState?.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _currentState?.Draw(spriteBatch);
    }
}