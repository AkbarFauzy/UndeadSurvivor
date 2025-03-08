using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agate.MVC.Base;

namespace Roguelike.Module.GameplayAudio {
    public class GameplayAudioView : ObjectView<IGameplayAudioModel>
    {
        [SerializeField]
        private AudioSource _bgmSfx;
        [SerializeField]
        private AudioSource _levelUpSfx;
        [SerializeField]
        private AudioSource _gameOverSfx;

        public void PlayBGMSfx()
        {
            _bgmSfx.Play();
        }

        public void PlayLevelUpSfx() {
            _levelUpSfx.Play();
        }

        public void PlayGameOverSfx() {
            _gameOverSfx.Play();
        }

        protected override void InitRenderModel(IGameplayAudioModel model)
        {
          
        }

        protected override void UpdateRenderModel(IGameplayAudioModel model)
        {

        }
    }
}


