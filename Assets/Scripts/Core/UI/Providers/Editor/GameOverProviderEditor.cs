using Core.GameOver;
using UnityEngine;
using UnityEditor;
using Core.UI.Providers;
using Core.UI.Common;
using TMPro;

namespace Core.UI.Providers.Editor
{
    /// <summary>
    /// Editor-скрипт для создания красивого UI окна окончания игры
    /// </summary>
    public class GameOverProviderEditor : EditorWindow
    {
        [MenuItem("Tools/UI/Create Game Over Provider")]
        public static void CreateGameOverProvider()
        {
            // Находим Canvas
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("Canvas не найден на сцене!");
                return;
            }

            // Создаем основной объект - панель
            GameObject panelObject = new GameObject("GameOverPanel");
            panelObject.transform.SetParent(canvas.transform, false);
            
            // Добавляем компонент GameOverProvider прямо на панель
            GameOverProvider gameOverProvider = panelObject.AddComponent<GameOverProvider>();
            
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
            backgroundImage.color = new Color(0.15f, 0.15f, 0.2f, 0.95f);
            
            // Создаем контейнер для контента с красивым дизайном
            GameObject contentObject = new GameObject("Content");
            contentObject.transform.SetParent(panelObject.transform, false);
            
            RectTransform contentRect = contentObject.AddComponent<RectTransform>();
            contentRect.anchorMin = new Vector2(0.5f, 0.5f);
            contentRect.anchorMax = new Vector2(0.5f, 0.5f);
            contentRect.sizeDelta = new Vector2(500, 400);
            contentRect.anchoredPosition = Vector2.zero;
            
            // Добавляем Image для контента, чтобы он выглядел как окно
            var contentImage = contentObject.AddComponent<UnityEngine.UI.Image>();
            contentImage.color = new Color(0.2f, 0.2f, 0.28f, 0.95f);
            
            // Создаем заголовок с красивым стилем
            GameObject titleObject = new GameObject("Title");
            titleObject.transform.SetParent(contentObject.transform, false);
            
            RectTransform titleRect = titleObject.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0, 1);
            titleRect.anchorMax = new Vector2(1, 1);
            titleRect.offsetMin = new Vector2(30, -100);
            titleRect.offsetMax = new Vector2(-30, -30);
            
            TextMeshProUGUI titleText = titleObject.AddComponent<TextMeshProUGUI>();
            titleText.text = "GAME OVER";
            titleText.fontSize = 32;
            titleText.fontStyle = FontStyles.Bold;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.color = new Color(1f, 0.3f, 0.3f, 1f); // Красный цвет для Game Over
            titleText.enableAutoSizing = true;
            titleText.fontSizeMin = 24;
            titleText.fontSizeMax = 40;
            
            // Создаем подзаголовок с типом окончания игры
            GameObject subtitleObject = new GameObject("Subtitle");
            subtitleObject.transform.SetParent(contentObject.transform, false);
            
            RectTransform subtitleRect = subtitleObject.AddComponent<RectTransform>();
            subtitleRect.anchorMin = new Vector2(0, 1);
            subtitleRect.anchorMax = new Vector2(1, 1);
            subtitleRect.offsetMin = new Vector2(30, -140);
            subtitleRect.offsetMax = new Vector2(-30, -100);
            
            TextMeshProUGUI subtitleText = subtitleObject.AddComponent<TextMeshProUGUI>();
            subtitleText.text = "Bankruptcy";
            subtitleText.fontSize = 18;
            subtitleText.fontStyle = FontStyles.Normal;
            subtitleText.alignment = TextAlignmentOptions.Center;
            subtitleText.color = new Color(0.8f, 0.8f, 0.8f, 1f);
            
            // Создаем описание с красивым оформлением
            GameObject descriptionObject = new GameObject("Description");
            descriptionObject.transform.SetParent(contentObject.transform, false);
            
            RectTransform descriptionRect = descriptionObject.AddComponent<RectTransform>();
            descriptionRect.anchorMin = new Vector2(0, 0.5f);
            descriptionRect.anchorMax = new Vector2(1, 0.8f);
            descriptionRect.offsetMin = new Vector2(40, 20);
            descriptionRect.offsetMax = new Vector2(-40, -20);
            
            TextMeshProUGUI descriptionText = descriptionObject.AddComponent<TextMeshProUGUI>();
            descriptionText.text = "Your balance has reached zero.\nTry again and improve your strategy!";
            descriptionText.fontSize = 16;
            descriptionText.alignment = TextAlignmentOptions.Center;
            descriptionText.color = new Color(0.9f, 0.9f, 0.9f, 1f);
            descriptionText.enableWordWrapping = true;
            
            // Создаем красивую кнопку "Начать заново"
            GameObject restartButtonObject = new GameObject("RestartButton");
            restartButtonObject.transform.SetParent(contentObject.transform, false);
            
            RectTransform restartButtonRect = restartButtonObject.AddComponent<RectTransform>();
            restartButtonRect.anchorMin = new Vector2(0, 0.15f);
            restartButtonRect.anchorMax = new Vector2(0.45f, 0.35f);
            restartButtonRect.offsetMin = new Vector2(30, 20);
            restartButtonRect.offsetMax = new Vector2(-15, -20);
            
            var restartButtonImage = restartButtonObject.AddComponent<UnityEngine.UI.Image>();
            restartButtonImage.color = new Color(0.2f, 0.8f, 0.3f, 1f);
            
            // Добавляем Button компонент
            var restartUnityButton = restartButtonObject.AddComponent<UnityEngine.UI.Button>();
            
