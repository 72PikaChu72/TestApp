using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Classes
{
    public class QuestionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Question).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);

            // Определите тип вопроса на основе значения свойства "type"
            string questionType = jsonObject.GetValue("type").ToString();

            // Создайте экземпляр соответствующего наследованного класса вопроса на основе типа
            Question question;
            if (questionType == "Radial")
            {
                question = new RadialQuestion();
            }
            else if (questionType == "CheckBox")
            {
                question = new CheckBoxQuestion();
            }
            else if (questionType == "Text")
            {
                question = new TextQuestion();
            }
            else
            {
                // Если тип вопроса неизвестен, вернуть null или выбросить исключение, в зависимости от вашего случая
                return null;
            }

            // Десериализуйте свойства вопроса из JSON
            serializer.Populate(jsonObject.CreateReader(), question);

            return question;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
