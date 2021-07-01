using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    [CustomPropertyDrawer(typeof(SpriteRenderAttribute))]
    public class SpritePropertyDrawer : PropertyDrawer
    {
        private float BORDER_WIDTH = 1;

        private Texture2D _cachedOriginalTexture = null;
        private Texture2D _tex = null;

        private readonly int[] _resolutionThresholds =
        {
            50, 16, 14, 12, 10, 8, 6, 4, 2
        };

        private bool IsPropertyValid(SerializedProperty prop)
        {
            string type = prop.type;
            return type.Contains(nameof(Sprite)) ||
                   type.Contains(nameof(Texture2D)) ||
                   type.Contains(nameof(Texture));
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = base.GetPropertyHeight(property, label);
            
            if (!IsPropertyValid(property)) 
            {
                return height;
            }


            //if no object assigned, do default height
            if (property.objectReferenceValue == null)
            {
                return height;
            }

            TryCacheTex(property);

            //else add more height based on the image's height

            if (_tex == null || property.hasMultipleDifferentValues)
            {
                return height;
            }

            height += 2 + BORDER_WIDTH * 2;
            height += _tex.height;
            return height;
            
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position = new Rect(position)
            {
                height = base.GetPropertyHeight(property, label)
            };

            if (!IsPropertyValid(property))
            {
                GUIContent error = new GUIContent()
                {
                    text = label.text,
                    tooltip = "Invalid usage of [SpriteRender].\nOnly use on Sprite, Texture2D, and Texture",
                    image = EditorGUIUtil.GetUnityIcon("console.erroricon.sml", "")
                };
                
                EditorGUI.LabelField(position, error);
                return;
            }
            
            EditorGUI.PropertyField(position, property, label);
            property.serializedObject.ApplyModifiedProperties();
            
            TryCacheTex(property);
            DrawPreviewImage(position);
        }

        private void DrawPreviewImage(Rect position) //image (only appears when is assigned)
        {
            if (_tex == null)
            {
                return;
            }
                
            Rect imageRect = new Rect(position)
            {
                x = position.x + EditorGUIUtility.labelWidth + BORDER_WIDTH + 2,
                y = position.y + EditorGUIUtility.singleLineHeight + BORDER_WIDTH + 2,
                width = _tex.width,
                height = _tex.height,
            };

            Rect borderRect = new Rect
            {
                position = imageRect.position - Vector2.one * BORDER_WIDTH,
                size = imageRect.size + Vector2.one * BORDER_WIDTH * 2,
            };
                
            EditorGUI.DrawTextureAlpha(borderRect, Texture2D.whiteTexture);
            GUI.DrawTexture(imageRect, GetCheckerboardTexture((int)imageRect.width, (int)imageRect.height));
            GUI.DrawTexture(imageRect, _tex, ScaleMode.ScaleAndCrop, true);
        }

        private void TryCacheTex(SerializedProperty property)
        {
            if (_cachedOriginalTexture != null && _cachedOriginalTexture == GetAssetPreview(property))
            {
                return;
            }
            
            _cachedOriginalTexture = GetAssetPreview(property);
            int scale = IdealScale(_cachedOriginalTexture);
            _tex = ScaledSize(_cachedOriginalTexture, scale);
        }

        private Texture2D GetCheckerboardTexture(int width, int height)
        {
            Texture2D texture = new Texture2D(width, height);

            const float checkerboardAlpha = 0.2f;
            int tileSize = Mathf.Min(width, height) /8;
            
            Color white = Color.black.Alpha(checkerboardAlpha);
            Color black = Color.black.Alpha(checkerboardAlpha*2);
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //bool isWhite = (x / height % factor + y / width % factor) == factor;
                    //bool isWhite = x % factor == y % factor;
                    //bool isWhite = x+y % Mathf.Min(width, height) == 0;
                    bool isWhite = x / tileSize % 2 == y / tileSize % 2;
                    
                    Color color = isWhite ? white : black;
                    texture.SetPixel(x, y, color);
                }
            }

            texture.Apply();
            return texture;
        }
        
        private Texture2D GetAssetPreview(SerializedProperty property)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                return null;
            }
            
            if (property.objectReferenceValue == null) return null;
            
            Texture2D previewTexture = AssetPreview.GetAssetPreview(property.objectReferenceValue);

            return previewTexture;
        }

        private Texture2D ScaledSize(Texture2D tex, int scale)
        {
            if (tex == null) return null;
            
            Texture2D newTex = new Texture2D(tex.width*scale, tex.height*scale);

            for (int x = 0; x < newTex.width; x++)
            {
                for (int y = 0; y < newTex.height; y++)
                {
                    int xCoord = Mathf.FloorToInt((float)x/scale);
                    int yCoord = Mathf.FloorToInt((float)y/scale);
                    
                    Color colorToUse = tex.GetPixel(xCoord, yCoord);
                    newTex.SetPixel(x, y, colorToUse);
                }
            }
            newTex.Apply();
            return newTex;
        }

        private int IdealScale(Texture2D tex)
        {
            int scale = 1;

            if (tex == null)
            {
                return scale;
            }
            
            //compare using the larger on of the two vectors.
            foreach (int threshold in _resolutionThresholds)
            {
                int largestVector = Mathf.Max(tex.height, tex.width/5);
                
                //if either of our vectors are too weak, make it bigger
                if (largestVector <= threshold)
                {
                    scale++;
                }
            }
            return scale;
        }
    }
}