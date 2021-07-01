using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    [CustomPropertyDrawer(typeof(MinMaxRangeAttribute), true)]
    public class MinMaxRangeAttributeDrawer : PropertyDrawer 
    {
        private const float RANGE_BOUNDS_LABEL_WIDTH = 35f;
        private const float RANGE_BOUNDS_LABEL_SPACING = 4f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Vector2)
            {
                GUIContent error = new GUIContent()
                {
                    text = label.text,
                    tooltip = "Invalid usage of [MinMaxRange].\nOnly use on Vector2",
                    image = EditorGUIUtil.GetUnityIcon("console.erroricon.sml", "")
                };
                
                EditorGUI.LabelField(position, error);
                return;
            }
            
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            MinMaxRangeAttribute minMaxSliderAttribute = (MinMaxRangeAttribute)attribute;
            
            float rangeMin = minMaxSliderAttribute.Min;
            float rangeMax = minMaxSliderAttribute.Max;
            float snapThreshold = minMaxSliderAttribute.SnapThreshold;

            Debug.Assert(snapThreshold < (rangeMax - rangeMin), "MinMaxRangeAttribute snapping threshold is too large!");
            
            float newMinValue = property.vector2Value.x;
            float newMaxValue = property.vector2Value.y;
            
            
            string numberFormat = snapThreshold.StringFormatNoTrailedZeros();

            Rect rangeBoundsLabel1Rect = new Rect(position);
            rangeBoundsLabel1Rect.width = RANGE_BOUNDS_LABEL_WIDTH;
            
            GUI.Label(rangeBoundsLabel1Rect, new GUIContent(newMinValue.ToString(numberFormat)));
            position.xMin += RANGE_BOUNDS_LABEL_WIDTH + RANGE_BOUNDS_LABEL_SPACING;
            
            Rect rangeBoundsLabel2Rect = new Rect(position);
            rangeBoundsLabel2Rect.xMin = rangeBoundsLabel2Rect.xMax - RANGE_BOUNDS_LABEL_WIDTH;

            GUI.Label(rangeBoundsLabel2Rect, new GUIContent(newMaxValue.ToString(numberFormat)));
            position.xMax -= (RANGE_BOUNDS_LABEL_WIDTH + RANGE_BOUNDS_LABEL_SPACING);
            

            //draw the slider
            EditorGUI.BeginChangeCheck();
            EditorGUI.MinMaxSlider(position, ref newMinValue, ref newMaxValue, rangeMin, rangeMax);
            
            //draw the float fields
            newMinValue = EditorGUI.FloatField(rangeBoundsLabel1Rect, newMinValue);
            newMaxValue = EditorGUI.FloatField(rangeBoundsLabel2Rect, newMaxValue);

            if (EditorGUI.EndChangeCheck())
            {
                //clamp the values
                newMinValue = Mathf.Clamp(newMinValue, rangeMin, rangeMax);
                newMaxValue = Mathf.Clamp(newMaxValue, rangeMin, rangeMax);
            
                //snap the values
                newMinValue = Mathf.Round(newMinValue / snapThreshold) * snapThreshold;
                newMaxValue = Mathf.Round(newMaxValue / snapThreshold) * snapThreshold;
                
                property.vector2Value = new Vector2(newMinValue, newMaxValue);
            }

            EditorGUI.EndProperty();
        }
    }
}