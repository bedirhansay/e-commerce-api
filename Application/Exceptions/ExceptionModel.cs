using Newtonsoft.Json;
using System.Collections.Generic;

namespace Application.Exceptions
{
    public class ExceptionModel:ErrorStatusCode
    {
        /// <summary>
        /// Hata mesajlarını saklayan bir liste.
        /// </summary>
        public IEnumerable<string> Errors { get; set; }

        /// <summary>
        /// ExceptionModel nesnesini JSON formatında döndürür.
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ErrorStatusCode
    {
        public int StatusCode { get; set; }
    }
}