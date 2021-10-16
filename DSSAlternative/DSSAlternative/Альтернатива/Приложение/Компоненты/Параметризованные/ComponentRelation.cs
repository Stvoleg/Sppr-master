﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using DSSAlternative.AHP;

namespace DSSAlternative.AppComponents
{
    public class DSSComponentParamRelation : DSSProject
    {
        [Parameter]
        public INodeRelation Relation { get; set; }
    }
}