            // Настраиваем цвета кнопки
            var restartButtonColors = restartUnityButton.colors;
            restartButtonColors.normalColor = new Color(0.2f, 0.8f, 0.3f, 1f);
            restartButtonColors.highlightedColor = new Color(0.3f, 0.9f, 0.4f, 1f);
            restartButtonColors.pressedColor = new Color(0.1f, 0.7f, 0.2f, 1f);
            restartButtonColors.selectedColor = new Color(0.2f, 0.8f, 0.3f, 1f);
            restartUnityButton.colors = restartButtonColors;
            
            ButtonProvider restartButton = restartButtonObject.AddComponent<ButtonProvider>();
            
            // Создаем текст кнопки
            GameObject restartTextObject = new GameObject("Text");
            restartTextObject.transform.SetParent(restartButtonObject.transform, false);
            
            RectTransform restartTextRect = restartTextObject.AddComponent<RectTransform>();
            restartTextRect.anchorMin = Vector2.zero;
            restartTextRect.anchorMax = Vector2.one;
            restartTextRect.offsetMin = Vector2.zero;
            restartTextRect.offsetMax = Vector2.zero;
            
            TextMeshProUGUI restartText = restartTextObject.AddComponent<TextMeshProUGUI>();
            restartText.text = "RESTART";
            restartText.fontSize = 16;
            restartText.fontStyle = FontStyles.Bold;
            restartText.alignment = TextAlignmentOptions.Center;
            restartText.color = Color.white;
            
            // Создаем красивую кнопку "Выход"
            GameObject exitButtonObject = new GameObject("ExitButton");
            exitButtonObject.transform.SetParent(contentObject.transform, false);
            
            RectTransform exitButtonRect = exitButtonObject.AddComponent<RectTransform>();
            exitButtonRect.anchorMin = new Vector2(0.55f, 0.15f);
            exitButtonRect.anchorMax = new Vector2(1, 0.35f);
            exitButtonRect.offsetMin = new Vector2(15, 20);
            exitButtonRect.offsetMax = new Vector2(-30, -20);
            
            var exitButtonImage = exitButtonObject.AddComponent<UnityEngine.UI.Image>();
            exitButtonImage.color = new Color(0.8f, 0.2f, 0.2f, 1f);
            
            // Добавляем Button компонент
            var exitUnityButton = exitButtonObject.AddComponent<UnityEngine.UI.Button>();
            
            // Настраиваем цвета кнопки
            var exitButtonColors = exitUnityButton.colors;
            exitButtonColors.normalColor = new Color(0.8f, 0.2f, 0.2f, 1f);
            exitButtonColors.highlightedColor = new Color(0.9f, 0.3f, 0.3f, 1f);
            exitButtonColors.pressedColor = new Color(0.7f, 0.1f, 0.1f, 1f);
            exitButtonColors.selectedColor = new Color(0.8f, 0.2f, 0.2f, 1f);
            exitUnityButton.colors = exitButtonColors;
            
            ButtonProvider exitButton = exitButtonObject.AddComponent<ButtonProvider>();
            
            // Создаем текст кнопки
            GameObject exitTextObject = new GameObject("Text");
            exitTextObject.transform.SetParent(exitButtonObject.transform, false);
            
            RectTransform exitTextRect = exitTextObject.AddComponent<RectTransform>();
            exitTextRect.anchorMin = Vector2.zero;
            exitTextRect.anchorMax = Vector2.one;
            exitTextRect.offsetMin = Vector2.zero;
            exitTextRect.offsetMax = Vector2.zero;
            
            TextMeshProUGUI exitText = exitTextObject.AddComponent<TextMeshProUGUI>();
            exitText.text = "EXIT";
            exitText.fontSize = 16;
            exitText.fontStyle = FontStyles.Bold;
            exitText.alignment = TextAlignmentOptions.Center;
            exitText.color = Color.white;
            
            // Создаем декоративный элемент - иконку
            GameObject iconObject = new GameObject("Icon");
            iconObject.transform.SetParent(contentObject.transform, false);
            
            RectTransform iconRect = iconObject.AddComponent<RectTransform>();
            iconRect.anchorMin = new Vector2(0.5f, 0.8f);
            iconRect.anchorMax = new Vector2(0.5f, 0.95f);
            iconRect.sizeDelta = new Vector2(60, 60);
            iconRect.anchoredPosition = Vector2.zero;
            
            var iconImage = iconObject.AddComponent<UnityEngine.UI.Image>();
            iconImage.color = new Color(1f, 0.3f, 0.3f, 0.8f);
            
            // Настраиваем ссылки в ButtonProvider
            SerializedObject restartButtonSerialized = new SerializedObject(restartButton);
            restartButtonSerialized.FindProperty("button").objectReferenceValue = restartUnityButton;
            restartButtonSerialized.ApplyModifiedProperties();
            
            SerializedObject exitButtonSerialized = new SerializedObject(exitButton);
            exitButtonSerialized.FindProperty("button").objectReferenceValue = exitUnityButton;
            exitButtonSerialized.ApplyModifiedProperties();
            
            // Настраиваем ссылки в GameOverProvider
            SerializedObject serializedObject = new SerializedObject(gameOverProvider);
            serializedObject.FindProperty("gameOverPanel").objectReferenceValue = panelObject;
            serializedObject.FindProperty("titleText").objectReferenceValue = titleText;
            serializedObject.FindProperty("descriptionText").objectReferenceValue = descriptionText;
            serializedObject.FindProperty("restartButton").objectReferenceValue = restartButton;
            serializedObject.FindProperty("closeButton").objectReferenceValue = exitButton;
            serializedObject.ApplyModifiedProperties();
            
            // Скрываем панель по умолчанию
            panelObject.SetActive(false);
            
            // Выбираем созданный объект
            Selection.activeGameObject = panelObject;
            
            Debug.Log("🎮 GameOverPanel успешно создан с красивым UI!");
        }
    }
} 