using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agate.MVC.Base;
using Roguelike.Message;
using Roguelike.Module.Weapon;

namespace Roguelike.Module.HUD 
{
    public class PowerUpCardController : ObjectController<PowerUpCardController, PowerUpCardModel, IPowerUpCardModel,PowerUpCardView>
    {
        HUDController _hud;

        public void Init(PowerUpCardModel model, PowerUpCardView view, HUDController hud) {
            _model = model;
            _hud = hud;
            SetView(view);
        }

        public override void SetView(PowerUpCardView view)
        {
            base.SetView(view);
            _view = view;
            view.SetCallbacks(OnClickCard);
        }

        public void UpdateCard(WeaponController weapon) {
            var model = weapon.Model;
            _model.SetWeapon(weapon);
            _model.SetName(model.WeaponName);
            _model.SetDescription(model.NextLevelDescription);
            _model.SetIcon(model.Icon);
            _view.gameObject.SetActive(true);
        }

        public void OnClickCard()
        {
            Publish<PowerUpSelectedMessage>(new PowerUpSelectedMessage(_model.PowerUpController));
            Publish<ShowPowerUpOptionsMessage>(new ShowPowerUpOptionsMessage());
        }

    }
}


