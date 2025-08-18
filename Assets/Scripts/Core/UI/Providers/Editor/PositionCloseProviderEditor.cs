using Core.TradePosition.Close;
using UnityEngine;
using UnityEditor;
using Core.UI.Providers;
using Core.UI.Common;
using TMPro;

namespace Core.UI.Providers.Editor
{
    /// <summary>
    /// Editor-скрипт для создания красивого UI окна закрытия позиции
    /// </summary>
    public class PositionCloseProviderEditor : EditorWindow
    {
        [MenuItem("Tools/UI/Create Position Close Provider")]
        public static void CreatePositionCloseProvider()
        {
            // Находим Canvas
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("Canvas не найден на сцене!");
                return;
            }

            // Создаем основной объект - панель
            GameObject panelObject = new GameObject("PositionClosePanel");
            panelObject.transform.SetParent(canvas.transform, false);
            
            // Добавляем компонент PositionCloseProvider прямо на панель
            PositionCloseProvider positionCloseProvider = panelObject.AddComponent<PositionCloseProvider>();
            
            // Добавляем RectTransform и настраиваем его
            RectTransform panelRect = panelObject.AddComponent<RectTransform>();
            panelRect.anchorMin = Vector2.zero;
            panelRect.anchorMax = Vector2.one;
            panelRect.offsetMin = Vector2.zero;
            panelRect.offsetMax = Vector2.zero;
            
            // Создаем background объект под контентом
            GameObject backgroundObject = new GameObject("Background");
            backgroundObject.transform.SetParent(panelObject.transform, false);
            
            RectTransform backgroundRect = backgroundObject.AddComponent<RectTransform>();
            backgroundRect.anchorMin = Vector2.zero;
            backgroundRect.anchorMax = Vector2.one;
            backgroundRect.offsetMin = Vector2.zero;
            backgroundRect.offsetMax = Vector2.zero;
            
            var backgroundImage = backgroundObject.AddComponent<UnityEngine.UI.Image>();
            backgroundImage.color = new Color(0.12f, 0.12f, 0.18f, 0.98f);
            
            // Создаем контейнер для контента с красивым дизайном
            GameObject contentObject = new GameObject("Content");
            contentObject.transform.SetParent(panelObject.transform, false);
            
            RectTransform contentRect = contentObject.AddComponent<RectTransform>();
            contentRect.anchorMin = new Vector2(0.5f, 0.5f);
            contentRect.anchorMax = new Vector2(0.5f, 0.5f);
            contentRect.sizeDelta = new Vector2(450, 320);
            contentRect.anchoredPosition = Vector2.zero;
            
            // Добавляем Image для контента, чтобы он выглядел как окно
            var contentImage = contentObject.AddComponent<UnityEngine.UI.Image>();
            contentImage.color = new Color(0.18f, 0.18f, 0.25f, 0.95f);
            
            // Создаем заголовок с красивым стилем
            GameObject titleObject = new GameObject("Title");
            titleObject.transform.SetParent(contentObject.transform, false);
            
            RectTransform titleRect = titleObject.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0, 1);
            titleRect.anchorMax = new Vector2(1, 1);
            titleRect.offsetMin = new Vector2(25, -70);
            titleRect.offsetMax = new Vector2(-25, -25);
            
            TextMeshProUGUI titleText = titleObject.AddComponent<TextMeshProUGUI>();
            titleText.text = "POSITION CLOSED";
            titleText.fontSize = 24;
            titleText.fontStyle = FontStyles.Bold;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.color = new Color(0.9f, 0.9f, 0.9f, 1f);
            titleText.enableAutoSizing = true;
            titleText.fontSizeMin = 20;
            titleText.fontSizeMax = 28;
            
            // Создаем подзаголовок с типом закрытия
            GameObject subtitleObject = new GameObject("Subtitle");
            subtitleObject.transform.SetParent(contentObject.transform, false);
            
            RectTransform subtitleRect = subtitleObject.AddComponent<RectTransform>();
            subtitleRect.anchorMin = new Vector2(0, 1);
            subtitleRect.anchorMax = new Vector2(1, 1);
            subtitleRect.offsetMin = new Vector2(25, -100);
            subtitleRect.offsetMax = new Vector2(-25, -70);
            
            TextMeshProUGUI subtitleText = subtitleObject.AddComponent<TextMeshProUGUI>();
            subtitleText.text = "Manual Close";
            subtitleText.fontSize = 16;
            subtitleText.fontStyle = FontStyles.Normal;
            subtitleText.alignment = TextAlignmentOptions.Center;
            subtitleText.color = new Color(0.7f, 0.7f, 0.8f, 1f);
            
