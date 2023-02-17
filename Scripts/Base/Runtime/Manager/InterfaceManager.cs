using System;
using Base.Runtime.EventServices;
using Base.Runtime.EventServices.Resources.Game;
using Base.Runtime.Misc;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using TMPro;
using UnityEngine;

namespace Base.Runtime.Manager
 {
     public class InterfaceManager : Singleton<InterfaceManager>
     {
         [Header("Panels")] 
         [SerializeField] private UIUpgradePanel upgradePanel;
         [SerializeField] private UIWinPanel winPanel;
         
         [Header("Transforms")]
         [SerializeField] private RectTransform canvas;
         [SerializeField] private RectTransform gameScreen;
         [SerializeField] private RectTransform chargeSlot;
         [SerializeField] private RectTransform moneySlot;
         
         [Header("Texts")] 
         [SerializeField] private TMP_Text moneyText;

         [Header("Prefabs")] 
         [SerializeField] private RectTransform chargePrefab;
         [SerializeField] private RectTransform moneyPrefab;
         [SerializeField] private TMP_Text moneyTextPrefab;


         protected override void Awake()
         {
             base.Awake();
             EventService.AddListener<MoneyUpdatedEvent>(OnMoneyUpdated);
         }

         public void EnableWinPanel() => winPanel.EnablePanel();

         public void EnableUpgradePanel() => upgradePanel.EnablePanel();
         
         public void DisableUpgradePanel() => upgradePanel.DisablePanel();
         
         public void FlyChargeFromWorld(Vector3 worldPosition)
         {
             Camera targetCamera = CameraManager.Instance.GetCamera();
             Vector3 screenPosition = GameUtils.WorldToCanvasPosition(canvas, targetCamera, worldPosition);
             Vector3 targetScreenPosition = canvas.InverseTransformPoint(chargeSlot.position);
                
             RectTransform createdCurrency = Instantiate(chargePrefab, canvas);
             createdCurrency.anchoredPosition = screenPosition;
             createdCurrency.localScale = Vector3.zero;

             Sequence sequence = DOTween.Sequence();

             sequence.Join(createdCurrency.DOScale(Vector3.one * 2.5F, 0.25F).SetEase(Ease.OutBack));
             sequence.Join(createdCurrency.transform.DOLocalMove(targetScreenPosition, 0.25F).SetEase(Ease.Linear).SetDelay(0.25F));
             sequence.Join(createdCurrency.transform.DOScale(Vector3.one, 0.25F).SetEase(Ease.Linear).SetDelay(0.125F));

             sequence.OnComplete(() =>
             {
                 Destroy(createdCurrency.gameObject);
             });

             sequence.Play();
            
             SoundManager.Instance.Play("CreditIncrease");
             VibrationManager.Instance.Haptic(HapticTypes.LightImpact);
         }

         public void FlyCurrencyFromWorld(Vector3 worldPosition)
         {
             Camera targetCamera = CameraManager.Instance.GetCamera();
             Vector3 screenPosition = GameUtils.WorldToCanvasPosition(canvas, targetCamera, worldPosition);
             Vector3 targetScreenPosition = canvas.InverseTransformPoint(moneySlot.position);
                
             RectTransform createdCurrency = Instantiate(moneyPrefab, canvas);
             createdCurrency.anchoredPosition = screenPosition;
            
             Sequence sequence = DOTween.Sequence();

             sequence.Join(createdCurrency.transform.DOLocalMove(targetScreenPosition, 0.5F));

             sequence.OnComplete(() =>
             {
                 Destroy(createdCurrency.gameObject);
             });

             sequence.Play();
            
             SoundManager.Instance.Play(CommonTypes.SFX_MONEY_FLY);
             VibrationManager.Instance.Haptic(HapticTypes.LightImpact);
         }

         public void FlyCurrencyFromScreen(Vector3 screenPosition)
         {
             Vector3 targetScreenPosition = canvas.InverseTransformPoint(moneySlot.position);
                
             RectTransform createdCurrency = Instantiate(moneyPrefab, canvas);
             createdCurrency.position = screenPosition;
            
             Sequence sequence = DOTween.Sequence();

             sequence.Join(createdCurrency.transform.DOLocalMove(targetScreenPosition, 0.5F));

             sequence.OnComplete(() =>
             {
                 Destroy(createdCurrency.gameObject);
             });

             sequence.Play();
            
             SoundManager.Instance.Play(CommonTypes.SFX_MONEY_FLY);
             VibrationManager.Instance.Haptic(HapticTypes.LightImpact);
         }
         
         // public void FlyCurrencyTextFromScreen(Vector3 screenPosition, int amount)
         // {
         //     Vector3 targetScreenPosition = canvas.InverseTransformPoint(creditSlot.position);
         //        
         //     TMP_Text createdCurrency = Instantiate(creditTextPrefab, canvas);
         //     createdCurrency.text = "+" + amount;
         //     createdCurrency.rectTransform.anchoredPosition = screenPosition + new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0);
         //    
         //     Sequence sequence = DOTween.Sequence();
         //
         //     sequence.Append(createdCurrency.rectTransform.DOScale(0f, .7f));
         //     sequence.Join(createdCurrency.transform.DOLocalMove(screenPosition + Vector3.up * 175f, .7F));
         //
         //     sequence.OnComplete(() =>
         //     {
         //         Destroy(createdCurrency.gameObject);
         //     });
         //
         //     sequence.Play();
         //    
         //     SoundManager.Instance.Play(CommonTypes.SFX_CURRENCY_FLY);
         //     VibrationManager.Instance.Haptic(HapticTypes.Success);
         // }
         
         public void FlyCurrencyTextFromWorld(Vector3 worldPosition, int amount)
         {
             Camera targetCamera = CameraManager.Instance.GetCamera();
             Vector3 screenPosition = GameUtils.WorldToCanvasPosition(canvas, targetCamera, worldPosition);
             //Vector3 targetScreenPosition = m_canvas.InverseTransformPoint(m_currencySlot.position);
                
             TMP_Text createdCurrency = Instantiate(moneyTextPrefab, gameScreen);
             createdCurrency.text = "+" + amount+"$";
             createdCurrency.rectTransform.anchoredPosition = screenPosition;
             createdCurrency.transform.localScale *= .8f;
            
            
             Sequence sequence = DOTween.Sequence();

             sequence.Append(createdCurrency.rectTransform.DOScale(.7f, .7f));
             sequence.Join(createdCurrency.transform.DOLocalMove(screenPosition + Vector3.up * 250f, .7F));
            
             sequence.OnComplete(() =>
             {
                 Destroy(createdCurrency.gameObject);
             });

             sequence.Play();

             MoneyManager.Instance.IncreaseMoney(amount);
           
             VibrationManager.Instance.Haptic(HapticTypes.LightImpact);
         }
         
         private void OnMoneyUpdated(MoneyUpdatedEvent e)
         {
             string editedText = moneyText.text;

             editedText = editedText.Replace(".", "");
             editedText = editedText.Replace(",", "");
            
             int cachedCurrency = int.Parse(editedText);

             float delay = e.UIDelay;
             float duration = e.UIDuration == -1 ? CommonTypes.UI_DEFAULT_FLY_MONEY_DURATION : e.UIDuration;
            
             Sequence sequence = DOTween.Sequence();

             sequence.Join(DOTween.To(() => cachedCurrency, x => cachedCurrency = x, e.Credit, duration).SetDelay(delay));

             sequence.OnUpdate(() =>
             {
                 moneyText.text = $"{cachedCurrency.ToString("N0").Replace(",",String.Empty)}";
             });

             sequence.SetId(moneyText.GetInstanceID());
             sequence.Play();
         }
         
         protected override void OnDestroy()
         {
             EventService.RemoveListener<MoneyUpdatedEvent>(OnMoneyUpdated);
             base.OnDestroy();
         }
     }  
 }

