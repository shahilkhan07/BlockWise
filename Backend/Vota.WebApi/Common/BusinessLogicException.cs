using System;
using System.Collections.Generic;
using System.Net;

namespace Vota.WebApi.Common
{
    /// <summary>
    /// Business logic exception
    /// </summary>
    public class BusinessLogicException : Exception
    {
        /// <summary>
        /// HTTP Status code
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Business logic exception
        /// </summary>
        /// <param name="message">Meesage</param>
        /// <param name="statusCode">HTTP Status code</param>
        public BusinessLogicException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }

    /// <summary>
    /// Resource not found exception
    /// </summary>
    public class ResourceNotFoundException : BusinessLogicException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message</param>
        public ResourceNotFoundException(string message)
            : base(message, HttpStatusCode.NotFound)
        {
        }
    }

    /// <summary>
    /// Duplicate resource found exception
    /// </summary>
    public class DuplicateResourceException : BusinessLogicException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message</param>
        public DuplicateResourceException(string message)
            : base(message, HttpStatusCode.Conflict)
        {
        }
    }

    /// <summary>
    /// Unauthorized esource access exception
    /// </summary>
    public class UnauthorizedResourceAccessException : BusinessLogicException
    {
        /// <summary>
        /// Construcctor
        /// </summary>
        /// <param name="message">Message</param>
        public UnauthorizedResourceAccessException(string message)
            : base(message, HttpStatusCode.Unauthorized)
        {
        }
    }

    /// <summary>
    /// Identity validation exception
    /// </summary>
    public class IdentityValidationException : BusinessLogicException
    {
        /// <summary>
        /// Erros
        /// </summary>
        public IEnumerable<string> Errors { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="errors">Errors list</param>
        public IdentityValidationException(string message, IEnumerable<string> errors = null)
            : base(message, HttpStatusCode.BadRequest)
        {
            Errors = errors;
        }
    }
}
