using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Api.Repository
{
    public interface IApiError
    {

    }
    //описание структуры ошибки
    public class ApiError:IApiError
    {
        //текстовое содержание ошибки
        public string Text { get; set; }
        //код ошибки
        public int Code { get; set; }
    }
}
