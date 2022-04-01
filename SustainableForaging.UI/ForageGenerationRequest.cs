using System;

namespace SustainableForaging.UI
{
    public class ForageGenerationRequest
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Count { get; set; }
    }
}
