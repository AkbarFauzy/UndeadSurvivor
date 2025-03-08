using System.Collections;
using UnityEngine;
using Agate.MVC.Base;
using Roguelike.Message;
using Roguelike.Module.Exp;

namespace Roguelike.Module.Player
{
    public class PlayerController : ObjectController<PlayerController, PlayerModel, IPlayerModel, PlayerView>
    {
        private Rigidbody2D rbody;
        private LevelManagerController _levelManager;

        public override void SetView(PlayerView view)
        {
            base.SetView(view);
            view.SetCallbacks(OnTriggerItem);
            rbody = view.gameObject.GetComponent<Rigidbody2D>();
            _model.SetHealth(10f);
        }

        public void OnMovePlayer(MovePlayerMessage message)
        {
            Vector2 direction = message.Direction;
            rbody.velocity = direction * _model.Speed;
        }

        public void OnPickupExp(PickupExpOrbMessage message)
        {
            _model.AddExperience(message.Experience);
            while (_model.Experience >= _levelManager.GetXPForLevel(_model.Level)) {
                _model.SubstractExperience(_levelManager.GetXPForLevel(_model.Level));
                _model.LevelUp();
                Publish<PlayerLevelUpMessage>(new PlayerLevelUpMessage(_model.Level));
            }
            Publish<PlayerGainingExperience>(new PlayerGainingExperience(_model.Experience, _levelManager.GetXPForLevel(_model.Level)));
        }

        public void PlayerTakeDamage(PlayerTakeDamageMessage message)
        {
            _model.TakeDamage(message.Damage);
            if (_model.CurrentHealth <= 0) {                                    
                Publish<GameOverMessage>(new GameOverMessage());
            }
        }

        public void PlayerRestoreHealth(PlayerRestoreHealthMessage message)
        {
            _model.RestoreMaxHealth();
        }

        private void OnTriggerItem(GameObject triggeredItem) {
            Publish<TriggerItemMessage>(new TriggerItemMessage(triggeredItem));
        }

    }

}
