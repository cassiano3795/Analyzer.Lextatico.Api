#nullable enable

using System.Collections.Generic;
using System.Linq;

namespace Lextatico.Domain.Dtos.Message
{
    public class Message : IMessage
    {
        public Message()
        {

        }

        public IList<Error> Errors { get; set; } = new List<Error>();
        private string _locationObjectCreated = string.Empty;

        public void AddError(Error error) => Errors.Add(error);

        public void AddError(string property, string message) => Errors.Add(new Error(property, message));

        public bool IsValid() => !Errors.Any();

        public void ClearErrors() => Errors.Clear();

        public string GetLocation() => _locationObjectCreated;

        public void SetLocation(string location) => _locationObjectCreated = location;
    }

    public interface IMessage
    {
        IList<Error> Errors { get; set; }
        void AddError(Error error);
        void AddError(string property, string message);
        bool IsValid();
        void ClearErrors();
        string GetLocation();
        void SetLocation(string location);
    }
}
