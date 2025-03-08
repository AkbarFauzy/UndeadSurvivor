using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Agate.MVC.Base;
using Roguelike.Message;
using Roguelike.Utility;
using Roguelike.Boot;
using Roguelike.Module.Weapon;

namespace Roguelike.Module.HUD 
{
    public class HUDController : ObjectController<HUDController, HUDModel, IHUDModel, HUDView>
    {
        public override void SetView(HUDView view)
        {
            base.SetView(view);
            view.SetCallbacks(PlayTimer, OnGamePause, OnGoToMainMenu);
            Time.timeScale = 1;
            SetPowerUpCard();
        }

        public void SetPowerUpCard()
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject card = GameObject.Instantiate(_view.powerUpCardPrefab, _view.powerUpPanel.transform);
                PowerUpCardView cardView = card.GetComponent<PowerUpCardView>();
                PowerUpCardModel cardModel = new PowerUpCardModel();
                PowerUpCardController cardController = new PowerUpCardController();

                InjectDependencies(cardController);

                cardController.Init(cardModel, cardView, this);
                _model.PowerUpCards.Add(cardController);
            }
        }

        public void OnPlayerLevelUp(PlayerLevelUpMessage message) {
            _model.SetLevel(message.Level);
            if (message.Level < 15)
            {
                Publish<ShowPowerUpOptionsMessage>(new ShowPowerUpOptionsMessage());
            }
            else {
                Publish<PlayerRestoreHealthMessage>(new PlayerRestoreHealthMessage());
            }
        }

        public void OnShowPowerUpOptions(ShowPowerUpOptionsMessage message) {
            if (_view.powerUpPanel.activeInHierarchy) {
                _view.powerUpPanel.SetActive(false);
                Time.timeScale = 1;
            }
            else {
                _view.powerUpPanel.SetActive(true);
                Time.timeScale = 0; 
            }
        }

        public void OnPowerUpSelection(PowerUpSelectionMessage message) {
            UpdatePowerUpCard(message.PowerUpSelection);
        }

        private void UpdatePowerUpCard(WeaponController[] model)
        {
            for (int i = 0; i < 3; i++)
            {
                _model.PowerUpCards[i].UpdateCard(model[i]);
            }
        }

        public void OnPlayerGainingExp(PlayerGainingExperience message) {
            _view._expBar.fillAmount = (float)message.Exp / (float)message.MaxExp;
        }

        private void PlayTimer() {
            _model.AddTime(Time.deltaTime);
        }

        public void OnGamePause() {
            Publish<ShowGamePauseMessage>(new ShowGamePauseMessage());
        }

        public void OnGameOver(GameOverMessage message)
        {
            Time.timeScale = 0;
            _view._gameOverPanel.SetActive(true);
        }

        public void OnGoToMainMenu() {
            SceneLoader.Instance.LoadScene(GameScene.MainMenu);
        }

    }
}