            // Создаем описание с красивым оформлением
            GameObject descriptionObject = new GameObject("Description");
            descriptionObject.transform.SetParent(contentObject.transform, false);
            
            RectTransform descriptionRect = descriptionObject.AddComponent<RectTransform>();
            descriptionRect.anchorMin = new Vector2(0, 0.65f);
            descriptionRect.anchorMax = new Vector2(1, 0.9f);
            descriptionRect.offsetMin = new Vector2(35, 15);
            descriptionRect.offsetMax = new Vector2(-35, -15);
            
            TextMeshProUGUI descriptionText = descriptionObject.AddComponent<TextMeshProUGUI>();
            descriptionText.text = "Your trading position has been successfully closed.\nCheck the result below.";
            descriptionText.fontSize = 14;
            descriptionText.alignment = TextAlignmentOptions.Center;
            descriptionText.color = new Color(0.8f, 0.8f, 0.9f, 1f);
            descriptionText.enableWordWrapping = true;
            
            // Создаем красивый PnL блок
            GameObject pnlBlockObject = new GameObject("PnLBlock");
            pnlBlockObject.transform.SetParent(contentObject.transform, false);
            
            RectTransform pnlBlockRect = pnlBlockObject.AddComponent<RectTransform>();
            pnlBlockRect.anchorMin = new Vector2(0.1f, 0.35f);
            pnlBlockRect.anchorMax = new Vector2(0.9f, 0.6f);
            pnlBlockRect.offsetMin = Vector2.zero;
            pnlBlockRect.offsetMax = Vector2.zero;
            
            // Добавляем фон для PnL блока
            var pnlBlockImage = pnlBlockObject.AddComponent<UnityEngine.UI.Image>();
            pnlBlockImage.color = new Color(0.2f, 0.2f, 0.3f, 0.8f);
            
            // Создаем заголовок PnL
            GameObject pnlTitleObject = new GameObject("PnLTitle");
            pnlTitleObject.transform.SetParent(pnlBlockObject.transform, false);
            
            RectTransform pnlTitleRect = pnlTitleObject.AddComponent<RectTransform>();
            pnlTitleRect.anchorMin = new Vector2(0, 0.7f);
            pnlTitleRect.anchorMax = new Vector2(1, 1);
            pnlTitleRect.offsetMin = new Vector2(15, 5);
            pnlTitleRect.offsetMax = new Vector2(-15, -5);
            
            TextMeshProUGUI pnlTitleText = pnlTitleObject.AddComponent<TextMeshProUGUI>();
            pnlTitleText.text = "Profit/Loss (PnL)";
            pnlTitleText.fontSize = 14;
            pnlTitleText.fontStyle = FontStyles.Bold;
            pnlTitleText.alignment = TextAlignmentOptions.Center;
            pnlTitleText.color = new Color(0.9f, 0.9f, 0.9f, 1f);
            
            // Создаем PnL значение
            GameObject pnlValueObject = new GameObject("PnLValue");
            pnlValueObject.transform.SetParent(pnlBlockObject.transform, false);
            
            RectTransform pnlValueRect = pnlValueObject.AddComponent<RectTransform>();
            pnlValueRect.anchorMin = new Vector2(0, 0.1f);
            pnlValueRect.anchorMax = new Vector2(1, 0.7f);
            pnlValueRect.offsetMin = new Vector2(15, 5);
            pnlValueRect.offsetMax = new Vector2(-15, -5);
            
            TextMeshProUGUI pnlValueText = pnlValueObject.AddComponent<TextMeshProUGUI>();
            pnlValueText.text = "+$1,250";
            pnlValueText.fontSize = 28;
            pnlValueText.fontStyle = FontStyles.Bold;
            pnlValueText.alignment = TextAlignmentOptions.Center;
            pnlValueText.color = new Color(0.2f, 0.9f, 0.3f, 1f); // Зеленый для прибыли
            
            // Создаем красивую кнопку "Закрыть"
            GameObject closeButtonObject = new GameObject("CloseButton");
            closeButtonObject.transform.SetParent(contentObject.transform, false);
            
            RectTransform closeButtonRect = closeButtonObject.AddComponent<RectTransform>();
            closeButtonRect.anchorMin = new Vector2(0.25f, 0.1f);
            closeButtonRect.anchorMax = new Vector2(0.75f, 0.25f);
            closeButtonRect.offsetMin = Vector2.zero;
            closeButtonRect.offsetMax = Vector2.zero;
            
