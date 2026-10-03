using ShoulderDelivery.Adapter;
using ShoulderDelivery.Infrastructure;
using ShoulderDelivery.UseCase;
using UnityEngine;

namespace ShoulderDelivery.CompositionRoot
{
    public class GameCompositionRoot : MonoBehaviour
    {
        [Header("Master")]
        [SerializeField] StageDefinitionAsset[] _stages;

        [Header("Views")]
        [SerializeField] InGameCountDownView _inGameCountDownView;
        [SerializeField] InGameHudView _inGameHudView;
        [SerializeField] CardboardContextView _cardboardContextView;

        GamePresenter _presenter;

        #region UseCase
        GameUseCase _gameUseCase;
        DiscardCardboardUseCase _cardboardUseCase;
        ResolveDeliveryUseCase _resolveDeliveryUseCase;
        ThrowCardboardUseCase _throwCardboardUseCase;
        #endregion

        #region Repository
        StageRepository _stageRepository;
        #endregion

        GameSessionStore _gameSessionStore;
        CardboardIdGenerator _cardboardIdGenerator;

        private void Awake()
        {
            _presenter = new(null
                , _inGameCountDownView
                , _inGameHudView
                , null
                , null
                , _cardboardContextView);

            _stageRepository = new(_stages);
            _gameSessionStore = new();
            _cardboardIdGenerator = new();

            _gameUseCase = new(_stageRepository
                , _gameSessionStore
                , _presenter);

            _cardboardUseCase = new(_gameSessionStore);

            _resolveDeliveryUseCase = new(_gameSessionStore
                , _presenter
                , null
                , _gameUseCase);

            //_throwCardboardUseCase = new(_gameSessionStore
            //    ,_cardboardIdGenerator
            //    ,)
        }
    }
}
