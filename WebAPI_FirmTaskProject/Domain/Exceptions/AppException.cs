using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class AppException : Exception
    {
        public int StatusCode { get; }

        public AppException(string message, int statusCode = 400) : base(message) { StatusCode = statusCode; }

        public class EntityNotFoundException : AppException
        {
            public EntityNotFoundException(Guid id) : base($"Сущность c таким идентификатором {id} не найдена.", 404) { }

        }

        public class EntityWithIdAlreadyExistException : AppException
        {
            public EntityWithIdAlreadyExistException(Guid id) : base($"Сущность c таким идентификатором {id} уже существует.", 400) { }
        }
    }
}
