using System.Collections.Generic;

namespace TryAspNetCore.Core
{
    public class ErrorInformation
    {
        public ErrorInformation()
        {
            Messages = new List<string>();
        }
        public string Title { get; set; }

        public IList<string> Messages { get; set; }
    }
}