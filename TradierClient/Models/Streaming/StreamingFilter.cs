using System;
using System.Collections.Generic;
using System.Text;

namespace Tradier.Client.Models.Streaming
{
    public enum StreamingFilter
    {
        None,
        Trade,
        Quote,
        Summary,
        Timesale,
        Tradex
    }
}
