using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Api.Repository
{
    //результат выполнения операции методов Api
    public interface IApiResponse
    {
        //добавляет ошибку
        void AddError(string Text, int Code=0);
        //добавляет данные в виде объекта (объект сериализуется)
        void AddData(Object Data);
        //добавляет данные в виде простой строки
        void AddData(string Text);
        //добавляет данные в виде булева значения
        void AddData(bool Value);
        //очистить список ошибок
        void ClearErrors();
        //очистить данные
        void ClearData();
        //очистить объект от ошибок и данных
        void Clear();
        //возвращает истину, если есть ошибки в объекте
        bool HasErrors();
        //возвращает истину, если есть данны в объекте
        bool HasData();
    }
    public class ApiResponse : IApiResponse
    {
        public List<ApiError> Errors { get; set; }
        public List<string> Data { get; set; }

        public ApiResponse()
        {
            Errors = new List<ApiError>();
            Data = new List<string>();
        }
        public void AddData(object Data)
        {
            this.Data.Add(Newtonsoft.Json.JsonConvert.SerializeObject(Data));
        }

        public void AddData(string Text)
        {
            Data.Add(Text);
        }

        public void AddError(string Text, int Code = 0)
        {
            Errors.Add(new ApiError{ Text=Text, Code=Code});
        }

        public void ClearErrors()
        {
            Errors.Clear();
        }

        public void ClearData()
        {
            Data.Clear();
        }

        public void Clear()
        {
            ClearErrors();
            ClearData();
        }

        public void AddData(bool Value)
        {
            Data.Add(Convert.ToString(Value));
        }

        public bool HasErrors()
        {
            return Errors.Count > 0;
        }

        public bool HasData()
        {
            return Data.Count > 0;
        }
    }
}
