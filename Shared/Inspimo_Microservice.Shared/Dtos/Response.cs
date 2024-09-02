using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspimo_Microservice.Shared.Dtos
{
    public class Response<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        //Başarılı olursa bool değeri dönecek.
        public bool IsScuccessful { get; set; }
        //Başarısız olursa Errors prop değeri dönecek.
        public List<string> Errors { get; set; }
        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsScuccessful = true };
        }
        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { StatusCode = statusCode, IsScuccessful = true, Data = default(T) };
        }
        public static Response<T> Fail(string error, int statusCode)
        {
            return new Response<T> { Errors = new List<string>() { error }, StatusCode = statusCode, IsScuccessful = false };
        }
        public static Response<T> Fail(List<string> errors, int statusCode)
        {
            return new Response<T> { Errors = errors, StatusCode = statusCode, IsScuccessful = false };
        }

    }
}
