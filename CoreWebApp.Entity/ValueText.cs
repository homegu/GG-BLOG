using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebApp.Model
{
    public class ValueText<TValue, TText>
    {
        public TValue Value { get; set; }
        public TText Text { get; set; }
    }
}
