﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITextFilterService
    {
        Task<string> FilterText(string text, double similarityThreshold);
    }
}