            var closeButtonImage = closeButtonObject.AddComponent<UnityEngine.UI.Image>();
            closeButtonImage.color = new Color(0.3f, 0.4f, 0.8f, 1f);
            
            // Добавляем Button компонент
            var closeUnityButton = closeButtonObject.AddComponent<UnityEngine.UI.Button>();
            
            // Настраиваем цвета кнопки
            var closeButtonColors = closeUnityButton.colors;
            closeButtonColors.normalColor = new Color(0.3f, 0.4f, 0.8f, 1f);
            closeButtonColors.highlightedColor = new Color(0.4f, 0.5f, 0.9f, 1f);
            closeButtonColors.pressedColor = new Color(0.2f, 0.3f, 0.7f, 1f);
            closeButtonColors.selectedColor = new Color(0.3f, 0.4f, 0.8f, 1f);
            closeUnityButton.colors = closeButtonColors;
            
            ButtonProvider closeButton = closeButtonObject.AddComponent<ButtonProvider>();
            
            // Создаем текст кнопки
            GameObject closeTextObject = new GameObject("Text");
            closeTextObject.transform.SetParent(closeButtonObject.transform, false);
            
            RectTransform closeTextRect = closeTextObject.AddComponent<RectTransform>();
            closeTextRect.anchorMin = Vector2.zero;
            closeTextRect.anchorMax = Vector2.one;
            closeTextRect.offsetMin = Vector2.zero;
            closeTextRect.offsetMax = Vector2.zero;
            
            TextMeshProUGUI closeText = closeTextObject.AddComponent<TextMeshProUGUI>();
            closeText.text = "CLOSE";
            closeText.fontSize = 16;
            closeText.fontStyle = FontStyles.Bold;
            closeText.alignment = TextAlignmentOptions.Center;
            closeText.color = Color.white;
            
            // Создаем декоративные элементы - статус иконки
            GameObject statusIconObject = new GameObject("StatusIcon");
            statusIconObject.transform.SetParent(contentObject.transform, false);
            
            RectTransform statusIconRect = statusIconObject.AddComponent<RectTransform>();
            statusIconRect.anchorMin = new Vector2(0.5f, 0.85f);
            statusIconRect.anchorMax = new Vector2(0.5f, 0.95f);
            statusIconRect.sizeDelta = new Vector2(40, 40);
            statusIconRect.anchoredPosition = Vector2.zero;
            
            var statusIconImage = statusIconObject.AddComponent<UnityEngine.UI.Image>();
            statusIconImage.color = new Color(0.2f, 0.9f, 0.3f, 0.9f); // Зеленый для успеха
            
            // Создаем дополнительную информацию
            GameObject infoObject = new GameObject("AdditionalInfo");
            infoObject.transform.SetParent(contentObject.transform, false);
            
            RectTransform infoRect = infoObject.AddComponent<RectTransform>();
            infoRect.anchorMin = new Vector2(0, 0.6f);
            infoRect.anchorMax = new Vector2(1, 0.65f);
            infoRect.offsetMin = new Vector2(25, 0);
            infoRect.offsetMax = new Vector2(-25, 0);
            
            TextMeshProUGUI infoText = infoObject.AddComponent<TextMeshProUGUI>();
            infoText.text = "ROI: +12.5% | Time in position: 2h 15m";
            infoText.fontSize = 12;
            infoText.alignment = TextAlignmentOptions.Center;
            infoText.color = new Color(0.6f, 0.6f, 0.7f, 1f);
            
            // Настраиваем ссылки в ButtonProvider
            SerializedObject closeButtonSerialized = new SerializedObject(closeButton);
            closeButtonSerialized.FindProperty("button").objectReferenceValue = closeUnityButton;
            closeButtonSerialized.ApplyModifiedProperties();
            
            // Настраиваем ссылки в PositionCloseProvider
            SerializedObject serializedObject = new SerializedObject(positionCloseProvider);
            serializedObject.FindProperty("positionClosePanel").objectReferenceValue = panelObject;
            serializedObject.FindProperty("titleText").objectReferenceValue = titleText;
            serializedObject.FindProperty("descriptionText").objectReferenceValue = descriptionText;
            serializedObject.FindProperty("pnlText").objectReferenceValue = pnlValueText;
            serializedObject.FindProperty("closeButton").objectReferenceValue = closeButton;
            serializedObject.ApplyModifiedProperties();
            
            // Скрываем панель по умолчанию
            panelObject.SetActive(false);
            
            // Выбираем созданный объект
            Selection.activeGameObject = panelObject;
            
            Debug.Log("💰 PositionClosePanel успешно создан с красивым UI!");
        }
    }
} 